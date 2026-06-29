using POS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.Data
{
    public class Memberprocessing
    {
        public class MemberProcessing
        {
            public Member ProcessMember(DataRow dr)
            {
                Member member = new Member();

                member.Memcode = Convert.ToDecimal(dr["Memcode"]);
                member.Firstname = dr["Firstname"] != DBNull.Value ? dr["Firstname"].ToString() : null;
                member.Lastname = dr["Lastname"] != DBNull.Value ? dr["Lastname"].ToString() : null;
                member.Adress1 = dr["Adress1"] != DBNull.Value ? dr["Adress1"].ToString() : null;
                member.Adress2 = dr["Adress2"] != DBNull.Value ? dr["Adress2"].ToString() : null;
                member.Hometele = dr["Hometele"] != DBNull.Value ? dr["Hometele"].ToString() : null;
                member.Lastvisit = dr["Lastvisit"] != DBNull.Value ? dr["Lastvisit"].ToString() : null;
                member.Lasttrans = dr["Lasttrans"] != DBNull.Value ? dr["Lasttrans"].ToString() : null;
                member.Lasttrans2 = dr["Lasttrans2"] != DBNull.Value ? dr["Lasttrans2"].ToString() : null;
                member.Lasttrans3 = dr["Lasttrans3"] != DBNull.Value ? dr["Lasttrans3"].ToString() : null;
                member.Lastorderdate = dr["Lastorderdate"] != DBNull.Value ? dr["Lastorderdate"].ToString() : null;
                member.Snum = Convert.ToInt32(ConfigurationManager.AppSettings["StoreId"].ToString());

                return member;
            }
        }
    }
}
