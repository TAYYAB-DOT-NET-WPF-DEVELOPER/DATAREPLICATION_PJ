using DataIntegration.Bases;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace DataIntegration.ViewModels
{
    public class MainWindowVM :ObservableObject
    {
        private IServiceProvider _serviceProvider;
        private string _clock;
        private string _currentdate = DateTime.Now.ToString("dd-MMM-yyyy");
        private Timer _timer;
        private object _currentpage;

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
        public MainWindowVM(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            CloseWindowCommand = new RelayCommand(CloseWindow);
            _currentpage = _serviceProvider.GetRequiredService<LandingViewVM>();
            InitializeTimer();
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
    }
}
