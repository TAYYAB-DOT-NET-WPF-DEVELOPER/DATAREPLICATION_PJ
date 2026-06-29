using DataIntegration.Bases;
using DataIntegration.Data;
using DataIntegration.Models;
using DataIntegration.Services;
using DataIntegration.Stores;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace DataIntegration.ViewModels
{
    /// <summary>
    /// Main shell view-model. Schedules and runs the periodic table-sync jobs
    /// (POS header/detail, day info, punch clock, punch payroll) and exposes
    /// per-table progress to the UI through <see cref="Alllogs"/>.
    ///
    /// Key behaviours relied on by this class:
    ///   * Every sync job is re-entrancy guarded (a tick that fires while the
    ///     same job is still running is skipped, not queued).
    ///   * All jobs are serialized through a single gate, because the data layer
    ///     (<see cref="IMainService"/> / ODBCHelper) is not assumed to be
    ///     thread-safe. If your data layer IS safe for concurrent use, you can
    ///     remove <c>_persistenceGate</c> to let jobs overlap.
    ///   * Database reads and writes run off the UI thread; only UI state
    ///     (log rows, clock) is touched on the dispatcher.
    /// </summary>
    public sealed class MainWindowVM : ObservableObject, IDisposable
    {
        // Subquery appended to every job so we only ever pull the latest business day.
        private const string LatestOpenDatePredicate = "=(select max(opendate) from dba.posheader)";

        private readonly IServiceProvider _serviceProvider;
        private readonly IMainService _mainService;
        private readonly QueryStrings _queryStrings;
        private readonly Dispatcher _dispatcher;
        private readonly CancellationTokenSource _cts;

        // Stateless row processors are created once and reused, instead of
        // allocating a new instance per database row as the original did.
        private readonly Posheaderdataprocessing _posHeaderProcessor = new Posheaderdataprocessing();
        private readonly PosdetailProcessing _posDetailProcessor = new PosdetailProcessing();
        private readonly Dayinfoprocessing _dayInfoProcessor = new Dayinfoprocessing();
        private readonly Punchclockprocessing _punchClockProcessor = new Punchclockprocessing();
        private readonly Punchpayrollprocessing _punchPayrollProcessor = new Punchpayrollprocessing();

        // One persistent log row per table. Reused on every run so the log does
        // not grow unboundedly (the 1-minute timers would otherwise add ~1,440
        // rows per table per day).
        private readonly ListViewModel _posHeaderLog = new ListViewModel { Descript = "POS Header", Status = "Idle", ETA = "0 %" };
        private readonly ListViewModel _posDetailLog = new ListViewModel { Descript = "POS Detail", Status = "Idle", ETA = "0 %" };
        private readonly ListViewModel _dayInfoLog = new ListViewModel { Descript = "Day Info", Status = "Idle", ETA = "0 %" };
        private readonly ListViewModel _punchClockLog = new ListViewModel { Descript = "Punch Clock", Status = "Idle", ETA = "0 %" };
        private readonly ListViewModel _punchPayrollLog = new ListViewModel { Descript = "Punch Payroll", Status = "Idle", ETA = "0 %" };

        // Per-job re-entrancy guards. WaitAsync(0) succeeds only if the job is
        // free; a tick that fires while the job is still running is dropped.
        private readonly SemaphoreSlim _posHeaderGate = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _posDetailGate = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _dayInfoGate = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _punchClockGate = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _punchPayrollGate = new SemaphoreSlim(1, 1);

        // Serializes all persistence so only one job hits the data layer at a time.
        private readonly SemaphoreSlim _persistenceGate = new SemaphoreSlim(1, 1);

        private DispatcherTimer _clockTimer;
        private DispatcherTimer _posHeaderTimer;
        private DispatcherTimer _posDetailTimer;
        private DispatcherTimer _dayInfoTimer;
        private DispatcherTimer _punchClockTimer;
        private DispatcherTimer _punchPayrollTimer;

        private LandingViewVM _landingViewVM;
        private SettingsVM _settingsVM;

        private string _clock;
        private string _storeName;
        private string _businessDate;
        private object _currentPage;
        private ObservableCollection<ListViewModel> _allLogs;

        private bool _disposed;

        public MainWindowVM(IServiceProvider serviceProvider, IMainService mainService)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _mainService = mainService ?? throw new ArgumentNullException(nameof(mainService));
            _queryStrings = new QueryStrings();
            _dispatcher = Application.Current?.Dispatcher ?? Dispatcher.CurrentDispatcher;
            _cts = new CancellationTokenSource();

            Alllogs = new ObservableCollection<ListViewModel>
            {
                _posHeaderLog,
                _posDetailLog,
                _dayInfoLog,
                _punchClockLog,
                _punchPayrollLog
            };

            _landingViewVM = _serviceProvider.GetRequiredService<LandingViewVM>();
            CurrentPage = _landingViewVM;

            BusinessDate = DateTime.Now.ToString("dd-MMM-yyyy");

            CloseWindowCommand = new RelayCommand(CloseWindow);
            SettingsPageCommand = new RelayCommand(ShowSettings);
            RunCodeCommand = new RelayCommand(_ => _ = RunAllNowAsync());

            ConfigureClock();
            ConfigureSyncTimers();
        }

        public ObservableCollection<ListViewModel> Alllogs
        {
            get => _allLogs;
            set { _allLogs = value; OnPropertyChanged(nameof(Alllogs)); }
        }

        public object CurrentPage
        {
            get => _currentPage;
            set { _currentPage = value; OnPropertyChanged(nameof(CurrentPage)); }
        }

        public string CurrentTime
        {
            get => _clock;
            set { _clock = value; OnPropertyChanged(nameof(CurrentTime)); }
        }

        public string BusinessDate
        {
            get => _businessDate;
            set { _businessDate = value; OnPropertyChanged(nameof(BusinessDate)); }
        }

        public string Storename
        {
            get => _storeName;
            set { _storeName = value; OnPropertyChanged(nameof(Storename)); }
        }

        public ICommand CloseWindowCommand { get; }
        public ICommand SettingsPageCommand { get; }
        public ICommand RunCodeCommand { get; }

        // ---------------------------------------------------------------------
        // Navigation / window commands
        // ---------------------------------------------------------------------

        private void ShowSettings(object _)
        {
            _settingsVM ??= _serviceProvider.GetRequiredService<SettingsVM>();
            CurrentPage = _settingsVM;
        }

        private void CloseWindow(object _)
        {
            Application.Current?.MainWindow?.Close();
        }

        // ---------------------------------------------------------------------
        // Timers
        // ---------------------------------------------------------------------

        private void ConfigureClock()
        {
            _clockTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _clockTimer.Tick += (_, __) => CurrentTime = DateTime.Now.ToString("dd-MMM-yy HH:mm:ss");
            _clockTimer.Start();
        }

        private void ConfigureSyncTimers()
        {
            _posHeaderTimer = CreateTimer(TimeSpan.FromMinutes(1), PosHeaderTick);
            _posDetailTimer = CreateTimer(TimeSpan.FromMinutes(1), PosDetailTick);
            _dayInfoTimer = CreateTimer(TimeSpan.FromHours(2), DayInfoTick);
            _punchClockTimer = CreateTimer(TimeSpan.FromHours(1), PunchClockTick);
            _punchPayrollTimer = CreateTimer(TimeSpan.FromHours(1), PunchPayrollTick);
        }

        private static DispatcherTimer CreateTimer(TimeSpan interval, EventHandler tick)
        {
            var timer = new DispatcherTimer { Interval = interval };
            timer.Tick += tick;
            timer.Start();
            return timer;
        }

        // async void is acceptable here because these are top-level event handlers.
        // RunIfFreeAsync never throws, so the handlers cannot crash the dispatcher.
        private async void PosHeaderTick(object sender, EventArgs e) =>
            await RunIfFreeAsync(_posHeaderGate, ProcessPosHeaderAsync);

        private async void PosDetailTick(object sender, EventArgs e) =>
            await RunIfFreeAsync(_posDetailGate, ProcessPosDetailAsync);

        private async void DayInfoTick(object sender, EventArgs e) =>
            await RunIfFreeAsync(_dayInfoGate, ProcessDayInfoAsync);

        private async void PunchClockTick(object sender, EventArgs e) =>
            await RunIfFreeAsync(_punchClockGate, ProcessPunchClockAsync);

        private async void PunchPayrollTick(object sender, EventArgs e) =>
            await RunIfFreeAsync(_punchPayrollGate, ProcessPunchPayrollAsync);

        // ---------------------------------------------------------------------
        // Job orchestration
        // ---------------------------------------------------------------------

        /// <summary>
        /// Runs <paramref name="work"/> only if the same job is not already
        /// running. This is the re-entrancy guard the original code intended but
        /// could not achieve (it reset its flag before the async work completed).
        /// </summary>
        private async Task RunIfFreeAsync(SemaphoreSlim jobGate, Func<CancellationToken, Task> work)
        {
            if (_disposed || _cts.IsCancellationRequested)
                return;

            // WaitAsync(0) returns false immediately if the job is still in flight.
            if (!await jobGate.WaitAsync(0).ConfigureAwait(false))
                return;

            try
            {
                await work(_cts.Token).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Defence in depth; ProcessTableAsync already handles its own errors.
                Log.Error(ex, "Unhandled error while running a scheduled sync job.");
            }
            finally
            {
                jobGate.Release();
            }
        }

        /// <summary>Manually triggers every sync now (bound to <see cref="RunCodeCommand"/>).</summary>
        private async Task RunAllNowAsync()
        {
            await RunIfFreeAsync(_posHeaderGate, ProcessPosHeaderAsync).ConfigureAwait(false);
            await RunIfFreeAsync(_posDetailGate, ProcessPosDetailAsync).ConfigureAwait(false);
            await RunIfFreeAsync(_dayInfoGate, ProcessDayInfoAsync).ConfigureAwait(false);
            await RunIfFreeAsync(_punchClockGate, ProcessPunchClockAsync).ConfigureAwait(false);
            await RunIfFreeAsync(_punchPayrollGate, ProcessPunchPayrollAsync).ConfigureAwait(false);
        }

        private Task ProcessPosHeaderAsync(CancellationToken ct) =>
            ProcessTableAsync(
                _posHeaderLog,
                _queryStrings.POSHEADER,
                " and PH.OPENDATE",
                row => _posHeaderProcessor.posheaderdataprocessing(row),
                entity => _mainService.SavePosheader(entity),
                ct);

        private Task ProcessPosDetailAsync(CancellationToken ct) =>
            ProcessTableAsync(
                _posDetailLog,
                _queryStrings.POSDETAIL,
                " and OPENDATE",
                row => _posDetailProcessor.Posdetailprocessing(row),
                entity => _mainService.SavePosdetail(entity),
                ct);

        private Task ProcessDayInfoAsync(CancellationToken ct) =>
            ProcessTableAsync(
                _dayInfoLog,
                _queryStrings.DAYINFO,
                " and OPENDATE",
                row => _dayInfoProcessor.processdayinfo(row),
                entity =>
                {
                    // Upsert: clear any existing row for this key, then insert.
                    _mainService.DeleteDayinfo(entity.Opendate, entity.Snum);
                    _mainService.SaveDayInfo(entity);
                },
                ct);

        private Task ProcessPunchClockAsync(CancellationToken ct) =>
            ProcessTableAsync(
                _punchClockLog,
                _queryStrings.PUNCHCLOCK,
                "  OPENDATE", // preserved exactly from the original (no "and")
                row => _punchClockProcessor.ProcessPunchclock(row),
                entity =>
                {
                    _mainService.DeletePunchclock(entity.Uniqueid, entity.Storenum);
                    _mainService.SavePunchclock(entity);
                },
                ct);

        private Task ProcessPunchPayrollAsync(CancellationToken ct) =>
            ProcessTableAsync(
                _punchPayrollLog,
                _queryStrings.PUNCHPAYROLL,
                " and OPENDATE",
                row => _punchPayrollProcessor.processpunchpayroll(row),
                entity =>
                {
                    // NOTE: the original saved THEN deleted by the same key, which
                    // would remove the row it had just written. Reordered to the
                    // delete-then-save upsert pattern used by the other tables.
                    // Verify against your schema before deploying.
                    _mainService.DeletePunchPayRoll(entity.Punchindex, entity.Storeid);
                    _mainService.SavePunchpayroll(entity);
                },
                ct);

        /// <summary>
        /// Shared pipeline for every table sync: read the latest rows, map each
        /// one, persist it, and report progress. All database work runs on a
        /// background thread; only the log row is updated on the UI thread.
        /// </summary>
        private async Task ProcessTableAsync<TEntity>(
            ListViewModel log,
            string baseQuery,
            string dateClause,
            Func<DataRow, TEntity> map,
            Action<TEntity> persist,
            CancellationToken cancellationToken)
        {
            UpdateLog(log, status: "Processing", eta: "0 %");

            string query = baseQuery + dateClause + LatestOpenDatePredicate;

            // Serialize against the other jobs (shared, non-thread-safe data layer).
            await _persistenceGate.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                await Task.Run(() =>
                {
                    using (DataTable table = ODBCHelper.SelectRec(query))
                    {
                        int totalRows = table.Rows.Count;
                        Log.Information("{Table}: {RowCount} row(s) to process.", log.Descript, totalRows);

                        if (totalRows == 0)
                        {
                            UpdateLog(log, status: "Done", eta: "100 %");
                            return;
                        }

                        int processed = 0;
                        int lastPercent = -1;

                        foreach (DataRow row in table.Rows)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            TEntity entity = map(row);
                            persist(entity);

                            processed++;

                            // Only push a UI update when the whole-number percent
                            // changes, so a large table dispatches ~100 updates
                            // rather than one per row.
                            int percent = (int)Math.Round((double)processed / totalRows * 100.0);
                            if (percent != lastPercent)
                            {
                                lastPercent = percent;
                                UpdateLog(log, eta: percent + " %");
                            }
                        }
                    }
                }, cancellationToken).ConfigureAwait(false);

                UpdateLog(log, status: "Done", eta: "100 %");
            }
            catch (OperationCanceledException)
            {
                UpdateLog(log, status: "Cancelled");
                Log.Warning("{Table} sync was cancelled.", log.Descript);
            }
            catch (Exception ex)
            {
                UpdateLog(log, status: "Failed");
                Log.Error(ex, "{Table} sync failed.", log.Descript);
            }
            finally
            {
                _persistenceGate.Release();
            }
        }

        // ---------------------------------------------------------------------
        // UI helpers (all marshalled to the dispatcher)
        // ---------------------------------------------------------------------

        private void UpdateLog(ListViewModel log, string status = null, string eta = null)
        {
            void Apply()
            {
                if (status != null) log.Status = status;
                if (eta != null) log.ETA = eta;
            }

            if (_dispatcher.CheckAccess())
                Apply();
            else
                _dispatcher.BeginInvoke((Action)Apply);
        }

        // ---------------------------------------------------------------------
        // Disposal
        // ---------------------------------------------------------------------

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;

            _clockTimer?.Stop();
            _posHeaderTimer?.Stop();
            _posDetailTimer?.Stop();
            _dayInfoTimer?.Stop();
            _punchClockTimer?.Stop();
            _punchPayrollTimer?.Stop();

            try { _cts.Cancel(); } catch (ObjectDisposedException) { }
            _cts.Dispose();

            _posHeaderGate.Dispose();
            _posDetailGate.Dispose();
            _dayInfoGate.Dispose();
            _punchClockGate.Dispose();
            _punchPayrollGate.Dispose();
            _persistenceGate.Dispose();
        }
    }
}