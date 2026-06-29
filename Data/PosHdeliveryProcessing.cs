using DataIntegration.Models;
using System;
using System.Configuration;
using System.Data;

namespace DataIntegration.Data
{
    public class PoshDeliveryProcessing
    {
        public PoshDelivery PoshDeliveryprocessing(DataRow dr)
        {
            PoshDelivery poshDelivery = new PoshDelivery();
            try
            {
                poshDelivery.Transact = dr["Transact"] != DBNull.Value ? Convert.ToInt32(dr["Transact"]) : 0;
                poshDelivery.EmpNum = dr["EmpNum"] != DBNull.Value ? (int?)Convert.ToInt32(dr["EmpNum"]) : null;
                poshDelivery.MemCode = dr["MemCode"] != DBNull.Value ? (int?)Convert.ToInt32(dr["MemCode"]) : null;
                poshDelivery.OpenDate = dr["OpenDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["OpenDate"]) : null;
                poshDelivery.TimeOut = dr["TimeOut"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["TimeOut"]) : null;
                poshDelivery.TimeIn = dr["TimeIn"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["TimeIn"]) : null;
                poshDelivery.PunchIndex = dr["PunchIndex"] != DBNull.Value ? (int?)Convert.ToInt32(dr["PunchIndex"]) : null;
                poshDelivery.DeliveryStatus = dr["DeliveryStatus"] != DBNull.Value ? (short?)Convert.ToInt16(dr["DeliveryStatus"]) : (short?)0; // Default to 0
                poshDelivery.RejectedReason = dr["RejectedReason"] != DBNull.Value ? dr["RejectedReason"].ToString() : null;
                poshDelivery.UpdateStatus = dr["UpdateStatus"] != DBNull.Value ? (short?)Convert.ToInt16(dr["UpdateStatus"]) : (short?)1; // Default to 1
                poshDelivery.PLink = dr["PLink"] != DBNull.Value ? dr["PLink"].ToString() : null;
                poshDelivery.TripId = dr["TripId"] != DBNull.Value ? (int?)Convert.ToInt32(dr["TripId"]) : null;
                poshDelivery.QLink = dr["QLink"] != DBNull.Value ? dr["QLink"].ToString() : null;
                poshDelivery.Delivered = dr["Delivered"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["Delivered"]) : null;
                poshDelivery.CommissionAmt = dr["CommissionAmt"] != DBNull.Value ? (double?)Convert.ToDouble(dr["CommissionAmt"]) : null;
                poshDelivery.PromptConfirmed = dr["PromptConfirmed"] != DBNull.Value ? (short?)Convert.ToInt16(dr["PromptConfirmed"]) : (short?)0; // Default to 0

                poshDelivery.SNum = Convert.ToInt32(ConfigurationManager.AppSettings["StoreId"].ToString());
            }
            catch (Exception ex)
            {
                // Log the exception details here
                Console.WriteLine($"Error processing PoshDelivery: {ex.Message}");

                // Optionally, set default values or handle the exception as needed
                // For example, you might want to set default values for the properties
                poshDelivery = new PoshDelivery(); // Reset to default or handle as needed
            }

            return poshDelivery;
        }
    }
}
