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
    public class Punchclockprocessing
    {
        public Punchclock ProcessPunchclock(DataRow dr)
        {
            Punchclock punchclock = new Punchclock();

            punchclock.Uniqueid = (int)dr["Uniqueid"];
            punchclock.Punchin = dr["Punchin"] != DBNull.Value ? dr["Punchin"].ToString() : null;
            punchclock.Punchout = dr["Punchout"] != DBNull.Value ? dr["Punchout"].ToString() : null;
            punchclock.Payrate = dr["Payrate"] != DBNull.Value ? (float?)(Convert.ToDouble(dr["Payrate"])) : null;
            punchclock.Jobtype = dr["Jobtype"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Jobtype"])) : null;
            punchclock.Empnum = dr["Empnum"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Empnum"])) : null;
            punchclock.Storenum = dr["Storenum"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Storenum"])) : null;
            punchclock.Opendate = dr["OPENDATE"] != DBNull.Value ? (DateTime?)dr["OPENDATE"] : null;
            punchclock.Mealtime = dr["Mealtime"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Mealtime"])) : null;
            punchclock.Storeid = Convert.ToInt32(ConfigurationManager.AppSettings["StoreId"].ToString());

            return punchclock;
        }
    }
}
