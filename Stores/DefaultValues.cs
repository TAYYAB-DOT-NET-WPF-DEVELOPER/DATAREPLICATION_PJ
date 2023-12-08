using DataIntegration.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.Stores
{
    public class DefaultValues
    {
        public Dictionary<int, string> SMLOC = new Dictionary<int, string>
        {
            { 2, "JT LHR" },
            { 3, "MT LHR" },
            { 4, "SKT CANTT" },
            { 5, "GUL LHR" },
            { 6, "VALENCIA" },
            { 7, "DHA6 LHR" },
            { 8, "Emporium Mall" },
            { 9, "Packages Mall" },
            { 10, "TNB" },
            { 11, "Gujranwala" },
            { 12, "DHA3 LHR" },
            { 13, "BT LHR" },
            { 15, "CANTT LHR" },
            { 16, "G-7" },
            { 17, "FSD KN" },
            { 18, "I-8" },
            { 19, "PWD" },
            { 20, "FSD CL" },
            { 21, "GUJRAT" },
            { 22, "BHATTA LHR" },
            { 23, "PWD-New" },
            { 24, "G-11" },
            { 25, "AIT LHR" },
            { 26, "DHA5 LHR" },
            { 27, "BT-P7 RWP" },
            { 28, "DHA2 ISB" },
            { 29, "Gujranwala 2" },
            { 999, "Hussain Chowk" },
            { 0, "Head Office" },
            { 1, "QCC" }
        };

        public int defaultdays = 1;
        public List<string> tables = new List<string> { "DAYINFO", "POSDETAIL", "POSHEADER", "POSBANK", "PUNCHPAYROLL", "STOCKTAKEDETAIL", "EMPLOYEE"};
        public ObservableCollection<DataTables> AllTables = new ObservableCollection<DataTables>();

        public DefaultValues()
        {
            InitializeTables();
        }
        private void InitializeTables()
        {
            foreach (string table in tables)
            {
                DataTables model = new DataTables
                {
                    Name = table,
                    IsChecked = true,
                };
                AllTables.Add(model);
            }

        }

}

}
