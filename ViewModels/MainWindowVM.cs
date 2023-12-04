using DataIntegration.Bases;
using DataIntegration.Data;
using DataIntegration.Models;
using DataIntegration.Services;
using DataIntegration.Stores;
using Microsoft.Extensions.DependencyInjection;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace DataIntegration.ViewModels
{
    public class MainWindowVM :ObservableObject
    {
        private IServiceProvider _serviceProvider;
        private IMainService _mainService;
        private LandingViewVM _landingViewVM;

        private SettingsVM _settingsvm;
        private QueryStrings _querystrings;

        private string _clock;
        private string _currentdate = DateTime.Now.ToString("dd-MMM-yyyy");
        private Timer _timer;
        private object _currentpage;

        private int _days;
        private DateTime _date;
        private bool _isdateselected;
        private bool _isdayselected;
        private List<string> _selectedTables;

        public object CurrentPage
        {
            get { return _currentpage; }
            set { _currentpage = value; OnPropertyChanged(nameof(CurrentPage)); }
        }
        public string CurrentTime
        {
            get { return _clock; }
            set
            {
                _clock = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }
        public string BusinessDate
        {
            get { return _currentdate; }
            set
            {
                _currentdate = value;
                OnPropertyChanged(nameof(BusinessDate));
            }
        }

        public ICommand CloseWindowCommand { get; }
        public ICommand SettingsPageCommand { get; }
        public ICommand RunCodeCommand { get; }

        public MainWindowVM(IServiceProvider serviceProvider, IMainService mainservice)
        {
            _serviceProvider = serviceProvider;
            _mainService = mainservice;
            _landingViewVM = _serviceProvider.GetRequiredService<LandingViewVM>();
            _currentpage = _landingViewVM;

            CloseWindowCommand = new RelayCommand(CloseWindow);
            SettingsPageCommand = new RelayCommand(Settingspage);
            RunCodeCommand = new RelayCommand(RunCode);
            
            _querystrings = new QueryStrings();
            InitializeTimer();
        }
        private void Settingspage(object obj)
        {
            CurrentPage = _serviceProvider.GetRequiredService<SettingsVM>();
        }
        private void CloseWindow(object obj)
        {
            Application.Current.MainWindow.Close();
        }
        private void InitializeTimer()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CurrentTime = DateTime.Now.ToString("dd-MMM-yy HH:mm:ss");
        }


        private  async void RunCode(object obj)
        {
            string typeofdata = "";

            CurrentPage = _serviceProvider.GetRequiredService<LandingViewVM>();
            _settingsvm = _serviceProvider.GetRequiredService<SettingsVM>();

            _selectedTables = new List<string>(_settingsvm.AllTables.Where(item => item.IsChecked == true).Select(item => item.Name));
            _isdateselected = _settingsvm.IsDateSelected;
            _isdayselected = _settingsvm.IsDaysSelected;
            _days = _settingsvm.Days;
            _date = _settingsvm.SelectedDate;

            if (_isdayselected)
            {
                var yestedaysDay = DateTime.Now.AddDays(-_days);
                string olddate = yestedaysDay.ToString("yyyy-MM-dd");
                typeofdata = " and OPENDATE >= '" + olddate + "';";
            }
            if (_isdateselected)
            {
                string olddate = _date.ToString("yyyy-MM-dd");
                typeofdata = " and OPENDATE = '" + olddate + "';";
            }
            if (_selectedTables.Contains("DAYINFO"))
            {
                ListViewModel dayinfolog = new ListViewModel();
                dayinfolog.Descript = "Processing DayInfo Table...";
                dayinfolog.Status = "Processing";
                dayinfolog.ETA = "0 %";
                string query = _querystrings.DAYINFO + typeofdata;
                DataTable dt = ODBCHelper.SelectRec(query);
                int totalrows = dt.Rows.Count;
                int processedrows = 0;
                _landingViewVM.Alllogs.Add(dayinfolog);
                foreach (DataRow dr in dt.Rows)
                {
                    await Task.Run(() =>
                    {
                        Dayinfo dayinfo = new Dayinfoprocessing().processdayinfo(dr, 300);
                        _mainService.DeleteDayinfo(dayinfo.Opendate, dayinfo.Snum);
                        _mainService.SaveDayInfo(dayinfo);
                        processedrows++;
                        dayinfolog.ETA = Math.Round(((double)processedrows / totalrows) * 100).ToString() + " %";
                    });
                }
                dayinfolog.Status = "Done";
            }
            if (_selectedTables.Contains("POSDETAIL"))
            {
                ListViewModel dayinfolog = new ListViewModel();
                dayinfolog.Descript = "Processing Posdetail Table...";
                dayinfolog.Status = "Processing";
                dayinfolog.ETA = "0 %";
                string query = _querystrings.POSDETAIL + typeofdata;
                DataTable dt = ODBCHelper.SelectRec(query);
                int totalrows = dt.Rows.Count;
                int processedrows = 0;
                _landingViewVM.Alllogs.Add(dayinfolog);
                foreach (DataRow dr in dt.Rows)
                {
                    await Task.Run(() =>
                    {
                        Posdetail posdetail = new PosdetailProcessing().Posdetailprocessing(dr, 300);
                        _mainService.SavePosdetail(posdetail);
                        processedrows++;
                        dayinfolog.ETA = Math.Round(((double)processedrows / totalrows) * 100).ToString() + " %";
                    });
                }
            }
            if (_selectedTables.Contains("POSHEADER"))
            {
                ListViewModel dayinfolog = new ListViewModel();
                dayinfolog.Descript = "Processing Posheader Table...";
                dayinfolog.Status = "Processing";
                dayinfolog.ETA = "0 %";
                string query = _querystrings.POSHEADER + typeofdata;
                DataTable dt = ODBCHelper.SelectRec(query);
                int totalrows = dt.Rows.Count;
                int processedrows = 0;
                _landingViewVM.Alllogs.Add(dayinfolog);
                foreach (DataRow dr in dt.Rows)
                {
                    await Task.Run(() =>
                    {
                        Posheader posheader = new Posheaderdataprocessing().posheaderdataprocessing(dr, 300);
                        _mainService.SavePosheader(posheader);
                        processedrows++;
                        dayinfolog.ETA = Math.Round(((double)processedrows / totalrows) * 100).ToString() + " %";
                    });
                }
                dayinfolog.Status = "Done";
            }
            if (_selectedTables.Contains("POSBANK"))
            {
                ListViewModel posbanklog = new ListViewModel();
                posbanklog.Descript = "Processing Posbank Table...";
                posbanklog.Status = "Processing";
                posbanklog.ETA = "0 %";
                string query = _querystrings.POSBANK + typeofdata;
                DataTable dt = ODBCHelper.SelectRec(query);
                int totalrows = dt.Rows.Count;
                int processedrows = 0;
                _landingViewVM.Alllogs.Add(posbanklog);
                foreach (DataRow dr in dt.Rows)
                {
                    await Task.Run(() =>
                    {
                        Posbank posbank = new Posbankprocessing().processposbank(dr, 300);
                        _mainService.DeletePosbank(posbank.Opendate);
                        _mainService.SavePosbank(posbank);
                        processedrows++;
                        posbanklog.ETA = Math.Round(((double)processedrows / totalrows) * 100).ToString() + " %";
                    });
                }
                posbanklog.Status = "Done";
            }
            if (_selectedTables.Contains("PUNCHPAYROLL"))
            {
                ListViewModel punchpayrolllog = new ListViewModel();
                punchpayrolllog.Descript = "Processing PunchPayroll Table...";
                punchpayrolllog.Status = "Processing";
                punchpayrolllog.ETA = "0 %";
                string query = _querystrings.PUNCHPAYROLL + typeofdata;
                DataTable dt = ODBCHelper.SelectRec(query);
                int totalrows = dt.Rows.Count;
                int processedrows = 0;
                _landingViewVM.Alllogs.Add(punchpayrolllog);
                foreach (DataRow dr in dt.Rows)
                {
                    await Task.Run(() =>
                    {
                        Punchpayroll punchpayroll = new Punchpayrollprocessing().processpunchpayroll(dr, 300);
                        _mainService.SavePunchpayroll(punchpayroll);
                        processedrows++;
                        punchpayrolllog.ETA = Math.Round(((double)processedrows / totalrows) * 100).ToString() + " %";
                    });
                }
                punchpayrolllog.Status = "Done";
            }
            if (_selectedTables.Contains("STOCKTAKEDETAIL"))
            {
                ListViewModel stocktakedetaillog = new ListViewModel();
                stocktakedetaillog.Descript = "Processing Stocktakedetail Table...";
                stocktakedetaillog.Status = "Processing";
                stocktakedetaillog.ETA = "0 %";
                string query = _querystrings.STOCKTAKEDETAIL + typeofdata;
                DataTable dt = ODBCHelper.SelectRec(query);
                int totalrows = dt.Rows.Count;
                int processedrows = 0;
                _landingViewVM.Alllogs.Add(stocktakedetaillog);
                foreach (DataRow dr in dt.Rows)
                {
                    await Task.Run(() =>
                    {
                        Stocktakedetail stocktakedetail = new Stocktakedetailprocessing().processstocktakedeatil(dr, 300);
                        _mainService.SaveStocktakedetail(stocktakedetail);
                        processedrows++;
                        stocktakedetaillog.ETA = Math.Round(((double)processedrows / totalrows) * 100).ToString() + " %";
                    });
                }
                stocktakedetaillog.Status = "Done";
            }
            if (_selectedTables.Contains("EMPLOYEE"))
            {
                ListViewModel employeelog = new ListViewModel();
                employeelog.Descript = "Processing Employees Table...";
                employeelog.Status = "Processing";
                employeelog.ETA = "0 %";
                string query = _querystrings.EMPLOYEE + typeofdata;
                DataTable dt = ODBCHelper.SelectRec(query);
                int totalrows = dt.Rows.Count;
                int processedrows = 0;
                _landingViewVM.Alllogs.Add(employeelog);
                foreach (DataRow dr in dt.Rows)
                {
                    await Task.Run(() =>
                    {
                        Employee employee = new Employeeprocessing().processemployee(dr, 300);
                        _mainService.DeleteEmployee(employee.Empnum);
                        _mainService.SaveEmployee(employee);
                        processedrows++;
                        employeelog.ETA = Math.Round(((double)processedrows / totalrows) * 100).ToString() + " %";
                    });
                }
                employeelog.Status = "Done";
            }
        }
    }
}
