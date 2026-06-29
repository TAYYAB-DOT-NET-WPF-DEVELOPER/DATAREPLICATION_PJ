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
    public class HowPaidProcessing
    {
        public HowPaid ProcessHowPaid(DataRow dr)
        {
            HowPaid howPaid = new HowPaid();
            howPaid.HowPaidLink = dr["HowPaidLink"] != DBNull.Value ? Convert.ToDecimal(dr["HowPaidLink"]) : 0;
            howPaid.TransDate = dr["TransDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["TransDate"]) : null;
            howPaid.EmpNum = dr["EmpNum"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["EmpNum"]) : null;
            howPaid.Tender = dr["Tender"] != DBNull.Value ? (float?)Convert.ToSingle(dr["Tender"]) : null;
            howPaid.MethodNum = dr["MethodNum"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["MethodNum"]) : null;
            howPaid.Change = dr["Change"] != DBNull.Value ? (float?)Convert.ToSingle(dr["Change"]) : null;
            howPaid.Authorized = dr["Authorized"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["Authorized"]) : null;
            howPaid.AuthCode = dr["AuthCode"].ToString();
            howPaid.MemCode = dr["MemCode"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["MemCode"]) : null;
            howPaid.ExchangeRate = dr["ExchangeRate"] != DBNull.Value ? (float?)Convert.ToSingle(dr["ExchangeRate"]) : null;
            howPaid.Transact = dr["Transact"] != DBNull.Value ? Convert.ToDecimal(dr["Transact"]) : 0;
            howPaid.PayType = dr["PayType"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["PayType"]) : null;
            howPaid.OpenDate = dr["OpenDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["OpenDate"]) : null;
            howPaid.PunchIndex = dr["PunchIndex"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["PunchIndex"]) : null;
            howPaid.UpdateStatus = dr["UpdateStatus"] != DBNull.Value ? Convert.ToDecimal(dr["UpdateStatus"]) : 1;
            howPaid.Settled = dr["Settled"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["Settled"]) : null;
            howPaid.Status = dr["Status"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["Status"]) : null;
            howPaid.Approved = dr["Approved"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["Approved"]) : null;
            howPaid.StatNum = dr["StatNum"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["StatNum"]) : null;
            howPaid.IsPayInOut = dr["IsPayInOut"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["IsPayInOut"]) : null;
            howPaid.PayReason = dr["PayReason"].ToString();
            howPaid.MealTime = dr["MealTime"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["MealTime"]) : null;
            howPaid.RevCenter = dr["RevCenter"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["RevCenter"]) : null;
            howPaid.Voided = dr["Voided"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["Voided"]) : null;
            howPaid.VoidedLink = dr["VoidedLink"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["VoidedLink"]) : null;
            howPaid.LcuDiff = dr["LcuDiff"] != DBNull.Value ? (float?)Convert.ToSingle(dr["LcuDiff"]) : null;
            howPaid.EnforcedGrat = dr["EnforcedGrat"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["EnforcedGrat"]) : null;
            howPaid.GratAmount = dr["GratAmount"] != DBNull.Value ? (float?)Convert.ToSingle(dr["GratAmount"]) : null;
            howPaid.OrigMethodNum = dr["OrigMethodNum"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["OrigMethodNum"]) : null;
            howPaid.CardType = dr["CardType"].ToString();
            howPaid.SNum = Convert.ToInt32(ConfigurationManager.AppSettings["StoreId"].ToString());
            //howPaid.Surcharge = dr["Surcharge"] != DBNull.Value ? (float?)Convert.ToSingle(dr["Surcharge"]) : 0;

            return howPaid;
        }
    }

}
