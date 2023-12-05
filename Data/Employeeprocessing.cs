using DataIntegration.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.Data
{
    public class Employeeprocessing
    {
        public Employee processemployee(DataRow dr)
        {
            Employee employee = new Employee();

            employee.Empnum = (int)dr["Empnum"];
            employee.Empname = dr["Empname"].ToString();
            employee.Emplastname = dr["Emplastname"].ToString();
            employee.Dateentered = !string.IsNullOrEmpty(dr["Dateentered"].ToString()) ? dr["Dateentered"].ToString() : null;
            employee.Adress1 = dr["Adress1"].ToString();
            employee.Adress2 = dr["Adress2"].ToString();
            employee.Startwork = !string.IsNullOrEmpty(dr["Startwork"].ToString()) ? dr["Startwork"].ToString() : null;
            employee.Endwork = !string.IsNullOrEmpty(dr["Endwork"].ToString()) ? dr["Endwork"].ToString() : null;
            employee.Isactive = !string.IsNullOrEmpty(dr["Isactive"].ToString()) ? dr["Isactive"].ToString() : null;
            employee.Posname = dr["Posname"].ToString();
            employee.Hourlywage = dr["Hourlywage"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Hourlywage"])) : null;
            employee.Gender = dr["Gender"].ToString();
            employee.Storeid = Convert.ToInt32(ConfigurationManager.AppSettings["StoreId"].ToString());
            employee.Sin = dr["Sin"].ToString();
            return employee;
        }
    }
}
