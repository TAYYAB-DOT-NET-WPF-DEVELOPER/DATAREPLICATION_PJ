using DataIntegration.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.Data
{
    public class Stocktakedetailprocessing
    {
        public Stocktakedetail processstocktakedeatil(DataRow dr, int snum)
        {
            Stocktakedetail stocktakedetail = new Stocktakedetail();

            stocktakedetail.Uniqueid = (int)dr["Uniqueid"];
            stocktakedetail.Stocktakenum = (int?)dr["Stocktakenum"];
            stocktakedetail.Invennum = (int?)dr["Invennum"];
            stocktakedetail.Startinv = dr["Startinv"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Startinv"])) : null;
            stocktakedetail.Endinv = dr["Endinv"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Endinv"])) : null;
            stocktakedetail.Posused = dr["Posused"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Posused"])) : null;
            stocktakedetail.Spill = dr["Spill"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Spill"])) : null;
            stocktakedetail.Transferin = dr["Transferin"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Transferin"])) : null;
            stocktakedetail.Transferout = dr["Transferout"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Transferout"])) : null;
            stocktakedetail.Other = dr["Other"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Other"])) : null;
            stocktakedetail.Startdate = dr["Startdate"].ToString();
            stocktakedetail.Enddate = dr["Enddate"].ToString();
            stocktakedetail.Whoenter = dr["Whoenter"].ToString();
            stocktakedetail.Purchased = dr["Purchased"].ToString();
            stocktakedetail.Batchused = dr["Batchused"].ToString();
            stocktakedetail.Overshort = dr["Overshort"].ToString();
            stocktakedetail.Shortvalue = dr["Shortvalue"].ToString();
            stocktakedetail.Warehousenum = dr["Warehousenum"].ToString();
            stocktakedetail.Reason = dr["Reason"].ToString();
            stocktakedetail.Snum = 300;
            stocktakedetail.Plink = dr["Plink"].ToString();
            stocktakedetail.Storetransferout = dr["Storetransferout"].ToString();
            stocktakedetail.Qtyintransfer = dr["Qtyintransfer"].ToString();
            stocktakedetail.Qlink = dr["Qlink"].ToString();

            return stocktakedetail;
        }
    }
}
