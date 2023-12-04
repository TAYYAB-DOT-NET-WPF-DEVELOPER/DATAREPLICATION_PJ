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
        public Dictionary<string, int> SMLOOC = new Dictionary<string, int>
        {
            {"JT LHR", 2},
            {"MT LHR", 3},
            {"SKT CANTT", 4},
            {"GUL LHR", 5},
            {"VALENCIA", 6},
            {"DHA6 LHR", 7},
            {"Emporium Mall", 8},
            {"Packages Mall", 9},
            {"TNB", 10},
            {"Gujranwala", 11},
            {"DHA3 LHR", 12},
            {"BT LHR", 13},
            {"CANTT LHR", 15},
            {"G-7", 16},
            {"FSD KN", 17},
            {"I-8", 18},
            {"PWD", 19},
            {"FSD CL", 20},
            {"GUJRAT", 21},
            {"BHATTA LHR", 22},
            {"PWD-New", 23},
            {"G-11", 24},
            {"AIT LHR", 25},
            {"DHA5 LHR", 26},
            {"BT-P7 RWP", 27},
            {"DHA2 ISB", 28},
            {"Gujranwala 2", 29},
            {"Head Office", 0},
            {"QCC", 1}
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
