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
    internal class PromoProcessing
    {
        public Promo ProcessPromo(DataRow dr)
        {
            Promo promo = new Promo();
            try
            {
               

                promo.PromoNum = dr["PROMONUM"] != DBNull.Value ? Convert.ToInt32(dr["PROMONUM"]) : 00;
                promo.Descript = dr["DESCRIPT"].ToString();
                promo.Amount = dr["AMOUNT"] != DBNull.Value ? Convert.ToDouble(dr["AMOUNT"]) : (double?)null;

                promo.IsActive = dr["ISACTIVE"] != DBNull.Value ? Convert.ToInt16(dr["ISACTIVE"]) : (short?)null;
                promo.Percent = dr["PERCENT"] != DBNull.Value ? Convert.ToDouble(dr["PERCENT"]) : (double?)null;
                promo.IsManual = dr["ISMANUAL"] != DBNull.Value ? Convert.ToInt16(dr["ISMANUAL"]) : (short?)null;
                promo.Tax1 = dr["TAX1"] != DBNull.Value ? Convert.ToInt16(dr["TAX1"]) : (short?)null;
                promo.Tax2 = dr["TAX2"] != DBNull.Value ? Convert.ToInt16(dr["TAX2"]) : (short?)null;
                promo.Tax3 = dr["TAX3"] != DBNull.Value ? Convert.ToInt16(dr["TAX3"]) : (short?)null;
                promo.Tax4 = dr["TAX4"] != DBNull.Value ? Convert.ToInt16(dr["TAX4"]) : (short?)null;
                promo.Tax5 = dr["TAX5"] != DBNull.Value ? Convert.ToInt16(dr["TAX5"]) : (short?)null;

                promo.AmountB = dr["AMOUNTB"] != DBNull.Value ? Convert.ToDouble(dr["AMOUNTB"]) : (double?)null;
                promo.AmountC = dr["AMOUNTC"] != DBNull.Value ? Convert.ToDouble(dr["AMOUNTC"]) : (double?)null;
                promo.PercentB = dr["PERCENTB"] != DBNull.Value ? Convert.ToDouble(dr["PERCENTB"]) : (double?)null;
                promo.PercentC = dr["PERCENTC"] != DBNull.Value ? Convert.ToDouble(dr["PERCENTC"]) : (double?)null;

                promo.RangeStart = dr["RANGESTART"] != DBNull.Value ? Convert.ToDateTime(dr["RANGESTART"]) : (DateTime?)null;
                promo.RangeEnd = dr["RANGEEND"] != DBNull.Value ? Convert.ToDateTime(dr["RANGEEND"]) : (DateTime?)null;
                promo.ProdNum = dr["PRODNUM"] != DBNull.Value ? Convert.ToInt32(dr["PRODNUM"]) : (int?)null;
                promo.IsAutoProd = dr["ISAUTOPROD"] != DBNull.Value ? Convert.ToInt16(dr["ISAUTOPROD"]) : (short?)null;


                return promo;
            }
            catch(Exception ex)
            {
                Log.Information(ex.Message);
            }
            return promo;
        }
    }
}
