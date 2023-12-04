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
            punchpayroll.Payrate = (int?)dr["Payrate"];
            punchpayroll.Origpayrate = (int?)dr["Origpayrate"];
            punchpayroll.Jobtype = (int?)dr["Jobtype"];
            punchpayroll.Empnum = (int?)dr["Empnum"];
            punchpayroll.Opendate = dr["Opendate"].ToString();
            punchpayroll.Shiftindex = (int?)dr["Shiftindex"];
            punchpayroll.Tip = (int?)dr["Tip"];
            punchpayroll.Quanvoid = (int?)dr["Quanvoid"];
            punchpayroll.Voidsales = (int?)dr["Voidsales"];
            punchpayroll.Othours = (int?)dr["Othours"];
            punchpayroll.Otwage = (int?)dr["Otwage"];
            punchpayroll.Shiftcount = (int?)dr["Shiftcount"];
            punchpayroll.Breakcount = (int?)dr["Breakcount"];
            punchpayroll.Mealtaken = (int?)dr["Mealtaken"];
            punchpayroll.Breaktaken = (int?)dr["Breaktaken"];
            punchpayroll.Breakscancelled = (int?)dr["Breakscancelled"];
            punchpayroll.Breakwaived = (int?)dr["Breakwaived"];
            punchpayroll.Mealwaived = (int?)dr["Mealwaived"];
            punchpayroll.Totalhours = (int?)dr["Totalhours"];
            punchpayroll.Breakunpaidhours = (int?)dr["Breakunpaidhours"];
            punchpayroll.Breakpaidhours = (int?)dr["Breakpaidhours"];
            punchpayroll.Breakhours = (int?)dr["Breakhours"];
            punchpayroll.Paidhours = (int?)dr["Paidhours"];
            punchpayroll.Reghours = (int?)dr["Reghours"];
            punchpayroll.Regrate = (int?)dr["Regrate"];
            punchpayroll.Totalwage = (int?)dr["Totalwage"];
            punchpayroll.Averagedpayrate = (int?)dr["Averagedpayrate"];
            punchpayroll.Revcenter = (int?)dr["Revcenter"];
            punchpayroll.Numnosale = (int?)dr["Numnosale"];
            punchpayroll.Tillbalance = (int?)dr["Tillbalance"];
            punchpayroll.Storeid = (int)dr["Storeid"];
            return punchpayroll;
        }
    }
}
