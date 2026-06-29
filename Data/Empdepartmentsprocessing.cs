using POS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.Data
{
    public class Empdepartmentsprocessing
    {
        public Empdepartment ProcessEmpdepartment(DataRow dr)
        {
            Empdepartment empdepartment = new Empdepartment();

            empdepartment.Deptnum = Convert.ToDecimal(dr["Deptnum"]);
            empdepartment.Descript = dr["Descript"] != DBNull.Value ? dr["Descript"].ToString() : null;
            empdepartment.Isactive = dr["Isactive"] != DBNull.Value ? dr["Isactive"].ToString() : null;
            empdepartment.Storeid = Convert.ToInt32(ConfigurationManager.AppSettings["StoreId"].ToString());

            return empdepartment;
        }
    }
}

