using DataIntegration.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.Data
{
    public class Punchpayrollprocessing
    {
        public Punchpayroll processpunchpayroll(DataRow dr, int snum)
        {
            Punchpayroll punchpayroll = new Punchpayroll();

            punchpayroll.Punchindex = (int)dr["Punchindex"];
            punchpayroll.Punchin = dr["Punchin"].ToString();
            punchpayroll.Punchout = dr["Punchout"].ToString();
            punchpayroll.Payrate = dr["Payrate"] != DBNull.Value ? (int?)Convert.ToInt32((double)dr["Payrate"]) : null;
            punchpayroll.Origpayrate = dr["Origpayrate"] != DBNull.Value ? Convert.ToInt32((double)dr["Origpayrate"]) : null; 
            punchpayroll.Jobtype = (int?)dr["Jobtype"];
            punchpayroll.Empnum = (int?)dr["Empnum"];
            punchpayroll.Opendate = dr["Opendate"].ToString();
            punchpayroll.Shiftindex = (int?)dr["Shiftindex"];
            punchpayroll.Tip = dr["Tip"] != DBNull.Value ? Convert.ToInt32((double)dr["Tip"]) : null;
            punchpayroll.Quanvoid = dr["Quanvoid"] != DBNull.Value ? Convert.ToInt32((double)dr["Quanvoid"]) : null;
            punchpayroll.Voidsales = dr["Voidsales"] != DBNull.Value ? Convert.ToInt32((double)dr["Voidsales"]) : null;
            punchpayroll.Othours = dr["Othours"] != DBNull.Value ? Convert.ToInt32((double)dr["Othours"]) : null;
            punchpayroll.Otwage = dr["Otwage"] != DBNull.Value ? Convert.ToInt32((double)dr["Otwage"]) : null;
            punchpayroll.Shiftcount = (int?)dr["Shiftcount"];
            punchpayroll.Breakcount = (int?)dr["Breakcount"];
            punchpayroll.Mealtaken = (int?)dr["Mealtaken"];
            punchpayroll.Breaktaken = (int?)dr["Breaktaken"];
            punchpayroll.Breakscancelled = (int?)dr["Breakscancelled"];
            punchpayroll.Breakwaived = (int?)dr["Breakwaived"];
            punchpayroll.Mealwaived = (int?)dr["Mealwaived"];
            punchpayroll.Totalhours = dr["Totalhours"] != DBNull.Value ? Convert.ToInt32((decimal)dr["Totalhours"]) : null;
            punchpayroll.Breakunpaidhours = dr["Breakunpaidhours"] != DBNull.Value ? Convert.ToInt32((decimal)dr["Breakunpaidhours"]) : null;
            punchpayroll.Breakpaidhours = dr["Breakpaidhours"] != DBNull.Value ? Convert.ToInt32((decimal)dr["Breakpaidhours"]) : null;
            punchpayroll.Breakhours = dr["Breakhours"] != DBNull.Value ? Convert.ToInt32((decimal)dr["Breakhours"]) : null;
            punchpayroll.Paidhours = dr["Paidhours"] != DBNull.Value ? Convert.ToInt32((decimal)dr["Paidhours"]) : null;
            punchpayroll.Reghours = dr["Reghours"] != DBNull.Value ? Convert.ToInt32((double)dr["Reghours"]) : null;
            punchpayroll.Regrate = dr["Regrate"] != DBNull.Value ? Convert.ToInt32((double)dr["Regrate"]) : null;
            punchpayroll.Totalwage = dr["Totalwage"] != DBNull.Value ? Convert.ToInt32((double)dr["Totalwage"]) : null;
            punchpayroll.Averagedpayrate = dr["Averagedpayrate"] != DBNull.Value ? Convert.ToInt32((double)dr["Averagedpayrate"]) : null;
            punchpayroll.Revcenter = (int?)dr["Revcenter"];
            punchpayroll.Numnosale = (int?)dr["Numnosale"];
            punchpayroll.Tillbalance = dr["Tillbalance"] != DBNull.Value ? Convert.ToInt32((double)dr["Tillbalance"]) : null;
            punchpayroll.Storeid = 300;
            return punchpayroll;
        }
    }
}
