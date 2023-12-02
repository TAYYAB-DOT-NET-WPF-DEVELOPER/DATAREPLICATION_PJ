using DataIntegration.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.Models
{
    public partial class ListViewModel : ObservableObject
    {
        private string _eta;
        private string _status;
        public string? Descript { get; set; }
        public string ETA
        {
            get { return _eta; }
            set
            {
                _eta = value;
                OnPropertyChanged(nameof(ETA));
            }
        }
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
    }
}
