using DataIntegration.Models;
using System;
using System.Data;

namespace DataIntegration.Data
{
    public class Dayinfoprocessing
    {
        public Dayinfo processdayinfo(DataRow dr, int opendate)
        {
            Dayinfo dayinfo = new Dayinfo();
            dayinfo.Transdate = !string.IsNullOrEmpty(dr["TRANSDATE"].ToString()) ? Convert.ToDateTime(dr["TIMESTART"]).ToString("dd-MM-yyyy hh:mm:ss") : null;
            dayinfo.Timestart = !string.IsNullOrEmpty(dr["TIMESTART"].ToString()) ? Convert.ToDateTime(dr["TIMESTART"]).ToString("dd-MM-yyyy hh:mm:ss") : null;
            dayinfo.Timeend = !string.IsNullOrEmpty(dr["TIMEEND"].ToString()) ? Convert.ToDateTime(dr["TIMESTART"]).ToString("dd-MM-yyyy hh:mm:ss") : null;
            dayinfo.Transend = dr["TRANSEND"].ToString();
            dayinfo.Weather = dr["WEATHER"].ToString();
            dayinfo.Whoopen = dr["WHOOPEN"].ToString();
            dayinfo.Whoopenname = dr["WHOOPENNAME"].ToString();
            dayinfo.Whoclose = dr["WHOCLOSE"].ToString();
            dayinfo.Whoclosename = dr["WHOCLOSENAME"].ToString();
            dayinfo.Temperature = dr["TEMPERATURE"].ToString();
            dayinfo.Temperature = dr["TEMPERATURE"].ToString();
            dayinfo.Empwages = dr["EMPWAGES"].ToString();
            dayinfo.Numshifts = dr["NUMSHIFTS"].ToString();
            dayinfo.Storenum = (int?)dr["STORENUM"];
            dayinfo.Salesnet = (int?)dr["SALESNET"];
            dayinfo.Tax1 = (int?)dr["TAX1"];
            dayinfo.Salesgross = (int?)dr["SALESGROSS"];
            dayinfo.Numtrans = (int?)dr["NUMTRANS"];
            dayinfo.Numcoupons = (int?)dr["NUMCOUPONS"];
            dayinfo.Couponvalue = (int?)dr["COUPONVALUE"];
            dayinfo.Numvoids = (int?)dr["NUMVOIDS"];
            dayinfo.Voidvalue = (int?)dr["VOIDVALUE"];
            dayinfo.Numclients= (int?)dr["NUMCLIENTS"];
            dayinfo.Clientsales = (int?)dr["CLIENTSALES"];
            dayinfo.Pointsawarded = (int?)dr["POINTSAWARDED"];
            dayinfo.Sumgroup1 = (int?)dr["SUMGROUP1"];
            dayinfo.Sumgroup2 = (int?)dr["SUMGROUP2"];
            dayinfo.Sumgroup3 = (int?)dr["SUMGROUP3"];
            dayinfo.Sumgroup4 = (int?)dr["SUMGROUP4"];
            dayinfo.Sumgroup5 = (int?)dr["SUMGROUP5"];
            dayinfo.Sumgroup6 = (int?)dr["SUMGROUP6"];
            dayinfo.Sumgroup7 = (int?)dr["SUMGROUP7"];
            dayinfo.Sumgroup8 = (int?)dr["SUMGROUP8"];
            dayinfo.Sumgroup9 = (int?)dr["SUMGROUP9"];
            dayinfo.Sumgroup10 = (int?)dr["SUMGROUP10"];
            dayinfo.Uiid = (int?)dr["UID"];
            dayinfo.Opendate = !string.IsNullOrEmpty(dr["OPENDATE"].ToString()) ? Convert.ToDateTime(dr["TIMESTART"]).ToString("dd-MM-yyyy") : null;
            dayinfo.Reexport = dr["REEXPORT"].ToString();
            dayinfo.Snum = opendate;

            return dayinfo;
        }
    }
}
