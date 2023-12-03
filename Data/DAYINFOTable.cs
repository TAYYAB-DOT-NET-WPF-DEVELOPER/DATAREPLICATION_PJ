using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace DataIntegration.Data
{
    /*
    public static class DAYINFOTable
    {
        
        public static void DAYINFOS(int days)
        {
            try
            {
                var yestedaysDay = DateTime.Now.AddDays(-days);
                string olddate = yestedaysDay.ToString("yyyy-MM-dd");

                string query = @"SELECT TRANSDATE , TIMESTART, TIMEEND , TRANSEND, WEATHER ,  WHOOPEN , WHOCLOSE , WHOOPENNAME , WHOCLOSENAME, TEMPERATURE ,
                                        EMPWAGES ,  NUMSHIFTS ,STORENUM ,SALESNET ,TAX1     , SALESGROSS, NUMTRANS , NUMCOUPONS, COUPONVALUE,  NUMVOIDS ,
                                        VOIDVALUE , NUMCLIENTS ,CLIENTSALES , POINTSAWARDED,  SUMGROUP1 , SUMGROUP2 , SUMGROUP3 , SUMGROUP4, SUMGROUP5,
                                        SUMGROUP6,  SUMGROUP7,  SUMGROUP8,  SUMGROUP9,  SUMGROUP10, REEXPORT, OPENDATE, REPGROUP, SNUM  , LASTUPDATE,
                                        LASTSTORE, UID 
                                FROM dba.DAYINFO where SNUM is not null and OPENDATE >=" + "'" + olddate + "'" + ";";
                
                DataTable dt1 = ODBCHelper.SelectRec(query);
                foreach (DataRow dr in dt1.Rows)
                {
                    string TIMESTART = null;
                    string TIMEEND = null;
                    string TRANSDATE = null;
                    string OPENDATE = null;


                    if (!string.IsNullOrEmpty(dr["TIMESTART"].ToString()))
                    {
                        TIMESTART = Convert.ToDateTime(dr["TIMESTART"]).ToString("dd-MM-yyyy hh:mm:ss");
                    }
                    if (!string.IsNullOrEmpty(dr["TIMEEND"].ToString()))
                    {
                        TIMEEND = Convert.ToDateTime(dr["TIMEEND"]).ToString("dd-MM-yyyy hh:mm:ss");
                    }
                    if (!string.IsNullOrEmpty(dr["OPENDATE"].ToString()))
                    {
                        OPENDATE = Convert.ToDateTime(dr["OPENDATE"]).ToString("dd-MM-yyyy");
                    }
                    if (!string.IsNullOrEmpty(dr["TRANSDATE"].ToString()))
                    {
                        TRANSDATE = Convert.ToDateTime(dr["TRANSDATE"]).ToString("dd-MM-yyyy");
                    }

                    string DELETEQuery = @"DELETE FROM DAYINFO WHERE OPENDATE = :OPENDATE";
                    OracleHelper.CURD(DELETEQuery, new OracleParameter("opendate", OPENDATE));

                }

                DataTable dt = ODBCHelper.SelectRec(query);
                foreach (DataRow dr in dt.Rows)
                {

                    string TIMESTART = null;
                    string TIMEEND = null;
                    string TRANSDATE = null;
                    string OPENDATE = null;


                    if (!string.IsNullOrEmpty(dr["TIMESTART"].ToString()))
                    {
                        TIMESTART = Convert.ToDateTime(dr["TIMESTART"]).ToString("dd-MM-yyyy hh:mm:ss");
                    }
                    if (!string.IsNullOrEmpty(dr["TIMEEND"].ToString()))
                    {
                        TIMEEND = Convert.ToDateTime(dr["TIMEEND"]).ToString("dd-MM-yyyy hh:mm:ss");
                    }
                    if (!string.IsNullOrEmpty(dr["OPENDATE"].ToString()))
                    {
                        OPENDATE = Convert.ToDateTime(dr["OPENDATE"]).ToString("dd-MM-yyyy");
                    }
                    if (!string.IsNullOrEmpty(dr["TRANSDATE"].ToString()))
                    {
                        TRANSDATE = Convert.ToDateTime(dr["TRANSDATE"]).ToString("dd-MM-yyyy");
                    }
                    /*

                    var orcdate = yestedaysDay.ToString("dd-MMM-yyyy");
                    string checkExistQuery = @"select snum||opendate from dayinfo
                            where snum='" + dr["SNUM"].ToString().Trim() + "' and to_date(to_date(opendate,'dd-MM-yyyy'),'DD-MM-RRRR') >= '" + orcdate + "' ";
                    bool checkExist = OracleHelper.ExistRec(checkExistQuery);
                    //if (checkExist == false)
                    //{
                        string insertQuery = @"insert into dayinfo 
                                            (TRANSDATE , TIMESTART, TIMEEND , TRANSEND, WEATHER ,  WHOOPEN , WHOCLOSE , WHOOPENNAME , WHOCLOSENAME, TEMPERATURE ,
                                                EMPWAGES ,  NUMSHIFTS ,STORENUM ,SALESNET ,TAX1     , SALESGROSS, NUMTRANS , NUMCOUPONS, COUPONVALUE,  NUMVOIDS ,
                                                VOIDVALUE , NUMCLIENTS ,CLIENTSALES , POINTSAWARDED,  SUMGROUP1 , SUMGROUP2 , SUMGROUP3 , SUMGROUP4, SUMGROUP5,
                                                SUMGROUP6,  SUMGROUP7,  SUMGROUP8,  SUMGROUP9,  SUMGROUP10, REEXPORT, OPENDATE, REPGROUP, SNUM  , LASTUPDATE,
                                                LASTSTORE,  UIID ) 
                                            values 
                                            (:TRANSDATE , :TIMESTART, :TIMEEND , :TRANSEND, :WEATHER ,  :WHOOPEN , :WHOCLOSE , :WHOOPENNAME , :WHOCLOSENAME, :TEMPERATURE ,
                                                :EMPWAGES ,  :NUMSHIFTS ,:STORENUM ,:SALESNET ,:TAX1     , :SALESGROSS, :NUMTRANS , :NUMCOUPONS, :COUPONVALUE,  :NUMVOIDS ,
                                                :VOIDVALUE , :NUMCLIENTS ,:CLIENTSALES , :POINTSAWARDED,  :SUMGROUP1 , :SUMGROUP2 , :SUMGROUP3 , :SUMGROUP4, :SUMGROUP5,
                                                :SUMGROUP6,  :SUMGROUP7,  :SUMGROUP8,  :SUMGROUP9,  :SUMGROUP10, :REEXPORT, :OPENDATE, :REPGROUP, :SNUM  , :LASTUPDATE,
                                                :LASTSTORE,  :UIID )";
                    OracleHelper.CURD(insertQuery,
                        new OracleParameter("TRANSDATE", TRANSDATE),
                        new OracleParameter("TIMESTART", TIMESTART),
                        new OracleParameter("TIMEEND", TIMEEND),
                        new OracleParameter("TRANSEND", dr["TRANSEND"].ToString().Trim()),
                        new OracleParameter("WEATHER", dr["WEATHER"].ToString().Trim()),
                        new OracleParameter("WHOOPEN", dr["WHOOPEN"].ToString().Trim()),
                        new OracleParameter("WHOCLOSE", dr["WHOCLOSE"].ToString().Trim()),
                        new OracleParameter("WHOOPENNAME", dr["WHOOPENNAME"].ToString().Trim()),
                        new OracleParameter("WHOCLOSENAME", dr["WHOCLOSENAME"].ToString().Trim()),
                        new OracleParameter("TEMPERATURE", dr["TEMPERATURE"].ToString().Trim()),
                        new OracleParameter("EMPWAGES", dr["EMPWAGES"].ToString().Trim()),
                        new OracleParameter("NUMSHIFTS", dr["NUMSHIFTS"].ToString().Trim()),
                        new OracleParameter("STORENUM", dr["STORENUM"].ToString().Trim()),
                        new OracleParameter("SALESNET", dr["SALESNET"].ToString().Trim()),
                        new OracleParameter("TAX1", dr["TAX1"].ToString().Trim()),
                        new OracleParameter("SALESGROSS", dr["SALESGROSS"].ToString().Trim()),
                        new OracleParameter("NUMTRANS", dr["NUMTRANS"].ToString().Trim()),
                        new OracleParameter("NUMCOUPONS", dr["NUMCOUPONS"].ToString().Trim()),
                        new OracleParameter("COUPONVALUE", dr["COUPONVALUE"].ToString().Trim()),
                        new OracleParameter("NUMVOIDS", dr["NUMVOIDS"].ToString().Trim()),
                        new OracleParameter("VOIDVALUE", dr["VOIDVALUE"].ToString().Trim()),
                        new OracleParameter("NUMCLIENTS", dr["NUMCLIENTS"].ToString().Trim()),
                        new OracleParameter("CLIENTSALES", dr["CLIENTSALES"].ToString().Trim()),
                        new OracleParameter("POINTSAWARDED", dr["POINTSAWARDED"].ToString().Trim()),
                        new OracleParameter("SUMGROUP1", dr["SUMGROUP1"].ToString().Trim()),
                        new OracleParameter("SUMGROUP2", dr["SUMGROUP2"].ToString().Trim()),
                        new OracleParameter("SUMGROUP3", dr["SUMGROUP3"].ToString().Trim()),
                        new OracleParameter("SUMGROUP4", dr["SUMGROUP4"].ToString().Trim()),
                        new OracleParameter("SUMGROUP5", dr["SUMGROUP5"].ToString().Trim()),
                        new OracleParameter("SUMGROUP6", dr["SUMGROUP6"].ToString().Trim()),
                        new OracleParameter("SUMGROUP7", dr["SUMGROUP7"].ToString().Trim()),
                        new OracleParameter("SUMGROUP8", dr["SUMGROUP8"].ToString().Trim()),
                        new OracleParameter("SUMGROUP9", dr["SUMGROUP9"].ToString().Trim()),
                        new OracleParameter("SUMGROUP10", dr["SUMGROUP10"].ToString().Trim()),
                        new OracleParameter("REEXPORT", dr["REEXPORT"].ToString().Trim()),
                        new OracleParameter("OPENDATE", OPENDATE),
                        new OracleParameter("REPGROUP", dr["REPGROUP"].ToString().Trim()),
                        new OracleParameter("SNUM", dr["SNUM"].ToString().Trim()),
                        new OracleParameter("LASTUPDATE", dr["LASTUPDATE"].ToString().Trim()),
                        new OracleParameter("LASTSTORE", dr["LASTSTORE"].ToString().Trim()),
                        new OracleParameter("UIID", dr["UID"].ToString().Trim())


                        );

                    Console.WriteLine(dr["snum"].ToString().Trim() + "  " + dr["TRANSDATE"].ToString().Trim() + " " + dr["OPENDATE"].ToString().Trim());

                    // }


                }

                Console.WriteLine("DAYINFO data synchronized.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("DAYINFO:-  " + ex.Message);
            }

    
        }
    }
    */
}