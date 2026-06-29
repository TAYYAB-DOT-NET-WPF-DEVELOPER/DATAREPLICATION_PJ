using DataIntegration.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.Data
{
    public class ProductProcessing
    {
        // Method to process a single product from a DataRow
        public Product ProcessProduct(DataRow row)
        {
            try
            {

                return new Product
                {
                    Prodnum = Convert.ToInt32(row["Prodnum"]),
                    Descript = row["Descript"].ToString(),

                    Reportno = row["Reportno"] != DBNull.Value ? Convert.ToInt32(row["Reportno"]) : (int?)null,
                    Pricea = row["Pricea"] != DBNull.Value ? Convert.ToDouble(row["Pricea"]) : (double?)null,
                    Priceb = row["Priceb"] != DBNull.Value ? Convert.ToDouble(row["Priceb"]) : (double?)null,
                    Pricec = row["Pricec"] != DBNull.Value ? Convert.ToDouble(row["Pricec"]) : (double?)null,
                    Tax1 = row["Tax1"] != DBNull.Value ? Convert.ToInt16(row["Tax1"]) : (short?)null,
                    Tax2 = row["Tax2"] != DBNull.Value ? Convert.ToInt16(row["Tax2"]) : (short?)null,
                    Tax3 = row["Tax3"] != DBNull.Value ? Convert.ToInt16(row["Tax3"]) : (short?)null,
                    Tax4 = row["Tax4"] != DBNull.Value ? Convert.ToInt16(row["Tax4"]) : (short?)null,
                    Tax5 = row["Tax5"] != DBNull.Value ? Convert.ToInt16(row["Tax5"]) : (short?)null,
                  
                    Seclevel = row["Seclevel"] != DBNull.Value ? Convert.ToInt16(row["Seclevel"]) : (short?)null,
                    Texempt = row["Texempt"] != DBNull.Value ? Convert.ToInt16(row["Texempt"]) : (short?)null,
                    Isactive = row["Isactive"] != DBNull.Value ? Convert.ToInt16(row["Isactive"]) : (short?)null,
                    Prodtype = row["Prodtype"] != DBNull.Value ? Convert.ToInt32(row["Prodtype"]) : (int?)null,
                 
                    Question1 = row["Question1"] != DBNull.Value ? Convert.ToInt32(row["Question1"]) : (int?)null,
                    Question2 = row["Question2"] != DBNull.Value ? Convert.ToInt32(row["Question2"]) : (int?)null,
                    Question3 = row["Question3"] != DBNull.Value ? Convert.ToInt32(row["Question3"]) : (int?)null,
                    Question4 = row["Question4"] != DBNull.Value ? Convert.ToInt32(row["Question4"]) : (int?)null,
                    Question5 = row["Question5"] != DBNull.Value ? Convert.ToInt32(row["Question5"]) : (int?)null,
                    Manualprice = row["Manualprice"] != DBNull.Value ? Convert.ToInt16(row["Manualprice"]) : (short?)null,
                    Pricemode = row["Pricemode"] != DBNull.Value ? Convert.ToDouble(row["Pricemode"]) : (double?)null,
                    Updatestatus = row["Updatestatus"] != DBNull.Value ? Convert.ToInt16(row["Updatestatus"]) : (short?)null,
                    Featurecode = row["Featurecode"] != DBNull.Value ? Convert.ToSingle(row["Featurecode"]) : (float?)null,
                    Costaccountcode = row["Costaccountcode"] != DBNull.Value ? Convert.ToInt32(row["Costaccountcode"]) : (int?)null,
                    Priced = row["Priced"] != DBNull.Value ? Convert.ToDouble(row["Priced"]) : (double?)null,
                    Pricee = row["Pricee"] != DBNull.Value ? Convert.ToDouble(row["Pricee"]) : (double?)null,
                    Pricef = row["Pricef"] != DBNull.Value ? Convert.ToDouble(row["Pricef"]) : (double?)null,
                    Priceg = row["Priceg"] != DBNull.Value ? Convert.ToDouble(row["Priceg"]) : (double?)null,
                    Priceh = row["Priceh"] != DBNull.Value ? Convert.ToDouble(row["Priceh"]) : (double?)null,
                    Pricei = row["Pricei"] != DBNull.Value ? Convert.ToDouble(row["Pricei"]) : (double?)null,
                    Pricej = row["Pricej"] != DBNull.Value ? Convert.ToDouble(row["Pricej"]) : (double?)null,
                 


                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error processing product");
                return new Product();  // Return a new Product object in case of error
            }
        }
    }
}
        

