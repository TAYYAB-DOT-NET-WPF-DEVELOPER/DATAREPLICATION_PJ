using DataIntegration.Bases;
using DataIntegration.Models;
using DataIntegration.Stores;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataIntegration.ViewModels
{
    public class SettingsVM : ObservableObject
    {
        private IServiceProvider _serviceProvider;
        private DefaultValues _defaultvalues;
        private ObservableCollection<DataTables> _alltables;
        private Visibility _daysvisibility = Visibility.Visible;
        private Visibility _datevisibility = Visibility.Collapsed;
        private bool _isdateselected;
        private bool _isdaysselected = true;
        private int _days = 1;
        private DateTime _selecteddate = DateTime.Now;

        public int Days
        {
            get { return _days; }
            set { _days = value; OnPropertyChanged(nameof(Days)); }
        }
        public DateTime SelectedDate
        {
            get { return _selecteddate; }
            set { _selecteddate = value; OnPropertyChanged(nameof(SelectedDate)); }
        }
        public bool IsDaysSelected
        {
            get { return _isdaysselected; }
            set { _isdaysselected = value; OnPropertyChanged(nameof(IsDaysSelected)); DaysVisibility = Visibility.Collapsed; DateVisibility = Visibility.Visible;}
        }
        public bool IsDateSelected
        {
            get { return _isdateselected; }
            set { _isdateselected = value; OnPropertyChanged(nameof(IsDateSelected)); DaysVisibility = Visibility.Visible; DateVisibility = Visibility.Collapsed; }
        }
        public Visibility DateVisibility
        {
            get { return _datevisibility; }
            set { _datevisibility = value; OnPropertyChanged(nameof(DateVisibility)); }
        }
        public Visibility DaysVisibility
        {
            get { return _daysvisibility; }
            set { _daysvisibility = value; OnPropertyChanged(nameof(DaysVisibility)); }
        }
        public ObservableCollection<DataTables> AllTables
        {
            get { return _alltables; }
            set { _alltables = value; OnPropertyChanged(nameof(AllTables)); }
        }

        public SettingsVM(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _defaultvalues = _serviceProvider.GetRequiredService<DefaultValues>();

            AllTables = _defaultvalues.AllTables;
            
        }
    }
}
