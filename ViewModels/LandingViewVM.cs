using DataIntegration.Bases;
using DataIntegration.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.ViewModels
{
    public class LandingViewVM : ObservableObject
    {
        private IServiceProvider _serviceprovider;

        private ObservableCollection<ListViewModel> _alllogs;

        public ObservableCollection<ListViewModel> Alllogs
        {
            get { return _alllogs; }
            set { _alllogs = value; OnPropertyChanged(nameof(Alllogs)); }   
        }

        public LandingViewVM(IServiceProvider serviceprovider)
        {
            _serviceprovider = serviceprovider;
            Alllogs = new ObservableCollection<ListViewModel>();
        }
    }
}
