using DataIntegration.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.Models
{
    public partial class DataTables : ObservableObject
    {
        private bool _ischecked;
        public string Name { get; set; }
        public bool IsChecked
        {
            get { return _ischecked; }
            set { _ischecked = value; OnPropertyChanged(nameof(IsChecked)); }
        }
    }

}
