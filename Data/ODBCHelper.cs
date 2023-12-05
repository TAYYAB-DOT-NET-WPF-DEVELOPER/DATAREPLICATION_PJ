using System;
using System.Configuration;
using System.Data;
using System.Data.Odbc;

namespace DataIntegration.Data
{
    public class ODBCHelper
    {
        public static OdbcConnection GetCon()
        {
            return new OdbcConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
        }

        public static DataTable SelectRec(string query)
        {
            OdbcConnection con = ODBCHelper.GetCon();
            using (con)
            {
                con.Open();
                OdbcDataAdapter da = new OdbcDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public static DateTime GetMaxopendatetable()
        {
            OdbcConnection con = ODBCHelper.GetCon();
            using (con)
            {
                string query = "SELECT max(opendate) FROM dba.dayinfo where timeend is not null";
                DataTable result = SelectRec(query);
                return (DateTime)result.Rows[0][0];
                //DateTime dt = new DateTime(2023, 10, 01);
                //return dt;
            }

        }
        public static DataTable SelectRecId(string query, params OdbcParameter[] parmerter)
        {
            OdbcConnection con = ODBCHelper.GetCon();
            using (con)
            {
                con.Open();
                OdbcDataAdapter da = new OdbcDataAdapter(query, con);
                da.SelectCommand.Parameters.AddRange(parmerter);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
