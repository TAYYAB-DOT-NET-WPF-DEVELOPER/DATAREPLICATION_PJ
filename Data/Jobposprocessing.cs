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
    public class Jobposprocessing
    {
        public Jobpo ProcessJobpo(DataRow dr)
        {
            Jobpo jobpo = new Jobpo();

            jobpo.Jobpos = (int)dr["Jobpos"];
            jobpo.Descript = dr["Descript"] != DBNull.Value ? dr["Descript"].ToString() : null;
            jobpo.Deptnum = dr["Deptnum"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Deptnum"])) : null;
            jobpo.Isactive = dr["Isactive"] != DBNull.Value ? dr["Isactive"].ToString() : null;
            jobpo.Storeid = Convert.ToInt32(ConfigurationManager.AppSettings["StoreId"].ToString());

            return jobpo;
        }
    }


}

