using DataIntegration.Bases;
using DataIntegration.Data;
using DataIntegration.Models;
using DataIntegration.Services;
using DataIntegration.Stores;
using Microsoft.Extensions.DependencyInjection;
using POS.Models;
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
        private readonly PoshDeliveryProcessing _posHDeliveryProcessor = new PoshDeliveryProcessing();
        private readonly Posbankprocessing _posBankProcessor = new Posbankprocessing();
        private readonly Memberprocessing.MemberProcessing _memberProcessor = new Memberprocessing.MemberProcessing();
        private readonly ProductProcessing _productProcessor = new ProductProcessing();
        private readonly PromoProcessing _promoProcessor = new PromoProcessing();
        private readonly MethodPayProcessing _methodPayProcessor = new MethodPayProcessing();
        private readonly Employeeprocessing _employeeProcessor = new Employeeprocessing();
        private readonly Salestypeprocessing.SalestypeProcessing _salesTypeProcessor = new Salestypeprocessing.SalestypeProcessing();

        // One persistent log row per table. Reused on every run so the log does not grow unboundedly.
        // Default selected tables (IsSelected = true)
        private readonly ListViewModel _posHeaderLog = new ListViewModel { Descript = "POS Header", Status = "Idle", ETA = "0 %", IsSelected = true };
        private readonly ListViewModel _posDetailLog = new ListViewModel { Descript = "POS Detail", Status = "Idle", ETA = "0 %", IsSelected = true };
        private readonly ListViewModel _dayInfoLog = new ListViewModel { Descript = "Day Info", Status = "Idle", ETA = "0 %", IsSelected = true };
        private readonly ListViewModel _punchClockLog = new ListViewModel { Descript = "Punch Clock", Status = "Idle", ETA = "0 %", IsSelected = true };
        private readonly ListViewModel _punchPayrollLog = new ListViewModel { Descript = "Punch Payroll", Status = "Idle", ETA = "0 %", IsSelected = true };
        private readonly ListViewModel _posHDeliveryLog = new ListViewModel { Descript = "POS H-Delivery", Status = "Idle", ETA = "0 %", IsSelected = true };
        private readonly ListViewModel _posBankLog = new ListViewModel { Descript = "POS Bank", Status = "Idle", ETA = "0 %", IsSelected = true };
        private readonly ListViewModel _memberLog = new ListViewModel { Descript = "Member", Status = "Idle", ETA = "0 %", IsSelected = true };

        // On-demand tables (IsSelected = false)
        private readonly ListViewModel _productLog = new ListViewModel { Descript = "Product", Status = "Idle", ETA = "0 %", IsSelected = false };
        private readonly ListViewModel _promoLog = new ListViewModel { Descript = "Promo", Status = "Idle", ETA = "0 %", IsSelected = false };
        private readonly ListViewModel _methodPayLog = new ListViewModel { Descript = "Method Pay", Status = "Idle", ETA = "0 %", IsSelected = false };
        private readonly ListViewModel _employeeLog = new ListViewModel { Descript = "Employee", Status = "Idle", ETA = "0 %", IsSelected = false };
        private readonly ListViewModel _salesTypeLog = new ListViewModel { Descript = "Sales Type", Status = "Idle", ETA = "0 %", IsSelected = false };

        // Per-job re-entrancy guards.
        private readonly SemaphoreSlim _posHeaderGate = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _posDetailGate = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _dayInfoGate = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _punchClockGate = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _punchPayrollGate = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _posHDeliveryGate = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _posBankGate = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _memberGate = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _productGate = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _promoGate = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _methodPayGate = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _employeeGate = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _salesTypeGate = new SemaphoreSlim(1, 1);

        // Serializes all persistence so only one job hits the data layer at a time.
        private readonly SemaphoreSlim _persistenceGate = new SemaphoreSlim(1, 1);

        private DispatcherTimer _clockTimer;
        private DispatcherTimer _posHeaderTimer;
        private DispatcherTimer _posDetailTimer;
        private DispatcherTimer _dayInfoTimer;
        private DispatcherTimer _punchClockTimer;
        private DispatcherTimer _punchPayrollTimer;
        private DispatcherTimer _posHDeliveryTimer;
        private DispatcherTimer _posBankTimer;
        private DispatcherTimer _memberTimer;
        private DispatcherTimer _productTimer;
        private DispatcherTimer _promoTimer;
        private DispatcherTimer _methodPayTimer;
        private DispatcherTimer _employeeTimer;
        private DispatcherTimer _salesTypeTimer;

        private LandingViewVM _landingViewVM;
        private SettingsVM _settingsVM;

        private string _clock;
        private string _storeName;
        private string _businessDate;
        private object _currentPage;
        private ObservableCollection<ListViewModel> _allLogs;

        private DateTime _selectedDate = DateTime.Today;
        private int _selectedDays = 3; // Default to 3 days
        private bool _isDateSelected = true;
        private bool _isDaysSelected = false;
        private Visibility _dateVisibility = Visibility.Visible;
        private Visibility _daysVisibility = Visibility.Collapsed;

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
                _punchPayrollLog,
                _posHDeliveryLog,
                _posBankLog,
                _memberLog,
                _productLog,
                _promoLog,
                _methodPayLog,
                _employeeLog,
                _salesTypeLog
            };

            _landingViewVM = _serviceProvider.GetRequiredService<LandingViewVM>();
            CurrentPage = _landingViewVM;

            BusinessDate = DateTime.Now.ToString("dd-MMM-yyyy");

            CloseWindowCommand = new RelayCommand(CloseWindow);
            SettingsPageCommand = new RelayCommand(ShowSettings);
            RunCodeCommand = new RelayCommand(_ => _ = RunAllNowAsync());
            RunManualSyncCommand = new RelayCommand(_ => _ = RunManualSyncAsync());

            ConfigureClock();
            ConfigureSyncTimers();

            // Trigger automatic sync of selected tables on startup after 2 seconds
            _ = Task.Run(async () =>
            {
                await Task.Delay(2000);
                await RunAllNowAsync();
            });
        }

        public bool IsDateSelected
        {
            get => _isDateSelected;
            set
            {
                _isDateSelected = value;
                OnPropertyChanged(nameof(IsDateSelected));
                if (value)
                {
                    _isDaysSelected = false;
                    OnPropertyChanged(nameof(IsDaysSelected));
                    DateVisibility = Visibility.Visible;
                    DaysVisibility = Visibility.Collapsed;
                }
                OnPropertyChanged(nameof(DateRangeText));
            }
        }

        public bool IsDaysSelected
        {
            get => _isDaysSelected;
            set
            {
                _isDaysSelected = value;
                OnPropertyChanged(nameof(IsDaysSelected));
                if (value)
                {
                    _isDateSelected = false;
                    OnPropertyChanged(nameof(IsDateSelected));
                    DateVisibility = Visibility.Collapsed;
                    DaysVisibility = Visibility.Visible;
                }
                OnPropertyChanged(nameof(DateRangeText));
            }
        }

        public Visibility DateVisibility
        {
            get => _dateVisibility;
            set { _dateVisibility = value; OnPropertyChanged(nameof(DateVisibility)); }
        }

        public Visibility DaysVisibility
        {
            get => _daysVisibility;
            set { _daysVisibility = value; OnPropertyChanged(nameof(DaysVisibility)); }
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
                OnPropertyChanged(nameof(DateRangeText));
            }
        }

        public int SelectedDays
        {
            get => _selectedDays;
            set
            {
                _selectedDays = value;
                OnPropertyChanged(nameof(SelectedDays));
                OnPropertyChanged(nameof(DateRangeText));
            }
        }

        public string DateRangeText
        {
            get
            {
                if (IsDateSelected)
                {
                    return $"Date: {SelectedDate:yyyy-MM-dd}";
                }
                else
                {
                    return $"Range: MaxDate - {SelectedDays} days to MaxDate";
                }
            }
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
        public ICommand RunManualSyncCommand { get; }

        private void ShowSettings(object _)
        {
            _settingsVM ??= _serviceProvider.GetRequiredService<SettingsVM>();
            CurrentPage = _settingsVM;
        }

        private void CloseWindow(object _)
        {
            Application.Current?.MainWindow?.Close();
        }

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
            _dayInfoTimer = CreateTimer(TimeSpan.FromMinutes(2), DayInfoTick);
            _punchClockTimer = CreateTimer(TimeSpan.FromMinutes(1), PunchClockTick);
            _punchPayrollTimer = CreateTimer(TimeSpan.FromMinutes(1), PunchPayrollTick);
            _posHDeliveryTimer = CreateTimer(TimeSpan.FromMinutes(1), PosHDeliveryTick);
            _posBankTimer = CreateTimer(TimeSpan.FromHours(1), PosBankTick);
            _memberTimer = CreateTimer(TimeSpan.FromHours(2), MemberTick);
            _productTimer = CreateTimer(TimeSpan.FromHours(4), ProductTick);
            _promoTimer = CreateTimer(TimeSpan.FromHours(4), PromoTick);
            _methodPayTimer = CreateTimer(TimeSpan.FromHours(4), MethodPayTick);
            _employeeTimer = CreateTimer(TimeSpan.FromHours(2), EmployeeTick);
            _salesTypeTimer = CreateTimer(TimeSpan.FromHours(2), SalesTypeTick);
        }

        private static DispatcherTimer CreateTimer(TimeSpan interval, EventHandler tick)
        {
            var timer = new DispatcherTimer { Interval = interval };
            timer.Tick += tick;
            timer.Start();
            return timer;
        }

        private async void PosHeaderTick(object sender, EventArgs e) => await RunIfFreeAsync(_posHeaderGate, ct => ProcessPosHeaderAsync(false, null, null, ct));
        private async void PosDetailTick(object sender, EventArgs e) => await RunIfFreeAsync(_posDetailGate, ct => ProcessPosDetailAsync(false, null, null, ct));
        private async void DayInfoTick(object sender, EventArgs e) => await RunIfFreeAsync(_dayInfoGate, ct => ProcessDayInfoAsync(false, null, null, ct));
        private async void PunchClockTick(object sender, EventArgs e) => await RunIfFreeAsync(_punchClockGate, ct => ProcessPunchClockAsync(false, null, null, ct));
        private async void PunchPayrollTick(object sender, EventArgs e) => await RunIfFreeAsync(_punchPayrollGate, ct => ProcessPunchPayrollAsync(false, null, null, ct));
        private async void PosHDeliveryTick(object sender, EventArgs e) => await RunIfFreeAsync(_posHDeliveryGate, ct => ProcessPosHDeliveryAsync(false, null, null, ct));
        private async void PosBankTick(object sender, EventArgs e) => await RunIfFreeAsync(_posBankGate, ct => ProcessPosBankAsync(false, null, null, ct));
        private async void MemberTick(object sender, EventArgs e) => await RunIfFreeAsync(_memberGate, ct => ProcessMemberAsync(false, null, null, ct));
        private async void ProductTick(object sender, EventArgs e) => await RunIfFreeAsync(_productGate, ct => ProcessProductAsync(false, null, null, ct));
        private async void PromoTick(object sender, EventArgs e) => await RunIfFreeAsync(_promoGate, ct => ProcessPromoAsync(false, null, null, ct));
        private async void MethodPayTick(object sender, EventArgs e) => await RunIfFreeAsync(_methodPayGate, ct => ProcessMethodPayAsync(false, null, null, ct));
        private async void EmployeeTick(object sender, EventArgs e) => await RunIfFreeAsync(_employeeGate, ct => ProcessEmployeeAsync(false, null, null, ct));
        private async void SalesTypeTick(object sender, EventArgs e) => await RunIfFreeAsync(_salesTypeGate, ct => ProcessSalesTypeAsync(false, null, null, ct));

        private async Task RunIfFreeAsync(SemaphoreSlim jobGate, Func<CancellationToken, Task> work)
        {
            if (_disposed || _cts.IsCancellationRequested)
                return;

            if (!await jobGate.WaitAsync(0).ConfigureAwait(false))
                return;

            try
            {
                await work(_cts.Token).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unhandled error while running a scheduled sync job.");
            }
            finally
            {
                jobGate.Release();
            }
        }

        private async Task RunAllNowAsync()
        {
            await RunIfFreeAsync(_posHeaderGate, ct => ProcessPosHeaderAsync(false, null, null, ct)).ConfigureAwait(false);
            await RunIfFreeAsync(_posDetailGate, ct => ProcessPosDetailAsync(false, null, null, ct)).ConfigureAwait(false);
            await RunIfFreeAsync(_dayInfoGate, ct => ProcessDayInfoAsync(false, null, null, ct)).ConfigureAwait(false);
            await RunIfFreeAsync(_punchClockGate, ct => ProcessPunchClockAsync(false, null, null, ct)).ConfigureAwait(false);
            await RunIfFreeAsync(_punchPayrollGate, ct => ProcessPunchPayrollAsync(false, null, null, ct)).ConfigureAwait(false);
            await RunIfFreeAsync(_posHDeliveryGate, ct => ProcessPosHDeliveryAsync(false, null, null, ct)).ConfigureAwait(false);
            await RunIfFreeAsync(_posBankGate, ct => ProcessPosBankAsync(false, null, null, ct)).ConfigureAwait(false);
            await RunIfFreeAsync(_memberGate, ct => ProcessMemberAsync(false, null, null, ct)).ConfigureAwait(false);
            await RunIfFreeAsync(_productGate, ct => ProcessProductAsync(false, null, null, ct)).ConfigureAwait(false);
            await RunIfFreeAsync(_promoGate, ct => ProcessPromoAsync(false, null, null, ct)).ConfigureAwait(false);
            await RunIfFreeAsync(_methodPayGate, ct => ProcessMethodPayAsync(false, null, null, ct)).ConfigureAwait(false);
            await RunIfFreeAsync(_employeeGate, ct => ProcessEmployeeAsync(false, null, null, ct)).ConfigureAwait(false);
            await RunIfFreeAsync(_salesTypeGate, ct => ProcessSalesTypeAsync(false, null, null, ct)).ConfigureAwait(false);
        }

        private async Task RunManualSyncAsync()
        {
            var end = SelectedDate;
            var start = SelectedDate.AddDays(-SelectedDays);

            await RunIfFreeAsync(_posHeaderGate, ct => ProcessPosHeaderAsync(true, start, end, ct)).ConfigureAwait(false);
            await RunIfFreeAsync(_posDetailGate, ct => ProcessPosDetailAsync(true, start, end, ct)).ConfigureAwait(false);
            await RunIfFreeAsync(_dayInfoGate, ct => ProcessDayInfoAsync(true, start, end, ct)).ConfigureAwait(false);
            await RunIfFreeAsync(_punchClockGate, ct => ProcessPunchClockAsync(true, start, end, ct)).ConfigureAwait(false);
            await RunIfFreeAsync(_punchPayrollGate, ct => ProcessPunchPayrollAsync(true, start, end, ct)).ConfigureAwait(false);
            await RunIfFreeAsync(_posHDeliveryGate, ct => ProcessPosHDeliveryAsync(true, start, end, ct)).ConfigureAwait(false);
            await RunIfFreeAsync(_posBankGate, ct => ProcessPosBankAsync(true, start, end, ct)).ConfigureAwait(false);
            await RunIfFreeAsync(_memberGate, ct => ProcessMemberAsync(true, start, end, ct)).ConfigureAwait(false);
        }

        private Task ProcessPosHeaderAsync(bool isManual, DateTime? start, DateTime? end, CancellationToken ct) =>
            ProcessTableAsync(_posHeaderLog, _queryStrings.POSHEADER, "PH.OPENDATE", false, isManual, start, end, row => _posHeaderProcessor.posheaderdataprocessing(row), entity => _mainService.SavePosheader(entity), ct);

        private Task ProcessPosDetailAsync(bool isManual, DateTime? start, DateTime? end, CancellationToken ct) =>
            ProcessTableAsync(_posDetailLog, _queryStrings.POSDETAIL, "OPENDATE", false, isManual, start, end, row => _posDetailProcessor.Posdetailprocessing(row), entity => _mainService.SavePosdetail(entity), ct);

        private Task ProcessDayInfoAsync(bool isManual, DateTime? start, DateTime? end, CancellationToken ct) =>
            ProcessTableAsync(_dayInfoLog, _queryStrings.DAYINFO, "OPENDATE", false, isManual, start, end, row => _dayInfoProcessor.processdayinfo(row), entity => { _mainService.DeleteDayinfo(entity.Opendate, entity.Snum); _mainService.SaveDayInfo(entity); }, ct);

        private Task ProcessPunchClockAsync(bool isManual, DateTime? start, DateTime? end, CancellationToken ct) =>
            ProcessTableAsync(_punchClockLog, _queryStrings.PUNCHCLOCK, "OPENDATE", true, isManual, start, end, row => _punchClockProcessor.ProcessPunchclock(row), entity => { _mainService.DeletePunchclock(entity.Uniqueid, entity.Storenum); _mainService.SavePunchclock(entity); }, ct);

        private Task ProcessPunchPayrollAsync(bool isManual, DateTime? start, DateTime? end, CancellationToken ct) =>
            ProcessTableAsync(_punchPayrollLog, _queryStrings.PUNCHPAYROLL, "OPENDATE", false, isManual, start, end, row => _punchPayrollProcessor.processpunchpayroll(row), entity => { _mainService.DeletePunchPayRoll(entity.Punchindex, entity.Storeid); _mainService.SavePunchpayroll(entity); }, ct);

        private Task ProcessPosHDeliveryAsync(bool isManual, DateTime? start, DateTime? end, CancellationToken ct) =>
            ProcessTableAsync(_posHDeliveryLog, _queryStrings.POSHDELIVERY, "OpenDate", false, isManual, start, end, row => _posHDeliveryProcessor.PoshDeliveryprocessing(row), entity => _mainService.SavePoshdelivery(entity), ct);

        private Task ProcessPosBankAsync(bool isManual, DateTime? start, DateTime? end, CancellationToken ct) =>
            ProcessTableAsync(_posBankLog, _queryStrings.POSBANK, "OPENDATE", false, isManual, start, end, row => _posBankProcessor.processposbank(row), entity => _mainService.SavePosbank(entity), ct);

        private Task ProcessMemberAsync(bool isManual, DateTime? start, DateTime? end, CancellationToken ct)
        {
            string memberQuery = _queryStrings.MEMBER;
            if (isManual)
            {
                if (IsDateSelected && start.HasValue)
                {
                    memberQuery += $" and memcode in (select memcode from dba.posheader where opendate = '{start.Value:yyyy-MM-dd}')";
                }
                else
                {
                    DateTime maxDate = DateTime.Today;
                    try
                    {
                        using (var maxDateDt = ODBCHelper.SelectRec("select max(opendate) from dba.posheader"))
                        {
                            if (maxDateDt.Rows.Count > 0 && maxDateDt.Rows[0][0] != DBNull.Value)
                            {
                                maxDate = Convert.ToDateTime(maxDateDt.Rows[0][0]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Failed to retrieve max opendate from posheader in ProcessMemberAsync. Falling back to today.");
                    }
                    var calculatedStartDate = maxDate.AddDays(-SelectedDays);
                    memberQuery += $" and memcode in (select memcode from dba.posheader where opendate >= '{calculatedStartDate:yyyy-MM-dd}' and opendate <= '{maxDate:yyyy-MM-dd}')";
                }
            }
            else
            {
                memberQuery += " and memcode in (select memcode from dba.posheader where opendate = (select max(opendate) from dba.posheader))";
            }

            return ProcessTableAsync(_memberLog, memberQuery, null, false, isManual, start, end, row => _memberProcessor.ProcessMember(row), entity => _mainService.SaveMember(entity), ct);
        }

        private Task ProcessProductAsync(bool isManual, DateTime? start, DateTime? end, CancellationToken ct) =>
            ProcessTableAsync(_productLog, _queryStrings.PRODUCT, null, false, isManual, start, end, row => _productProcessor.ProcessProduct(row), entity => _mainService.SaveProduct(entity), ct, "PRODUCT");

        private Task ProcessPromoAsync(bool isManual, DateTime? start, DateTime? end, CancellationToken ct) =>
            ProcessTableAsync(_promoLog, _queryStrings.PROMO, null, false, isManual, start, end, row => _promoProcessor.ProcessPromo(row), entity => _mainService.SavePromo(entity), ct, "PROMO");

        private Task ProcessMethodPayAsync(bool isManual, DateTime? start, DateTime? end, CancellationToken ct) =>
            ProcessTableAsync(_methodPayLog, _queryStrings.METHODPAY, null, false, isManual, start, end, row => _methodPayProcessor.ProcessMethodPay(row), entity => _mainService.SaveMethodPay(entity), ct, "METHODPAY");

        private Task ProcessEmployeeAsync(bool isManual, DateTime? start, DateTime? end, CancellationToken ct) =>
            ProcessTableAsync(_employeeLog, _queryStrings.EMPLOYEE, null, false, isManual, start, end, row => _employeeProcessor.processemployee(row), entity => _mainService.SaveEmployee(entity), ct, "EMPLOYEE");

        private Task ProcessSalesTypeAsync(bool isManual, DateTime? start, DateTime? end, CancellationToken ct) =>
            ProcessTableAsync(_salesTypeLog, _queryStrings.SALESTYPE, null, false, isManual, start, end, row => _salesTypeProcessor.ProcessSalestype(row), entity => _mainService.SaveSalestype(entity), ct, "SALESTYPE");

        private async Task ProcessTableAsync<TEntity>(ListViewModel log, string baseQuery, string dateColumn, bool isPunchClock, bool isManual, DateTime? startDate, DateTime? endDate, Func<DataRow, TEntity> map, Action<TEntity> persist, CancellationToken cancellationToken, string clearTableName = null)
        {
            if (!log.IsSelected)
            {
                UpdateLog(log, status: "Skipped", eta: "---", progress: 0);
                return;
            }

            UpdateLog(log, status: "Processing", eta: "0 %", progress: 0);

            // If a table name is provided, clear all its existing data in Oracle before syncing
            if (!string.IsNullOrEmpty(clearTableName))
            {
                try
                {
                    await _mainService.ClearTable(clearTableName).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    UpdateLog(log, status: "Failed", progress: 0);
                    Log.Error(ex, "Failed to clear table {Table} before sync.", clearTableName);
                    return;
                }
            }

            string query;
            if (string.IsNullOrEmpty(dateColumn))
            {
                query = baseQuery;
            }
            else if (isManual)
            {
                if (IsDateSelected && startDate.HasValue)
                {
                    query = baseQuery + (isPunchClock ? $" {dateColumn} = '{startDate.Value:yyyy-MM-dd}'" : $" and {dateColumn} = '{startDate.Value:yyyy-MM-dd}'");
                }
                else
                {
                    DateTime maxDate = DateTime.Today;
                    try
                    {
                        using (var maxDateDt = ODBCHelper.SelectRec("select max(opendate) from dba.posheader"))
                        {
                            if (maxDateDt.Rows.Count > 0 && maxDateDt.Rows[0][0] != DBNull.Value)
                            {
                                maxDate = Convert.ToDateTime(maxDateDt.Rows[0][0]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Failed to retrieve max opendate from posheader in ProcessTableAsync. Falling back to today.");
                    }
                    var calculatedStartDate = maxDate.AddDays(-SelectedDays);
                    query = baseQuery + (isPunchClock 
                        ? $" {dateColumn} >= '{calculatedStartDate:yyyy-MM-dd}' and {dateColumn} <= '{maxDate:yyyy-MM-dd}'" 
                        : $" and {dateColumn} >= '{calculatedStartDate:yyyy-MM-dd}' and {dateColumn} <= '{maxDate:yyyy-MM-dd}'");
                }
            }
            else
            {
                query = baseQuery + (isPunchClock ? $" {dateColumn}" : $" and {dateColumn}") + LatestOpenDatePredicate;
            }

            await _persistenceGate.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                await Task.Run(() =>
                {
                    using (DataTable table = ODBCHelper.SelectRec(query))
                    {
                        int totalRows = table.Rows.Count;
                        if (totalRows == 0) { UpdateLog(log, status: "Done", eta: "100 %", progress: 100); return; }
                        int processed = 0;
                        int lastPercent = -1;
                        foreach (DataRow row in table.Rows)
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                            persist(map(row));
                            processed++;
                            int percent = (int)Math.Round((double)processed / totalRows * 100.0);
                            if (percent != lastPercent) { lastPercent = percent; UpdateLog(log, eta: percent + " %", progress: percent); }
                        }
                    }
                }, cancellationToken).ConfigureAwait(false);
                UpdateLog(log, status: "Done", eta: "100 %", progress: 100);
            }
            catch (OperationCanceledException) { UpdateLog(log, status: "Cancelled", progress: 0); }
            catch (Exception ex) 
            { 
                UpdateLog(log, status: "Failed", progress: 0); 
                Log.Error(ex, "{Table} sync failed.", log.Descript);
                try
                {
                    string errorContent = $"Timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\r\n" +
                                         $"Table: {log.Descript}\r\n" +
                                         $"Error: {ex.Message}\r\n" +
                                         $"Stack Trace:\r\n{ex.StackTrace}\r\n" +
                                         $"--------------------------------------------------\r\n";
                    System.IO.File.WriteAllText("LATEST_ERROR.txt", errorContent);
                }
                catch { }
            }
            finally { _persistenceGate.Release(); }
        }

        private bool _hasErrors;
        public bool HasErrors
        {
            get => _hasErrors;
            set { _hasErrors = value; OnPropertyChanged(nameof(HasErrors)); }
        }

        private void UpdateLog(ListViewModel log, string status = null, string eta = null, double? progress = null)
        {
            void Apply()
            {
                if (status != null) log.Status = status;
                if (eta != null) log.ETA = eta;
                if (progress != null) log.ProgressValue = progress.Value;

                // Recalculate HasErrors
                bool anyError = false;
                foreach (var item in Alllogs)
                {
                    if (item.Status == "Failed")
                    {
                        anyError = true;
                        break;
                    }
                }
                HasErrors = anyError;
            }
            if (_dispatcher.CheckAccess()) Apply(); else _dispatcher.BeginInvoke((Action)Apply);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _clockTimer?.Stop();
            _posHeaderTimer?.Stop();
            _posDetailTimer?.Stop();
            _dayInfoTimer?.Stop();
            _punchClockTimer?.Stop();
            _punchPayrollTimer?.Stop();
            _posHDeliveryTimer?.Stop();
            _posBankTimer?.Stop();
            _memberTimer?.Stop();
            _productTimer?.Stop();
            _promoTimer?.Stop();
            _methodPayTimer?.Stop();
            _employeeTimer?.Stop();
            _salesTypeTimer?.Stop();
            try { _cts.Cancel(); } catch (ObjectDisposedException) { }
            _cts.Dispose();
            _posHeaderGate.Dispose();
            _posDetailGate.Dispose();
            _dayInfoGate.Dispose();
            _punchClockGate.Dispose();
            _punchPayrollGate.Dispose();
            _posHDeliveryGate.Dispose();
            _posBankGate.Dispose();
            _memberGate.Dispose();
            _productGate.Dispose();
            _promoGate.Dispose();
            _methodPayGate.Dispose();
            _employeeGate.Dispose();
            _salesTypeGate.Dispose();
            _persistenceGate.Dispose();
        }
    }
}