using DataIntegration.Models;
using System;
using System.Configuration;
using System.Data;

namespace DataIntegration.Data
{
    public class Dayinfoprocessing
    {
        public Dayinfo processdayinfo(DataRow dr)
        {
            Dayinfo dayinfo = new Dayinfo();

            dayinfo.Transdate = dr["TRANSDATE"] != DBNull.Value ? Convert.ToDateTime(dr["TRANSDATE"]) : DateTime.MinValue;
            dayinfo.Timestart = dr["TIMESTART"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["TIMESTART"]) : null;
            dayinfo.Timeend = dr["TIMEEND"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["TIMEEND"]) : null;
            dayinfo.Transend = dr["TRANSEND"].ToString();
            dayinfo.Weather = dr["WEATHER"].ToString();
            dayinfo.Whoopen = dr["WHOOPEN"].ToString();
            dayinfo.Whoopenname = dr["WHOOPENNAME"].ToString();
            dayinfo.Whoclose = dr["WHOCLOSE"].ToString();
            dayinfo.Whoclosename = dr["WHOCLOSENAME"].ToString();
            dayinfo.Temperature = dr["TEMPERATURE"].ToString();
            dayinfo.Empwages = dr["EMPWAGES"].ToString();
            dayinfo.Numshifts = dr["NUMSHIFTS"].ToString();

            // Handling conversion with DBNull checks
            dayinfo.Storenum = dr["STORENUM"] != DBNull.Value ? (int?)Convert.ToInt32(dr["STORENUM"]) : null;
            dayinfo.Salesnet = dr["SALESNET"] != DBNull.Value ? (float?)Convert.ToSingle(dr["SALESNET"]) : null;
            dayinfo.Tax1 = dr["TAX1"] != DBNull.Value ? (float?)Convert.ToSingle(dr["TAX1"]) : null;
            dayinfo.Salesgross = dr["SALESGROSS"] != DBNull.Value ? (float?)Convert.ToSingle(dr["SALESGROSS"]) : null;
            dayinfo.Numtrans = dr["NUMTRANS"] != DBNull.Value ? (int?)Convert.ToInt32(dr["NUMTRANS"]) : null;
            dayinfo.Numcoupons = dr["NUMCOUPONS"] != DBNull.Value ? (int?)Convert.ToInt32(dr["NUMCOUPONS"]) : null;
            dayinfo.Couponvalue = dr["COUPONVALUE"] != DBNull.Value ? (float?)Convert.ToSingle(dr["COUPONVALUE"]) : null;
            dayinfo.Numvoids = dr["NUMVOIDS"] != DBNull.Value ? (float?)Convert.ToSingle(dr["NUMVOIDS"]) : null;
            dayinfo.Voidvalue = dr["VOIDVALUE"] != DBNull.Value ? (float?)Convert.ToSingle(dr["VOIDVALUE"]) : null;
            dayinfo.Numclients = dr["NUMCLIENTS"] != DBNull.Value ? (int?)Convert.ToInt32(dr["NUMCLIENTS"]) : null;
            dayinfo.Clientsales = dr["CLIENTSALES"] != DBNull.Value ? (float?)Convert.ToSingle(dr["CLIENTSALES"]) : null;
            dayinfo.Pointsawarded = dr["POINTSAWARDED"] != DBNull.Value ? (int?)Convert.ToInt32(dr["POINTSAWARDED"]) : null;
            dayinfo.Sumgroup1 = dr["SUMGROUP1"] != DBNull.Value ? (float?)Convert.ToSingle(dr["SUMGROUP1"]) : null;
            dayinfo.Sumgroup2 = dr["SUMGROUP2"] != DBNull.Value ? (float?)Convert.ToSingle(dr["SUMGROUP2"]) : null;
            dayinfo.Sumgroup3 = dr["SUMGROUP3"] != DBNull.Value ? (float?)Convert.ToSingle(dr["SUMGROUP3"]) : null;
            dayinfo.Sumgroup4 = dr["SUMGROUP4"] != DBNull.Value ? (float?)Convert.ToSingle(dr["SUMGROUP4"]) : null;
            dayinfo.Sumgroup5 = dr["SUMGROUP5"] != DBNull.Value ? (float?)Convert.ToSingle(dr["SUMGROUP5"]) : null;
            dayinfo.Sumgroup6 = dr["SUMGROUP6"] != DBNull.Value ? (float?)Convert.ToSingle(dr["SUMGROUP6"]) : null;
            dayinfo.Sumgroup7 = dr["SUMGROUP7"] != DBNull.Value ? (float?)Convert.ToSingle(dr["SUMGROUP7"]) : null;
            dayinfo.Sumgroup8 = dr["SUMGROUP8"] != DBNull.Value ? (float?)Convert.ToSingle(dr["SUMGROUP8"]) : null;
            dayinfo.Sumgroup9 = dr["SUMGROUP9"] != DBNull.Value ? (float?)Convert.ToSingle(dr["SUMGROUP9"]) : null;
            dayinfo.Sumgroup10 = dr["SUMGROUP10"] != DBNull.Value ? (float?)Convert.ToSingle(dr["SUMGROUP10"]) : null;
            dayinfo.Uiid = Convert.ToInt32(dr["UID"]);
            dayinfo.Reexport = dr["REEXPORT"].ToString();


            dayinfo.Snum = Convert.ToInt32(ConfigurationManager.AppSettings["StoreId"].ToString());
            dayinfo.Opendate = dr["OPENDATE"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["OPENDATE"]) : null;


            return dayinfo;
        }
    }
}
