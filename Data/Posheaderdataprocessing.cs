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
    public class Posheaderdataprocessing
    {
        public Posheader posheaderdataprocessing(DataRow dr)
        {
            Posheader posheader = new Posheader();

            posheader.Transact = (int)dr["TRANSACT"];
            posheader.Timestart = dr["TIMESTART"] != DBNull.Value ? (DateTime?)dr["TIMESTART"] : null;
            posheader.Timeend = dr["TIMEEND"] != DBNull.Value ? (DateTime?)dr["TIMEEND"] : null;
            posheader.Numcust = dr["NUMCUST"] != DBNull.Value ? (int?)(short)dr["NUMCUST"] : null;
            posheader.Tax1 = dr["TAX1"] != DBNull.Value ? (float?)(Convert.ToDouble(dr["TAX1"])) : null;
            posheader.Tax2 = dr["TAX2"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["TAX2"])) : null;
            posheader.Tax1able = dr["TAX1ABLE"] != DBNull.Value ? (float?)(Convert.ToDouble(dr["TAX1ABLE"])) : null;
            posheader.Tax2able = dr["TAX2ABLE"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["TAX2ABLE"])) : null;
            posheader.Nettotal = dr["NETTOTAL"] != DBNull.Value ? (float?)(Convert.ToDouble(dr["NETTOTAL"])) : null;
            posheader.Whostart = dr["WHOSTART"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["WHOSTART"])) : null;
            posheader.Whoclose = dr["WHOCLOSE"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["WHOCLOSE"])) : null;
            posheader.Issplit = dr["ISSPLIT"] != DBNull.Value ? (int?)(short)dr["ISSPLIT"] : null;
            posheader.Saletypeindex = dr["SALETYPEINDEX"] != DBNull.Value ? (int?)dr["SALETYPEINDEX"] : null;
            posheader.Status = dr["STATUS"] != DBNull.Value ? (int?)(short)dr["STATUS"] : null;
            posheader.Finaltotal = dr["FINALTOTAL"] != DBNull.Value ? (float?)(Convert.ToDouble(dr["FINALTOTAL"])) : null;
            posheader.Storenum = dr["STORENUM"] != DBNull.Value ? (int?)dr["STORENUM"] : null;
            posheader.Opendate = dr["OPENDATE"] != DBNull.Value ? (DateTime?)dr["OPENDATE"] : null;
            posheader.Memcode = dr["MEMCODE"] != DBNull.Value ? (int?)dr["MEMCODE"] : null;
            posheader.Totalpoints = dr["TOTALPOINTS"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["TOTALPOINTS"])) : null;
            posheader.Pointsapplied = dr["POINTSAPPLIED"] != DBNull.Value ? (int?)dr["POINTSAPPLIED"] : null;
            posheader.Isdelivery = dr["ISDELIVERY"] != DBNull.Value ? (int?)(short)dr["ISDELIVERY"] : null;
            posheader.Tax1exempt = dr["TAX1EXEMPT"] != DBNull.Value ? (int?)(short)dr["TAX1EXEMPT"] : null;
            posheader.Mealtime = dr["MEALTIME"] != DBNull.Value ? (int?)(short)dr["MEALTIME"] : null;
            posheader.Isinternet = dr["ISINTERNET"] != DBNull.Value ? (int?)dr["ISINTERNET"] : null;
            posheader.Revcenter = dr["REVCENTER"] != DBNull.Value ? (int?)dr["REVCENTER"] : null;
            posheader.Numprintedfinal = dr["NUMPRINTEDFINAL"] != DBNull.Value ? (int?)(short)dr["NUMPRINTEDFINAL"] : null;
            posheader.METHODNUM = dr["METHODNUM"] != DBNull.Value ? (int?)dr["METHODNUM"] : null;
            posheader.Refid = dr["REFID"] != DBNull.Value ? dr["REFID"].ToString() : null;
            posheader.Snum = Convert.ToInt32(ConfigurationManager.AppSettings["StoreId"].ToString()); ;
            return posheader;
        }
    }
}
