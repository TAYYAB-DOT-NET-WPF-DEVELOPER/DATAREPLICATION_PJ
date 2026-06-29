using DataIntegration.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.Data
{
    public class Xcubepayinoutprocessing
    {
        public XcubePayinout ProcessXcubePayinout(DataRow dr)
        {
            XcubePayinout xcubePayinout = new XcubePayinout();

            xcubePayinout.Snum = dr["Snum"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Snum"])) : null;
            xcubePayinout.Opendate = dr["Opendate"].ToString();
            xcubePayinout.Payinout = dr["Payinout"].ToString();
            xcubePayinout.Calctendered = dr["Calctendered"] != DBNull.Value ? (int?)(Convert.ToDouble(dr["Calctendered"])) : null;
            xcubePayinout.Reason = dr["Reason"].ToString();
            xcubePayinout.Refcode = dr["Refcode"].ToString();

            return xcubePayinout;
        }
    }

}

