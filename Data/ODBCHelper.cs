using Serilog;
using System;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Threading;

namespace DataIntegration.Data
{
    public class ODBCHelper
    {
        private const int MaxRetries = 3; // Number of retry attempts
        private const int RetryDelayMilliseconds = 2000; // Delay between retries

        private static OdbcConnection GetCon()
        {
            return new OdbcConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
        }

        private static T ExecuteWithRetry<T>(Func<T> action)
        {
            int attempt = 0;
            while (true)
            {
                try
                {
                    return action();
                }
                catch (Exception ex) when (attempt < MaxRetries)
                {
                    attempt++;
                    Log.Information($"Attempt {attempt} failed: {ex.Message}. Retrying in {RetryDelayMilliseconds / 1000} seconds...");
                    Thread.Sleep(RetryDelayMilliseconds);
                }
                catch (Exception ex)
                {
                    Log.Information($"Operation failed after {MaxRetries} attempts: {ex.Message}");
                    throw;
                }
            }
        }

        public static DataTable SelectRec(string query)
        {
            return ExecuteWithRetry(() =>
            {
                using (OdbcConnection con = GetCon())
                {
                    con.Open();
                    OdbcDataAdapter da = new OdbcDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            });
        }

        public static DateTime GetMaxopendatetable()
        {
            return ExecuteWithRetry(() =>
            {
                string query = "SELECT max(opendate) FROM dba.dayinfo where timeend is not null";
                DataTable result = SelectRec(query);
                return (DateTime)result.Rows[0][0];
            });
        }

        public static DataTable SelectRecId(string query, params OdbcParameter[] parameters)
        {
            return ExecuteWithRetry(() =>
            {
                using (OdbcConnection con = GetCon())
                {
                    con.Open();
                    OdbcDataAdapter da = new OdbcDataAdapter(query, con);
                    da.SelectCommand.Parameters.AddRange(parameters);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            });
        }
    }
}
