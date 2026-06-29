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
    public class Salestypeprocessing
    {
        public class SalestypeProcessing
        {
            public Salestype ProcessSalestype(DataRow dr)
            {
                Salestype salestype = new Salestype();

                salestype.Saletypeindex = (int)dr["Saletypeindex"];
                salestype.Descript = dr["Descript"] != DBNull.Value ? dr["Descript"].ToString() : null;
                salestype.Isactive = dr["Isactive"] != DBNull.Value ? dr["Isactive"].ToString() : null;
                salestype.Snum = Convert.ToInt32(ConfigurationManager.AppSettings["StoreId"].ToString());
                return salestype;
            }
        }
    }
}
