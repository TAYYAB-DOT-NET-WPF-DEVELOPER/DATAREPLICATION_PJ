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
