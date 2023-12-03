using DataIntegration.Models;
using System;
using System.Data;

namespace DataIntegration.Data
{
    public class Dayinfoprocessing
    {
        public Dayinfo processdayinfo(DataRow dr, int snum)
        {
            Dayinfo dayinfo = new Dayinfo();

            dayinfo.Transdate = !string.IsNullOrEmpty(dr["TRANSDATE"].ToString()) ? Convert.ToDateTime(dr["TRANSDATE"]).ToString("dd-MM-yyyy hh:mm:ss") : null;
            dayinfo.Timestart = !string.IsNullOrEmpty(dr["TIMESTART"].ToString()) ? Convert.ToDateTime(dr["TIMESTART"]).ToString("dd-MM-yyyy hh:mm:ss") : null;
            dayinfo.Timeend = !string.IsNullOrEmpty(dr["TIMEEND"].ToString()) ? Convert.ToDateTime(dr["TIMEEND"]).ToString("dd-MM-yyyy hh:mm:ss") : null;
            dayinfo.Transend = dr["TRANSEND"].ToString();
            dayinfo.Weather = dr["WEATHER"].ToString();
            dayinfo.Whoopen = dr["WHOOPEN"].ToString();
            dayinfo.Whoopenname = dr["WHOOPENNAME"].ToString();
            dayinfo.Whoclose = dr["WHOCLOSE"].ToString();
            dayinfo.Whoclosename = dr["WHOCLOSENAME"].ToString();
            dayinfo.Temperature = dr["TEMPERATURE"].ToString();
            dayinfo.Empwages = dr["EMPWAGES"].ToString();
            dayinfo.Numshifts = dr["NUMSHIFTS"].ToString();

            // Handling conversion from double to int?
            dayinfo.Storenum = (int?)Convert.ToInt32(dr["STORENUM"]);
            dayinfo.Salesnet = (int?)Convert.ToInt32(dr["SALESNET"]);
            dayinfo.Tax1 = (int?)Convert.ToDouble(dr["TAX1"]);
            dayinfo.Salesgross = (int?)Convert.ToDouble(dr["SALESGROSS"]);
            dayinfo.Numtrans = (int?)Convert.ToDouble(dr["NUMTRANS"]);
            dayinfo.Numcoupons = (int?)Convert.ToDouble(dr["NUMCOUPONS"]);
            dayinfo.Couponvalue = (int?)Convert.ToDouble(dr["COUPONVALUE"]);
            dayinfo.Numvoids = (int?)Convert.ToDouble(dr["NUMVOIDS"]);
            dayinfo.Voidvalue = (int?)Convert.ToDouble(dr["VOIDVALUE"]);
            dayinfo.Numclients = (int?)Convert.ToDouble(dr["NUMCLIENTS"]);
            dayinfo.Clientsales = (int?)Convert.ToDouble(dr["CLIENTSALES"]);
            dayinfo.Pointsawarded = (int?)Convert.ToDouble(dr["POINTSAWARDED"]);
            dayinfo.Sumgroup1 = (int?)Convert.ToDouble(dr["SUMGROUP1"]);
            dayinfo.Sumgroup2 = (int?)Convert.ToDouble(dr["SUMGROUP2"]);
            dayinfo.Sumgroup3 = (int?)Convert.ToDouble(dr["SUMGROUP3"]);
            dayinfo.Sumgroup4 = (int?)Convert.ToDouble(dr["SUMGROUP4"]);
            dayinfo.Sumgroup5 = (int?)Convert.ToDouble(dr["SUMGROUP5"]);
            dayinfo.Sumgroup6 = (int?)Convert.ToDouble(dr["SUMGROUP6"]);
            dayinfo.Sumgroup7 = (int?)Convert.ToDouble(dr["SUMGROUP7"]);
            dayinfo.Sumgroup8 = (int?)Convert.ToDouble(dr["SUMGROUP8"]);
            dayinfo.Sumgroup9 = (int?)Convert.ToDouble(dr["SUMGROUP9"]);
            dayinfo.Sumgroup10 = (int?)Convert.ToDouble(dr["SUMGROUP10"]);
            dayinfo.Uiid = (int?)Convert.ToInt32(dr["UID"]);
            dayinfo.Reexport = dr["REEXPORT"].ToString();


            dayinfo.Snum = 300;
            dayinfo.Opendate = !string.IsNullOrEmpty(dr["OPENDATE"].ToString()) ? Convert.ToDateTime(dr["OPENDATE"]).ToString("dd-MM-yyyy") : null;


            return dayinfo;
        }
    }
}
