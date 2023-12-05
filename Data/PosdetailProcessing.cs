using DataIntegration.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.Data
{
    public class PosdetailProcessing
    {
        public Posdetail Posdetailprocessing(DataRow dr)
        {
            Posdetail posdetail = new Posdetail();

            posdetail.Uniqueid = (int)dr["UNIQUEID"];
            posdetail.Transact = (int)dr["TRANSACT"];
            posdetail.Prodnum = dr["PRODNUM"] != DBNull.Value ? (int?)dr["PRODNUM"] : null;
            posdetail.Whoorder = dr["WHOORDER"] != DBNull.Value ? (int?)dr["WHOORDER"] : null;
            posdetail.Whoauth = dr["WHOAUTH"] != DBNull.Value ? (int?)dr["WHOAUTH"] : null;
            posdetail.Costeach = dr["COSTEACH"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["COSTEACH"])) : null;
            posdetail.Quan = dr["QUAN"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["QUAN"])) : null;
            posdetail.Timeord = dr["TIMEORD"] != DBNull.Value ? (DateTime?)dr["TIMEORD"] : null;
            posdetail.Notax = dr["NOTAX"] != DBNull.Value ? (short?)dr["NOTAX"] : null;
            posdetail.Howordered = dr["HOWORDERED"] != DBNull.Value ? (short?)dr["HOWORDERED"] : null;
            posdetail.Status = dr["STATUS"] != DBNull.Value ? (int?)dr["STATUS"] : null;
            posdetail.Recpos = dr["RECPOS"] != DBNull.Value ? (short?)dr["RECPOS"] : null;
            posdetail.Prodtype = dr["PRODTYPE"] != DBNull.Value ? (int?)dr["PRODTYPE"] : null;
            posdetail.Applytax1 = dr["APPLYTAX1"] != DBNull.Value ? (short?)dr["APPLYTAX1"] : null;
            posdetail.Applytax2 = dr["APPLYTAX2"] != DBNull.Value ? (short?)dr["APPLYTAX2"] : null;
            posdetail.Reduceinventory = dr["REDUCEINVENTORY"] != DBNull.Value ? (short?)dr["REDUCEINVENTORY"] : null;
            posdetail.Storenum = dr["STORENUM"] != DBNull.Value ? (int?)dr["STORENUM"] : null;
            posdetail.Statnum = dr["STATNUM"] != DBNull.Value ? (int?)dr["STATNUM"] : null;
            posdetail.Opendate = dr["OPENDATE"] != DBNull.Value ? DateTime.TryParseExact(dr["OPENDATE"].ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate) ? (DateTime?)parsedDate : null: null;
            posdetail.Mealtime = dr["MEALTIME"] != DBNull.Value ? (short?)dr["MEALTIME"] : null;
            posdetail.Linedes = dr["LINEDES"] != DBNull.Value ? dr["LINEDES"].ToString() : null;
            posdetail.Revcenter = dr["REVCENTER"] != DBNull.Value ? (int?)dr["REVCENTER"] : null;
            posdetail.Discount = dr["DISCOUNT"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["DISCOUNT"])) : null;
            posdetail.Authcode = int.TryParse(dr["AUTHCODE"].ToString(), out int parsedAuthCode) ? (int?)parsedAuthCode : (int?)null;

            posdetail.Revcenter = dr["REVCENTER"] != DBNull.Value ? (int?)dr["REVCENTER"] : null;

            posdetail.Snum = Convert.ToInt32(ConfigurationManager.AppSettings["StoreId"].ToString());

            return posdetail;
        }
    }
}
