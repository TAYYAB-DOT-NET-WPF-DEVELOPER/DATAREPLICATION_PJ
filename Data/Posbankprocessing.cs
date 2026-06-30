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
    public class Posbankprocessing
    {
        public Posbank processposbank(DataRow dr)
        {
            Posbank posbank = new Posbank();

            posbank.Uniqueid = (int)dr["UNIQUEID"];
            posbank.Punchindex = (int)dr["PUNCHINDEX"];
            posbank.Methodnum = (int)dr["METHODNUM"];
            posbank.Calctendered = (int?)Convert.ToDouble(dr["CALCTENDERED"]);
            posbank.Exchangerate = (int)Convert.ToDouble(dr["EXCHANGERATE"]);
            posbank.Dateentered = !string.IsNullOrEmpty(dr["DATEENTERED"].ToString()) ? Convert.ToDateTime(dr["DATEENTERED"]).ToString("yyyy-MM-dd HH:mm:ss") : null;
            posbank.Entrytype = (int)(short)dr["ENTRYTYPE"];
            posbank.Refcode = dr["RefCode"].ToString();
            posbank.Opendate = dr["OPENDATE"] != DBNull.Value ? (DateTime?)dr["OPENDATE"] : null;
            posbank.Whoauth = (int?)dr["WhoAuth"];
            posbank.Plink = dr["PLink"].ToString();
            posbank.Reasonid = dr["ReasonID"] != DBNull.Value ? (int?)dr["ReasonID"] : null;
            posbank.Safenum = (int?)dr["SafeNum"];
            posbank.Floatnum = (int?)dr["FloatNum"];
            posbank.Denomnum = (int?)dr["DenomNum"];
            posbank.Wasprocessed = (int?)dr["WasProcessed"];
            posbank.Isactive = (int)(short)dr["IsActive"];
            posbank.Statnum = (int?)dr["StatNum"];
            posbank.Memcode = dr["MemCode"]!= DBNull.Value ? (int?)dr["MemCode"] : null;
            posbank.Qlink = dr["QLink"].ToString();
            posbank.Snum = Convert.ToInt32(ConfigurationManager.AppSettings["StoreId"].ToString());

            return posbank;
        }
    }
}
