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


        private void RunCode(object obj)
        {
            string typeofdata = "";

            CurrentPage ??= _serviceProvider.GetRequiredService<LandingViewVM>();
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
                    Dayinfo dayinfo = new Dayinfoprocessing().processdayinfo(dr, 300);

                    _mainService.DeleteDayinfo(dayinfo.Opendate, dayinfo.Snum);
                    _mainService.SaveDayInfo(dayinfo);
                    processedrows++;
                    dayinfolog.ETA = Math.Round(((double)processedrows / totalrows) * 100).ToString() + " %";



                }
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
                    Posdetail posdetail = new PosdetailProcessing().Posdetailprocessing(dr, 300);

                    _mainService.SavePosdetail(posdetail);
                    processedrows++;
                    dayinfolog.ETA = Math.Round(((double)processedrows / totalrows) * 100).ToString() + " %";



                }
            }


        }
    }
}
