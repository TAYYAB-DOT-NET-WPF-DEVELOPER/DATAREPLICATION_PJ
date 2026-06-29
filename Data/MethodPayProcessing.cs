using DataIntegration.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.Data
{
    internal class MethodPayProcessing
    {
        public MethodPay ProcessMethodPay(DataRow dr)
        {
            MethodPay methodPay = new MethodPay();

            try
            {
                // MethodNum
                methodPay.MethodNum = dr["METHODNUM"] != DBNull.Value
                    ? Convert.ToInt32(dr["METHODNUM"])
                    : (int?)null;

                // Currency
                methodPay.Currency = dr["CURRENCY"] != DBNull.Value
                    ? dr["CURRENCY"].ToString()
                    : null;

                // AuthReqR
                methodPay.AuthReqR = dr["AUTHREQR"] != DBNull.Value
                    ? dr["AUTHREQR"].ToString()
                    : null;

                // IsActive
                methodPay.IsActive = dr["ISACTIVE"] != DBNull.Value
                    ? Convert.ToInt16(dr["ISACTIVE"])
                    : (short?)null;

                // Exchange
                methodPay.Exchange = dr["EXCHANGE"] != DBNull.Value
                    ? Convert.ToDouble(dr["EXCHANGE"])
                    : (double?)null; // Updated to double to match the property type

                // Descript
                methodPay.Descript = dr["DESCRIPT"] != DBNull.Value
                    ? dr["DESCRIPT"].ToString()
                    : null;

                // SecLevel
                methodPay.SecLevel = dr["SECLEVEL"] != DBNull.Value
                    ? Convert.ToInt16(dr["SECLEVEL"])
                    : (short?)null;

                // NumDecimals
                methodPay.NumDecimals = dr["NUMDECIMALS"] != DBNull.Value
                    ? Convert.ToInt32(dr["NUMDECIMALS"])
                    : (int?)null; // Updated to int to match the property type
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, rethrow it, etc.)
                // For now, just rethrowing the exception
                throw new ApplicationException("Error processing MethodPay data", ex);
            }

            return methodPay;
        }

    }
}
