using DataIntegration.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.Data
{
    public class XcubePaymentprocessing
    {
        public XcubePayment ProcessXcubePayment(DataRow dr)
        {
            XcubePayment xcubePayment = new XcubePayment();

            xcubePayment.Snum = (int)dr["Snum"];
            xcubePayment.Opendate = Convert.ToDateTime(dr["Opendate"]);
            xcubePayment.Currency = dr["Currency"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Currency"])) : null;
            xcubePayment.Methoddesc = dr["Methoddesc"].ToString();
            xcubePayment.Methodnum = (int)dr["Methodnum"];
            xcubePayment.Tendered = dr["Tendered"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Tendered"])) : null;
            xcubePayment.Paycount = dr["Paycount"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Paycount"])) : null;
            xcubePayment.Voidedpaycount = dr["Voidedpaycount"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Voidedpaycount"])) : null;
            xcubePayment.Debit = dr["Debit"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Debit"])) : null;
            xcubePayment.Credit = dr["Credit"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Credit"])) : null;
            xcubePayment.Chargetip = dr["Chargetip"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Chargetip"])) : null;
            xcubePayment.Totaltender = dr["Totaltender"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Totaltender"])) : null;
            xcubePayment.Tipsurcharge = dr["Tipsurcharge"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Tipsurcharge"])) : null;
            xcubePayment.Tipsurchargevalue = dr["Tipsurchargevalue"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Tipsurchargevalue"])) : null;
            xcubePayment.Chargetipminustipsurcharge = dr["Chargetipminustipsurcharge"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Chargetipminustipsurcharge"])) : null;
            xcubePayment.Authreqrd = dr["Authreqrd"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Authreqrd"])) : null;

            return xcubePayment;
        }
    }
}

