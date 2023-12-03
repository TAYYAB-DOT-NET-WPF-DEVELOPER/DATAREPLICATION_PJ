using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.Stores
{
    public class QueryStrings
    {
        public string DAYINFO = @"SELECT TRANSDATE , TIMESTART, TIMEEND , TRANSEND, WEATHER ,  WHOOPEN , WHOCLOSE , WHOOPENNAME , 
                                 WHOCLOSENAME, TEMPERATURE, EMPWAGES ,  NUMSHIFTS ,STORENUM ,SALESNET ,TAX1     , SALESGROSS, 
                                NUMTRANS , NUMCOUPONS, COUPONVALUE,  NUMVOIDS, VOIDVALUE, NUMCLIENTS, CLIENTSALES, POINTSAWARDED,
                                SUMGROUP1, SUMGROUP2, SUMGROUP3, SUMGROUP4, SUMGROUP5, SUMGROUP6,  SUMGROUP7,  SUMGROUP8,  
                                SUMGROUP9,  SUMGROUP10, REEXPORT, OPENDATE, SNUM, UID
                                FROM dba.DAYINFO where SNUM is not null AND TIMEEND IS NOT NULL";
    }
}
