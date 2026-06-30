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
                                 FROM dba.DAYINFO where SNUM is not null";

        public string POSHDELIVERY = @"SELECT Transact, EmpNum,    MemCode,    OpenDate,    TimeOut,    TimeIn,    PunchIndex,    DeliveryStatus   ,  UpdateStatus,    SNum,    PLink,    TripId,    QLink,    Delivered,    CommissionAmt,PromptConfirmed 
                                       FROM dba.PosHDelivery 
                                       where transact in (select transact from dba.posheader where status=3)";

        public string POSDETAIL = @"SELECT UNIQUEID, TRANSACT, PRODNUM, WHOORDER, WHOAUTH, COSTEACH, QUAN,
                                    TIMEORD, PRINTLOC, SEATNUM, MINUTES, NOTAX, HOWORDERED, STATUS,
                                    NEXTPOS, PRIORPOS, RECPOS, PRODTYPE, APPLYTAX1, APPLYTAX2, APPLYTAX3, APPLYTAX4,
                                    APPLYTAX5, REDUCEINVENTORY, STORENUM, STATNUM, RECIPECOSTEACH, OPENDATE, MEALTIME,
                                    LINEDES, REVCENTER, QUESTIONID, ORIGCOSTEACH, NETCOSTEACH, DISCOUNT,
                                    GRATEXEMPT, AUTHCODE, SNUM, SPLITPRODNUM, MASTERITEM,NETCOSTEACH,RECIPECOSTEACH
                                    FROM DBA.POSDETAIL WHERE UNIQUEID IS NOT NULL and transact in (select transact from DBA.posheader where status=3)";

        public string POSHEADER = @"SELECT 
                                    PH.TRANSACT, PH.TABLENUM, PH.TIMESTART, PH.TIMEEND, PH.NUMCUST, PH.TAX1, PH.TAX2, PH.TAX3, PH.TAX4, PH.TAX5,
                                    PH.TAX1ABLE, PH.TAX2ABLE, PH.TAX3ABLE, PH.TAX4ABLE, PH.TAX5ABLE, PH.NETTOTAL, PH.WHOSTART, PH.WHOCLOSE, PH.ISSPLIT,
                                    PH.SALETYPEINDEX, PH.EXP, PH.WAITINGAUTH, PH.STATNUM, PH.STATUS, PH.FINALTOTAL, PH.STORENUM, PH.PUNCHINDEX, 
                                    PH.GRATUITY, PH.OPENDATE, PH.MEMCODE, PH.TOTALPOINTS, PH.POINTSAPPLIED, PH.UPDATESTATUS, PH.ISDELIVERY, 
                                    PH.SCHEDULEDATE, PH.TAX1EXEMPT, PH.TAX2EXEMPT, PH.TAX3EXEMPT, PH.TAX4EXEMPT, PH.TAX5EXEMPT, PH.MEMRATE, 
                                    PH.MEALTIME, PH.ISINTERNET, PH.REVCENTER, PH.PUNCHIDXSTART, PH.STATNUMSTART, PH.SECNUM, PH.GRATAMOUNT, 
                                    PH.SHIPTO, PH.ENFORCEDGRAT, PH.NUMPRINTEDFINAL, PH.REFID, PH.RSTORDNUM, PH.EMAILADDRID, PH.LABEL, PH.SNUM,
                                    HP.METHODNUM
                                    FROM DBA.POSHEADER PH 
                                    LEFT JOIN DBA.HOWPAID HP ON PH.TRANSACT = HP.TRANSACT
                                    WHERE PH.TRANSACT IS NOT NULL AND PH.STATUS = 3";

        public string POSBANK = @"SELECT UNIQUEID, PUNCHINDEX, METHODNUM, CALCTENDERED,
                                  EXCHANGERATE, DATEENTERED, ENTRYTYPE, REFCODE,
                                  OPENDATE, WHOAUTH, PLINK, REASONID, SAFENUM, FLOATNUM, DENOMNUM,
                                  WASPROCESSED, ISACTIVE, STATNUM, MEMCODE, QLINK, UPDATESTATUS, SNUM
                                  FROM DBA.POSBANK WHERE UNIQUEID IS NOT NULL"; 

        public string PRODUCT = @"SELECT PRODNUM, DESCRIPT, REPORTNO, PRICEA, PRICEB, PRICEC, TAX1, TAX2, TAX3, TAX4, TAX5, PRINTLOC, SECLEVEL, TEXEMPT, ISACTIVE, PRODTYPE, FORCOLOR, BACKCOLOR, COUNTDOWN, MENUSCHED, BUTTON1, BUTTON2, BUTTON3, MODIFYSCR1, MODIFYSCR2, MODIFYSCR3, MODIFYSCR4, MODIFYSCR5, USEITEMCAT, QUESTION1, QUESTION2, QUESTION3, QUESTION4, QUESTION5, PRINTDES, REFCODE, ISWEIGHED, ServeTime, SPECIAL, ManualPrice, PRICEMODE, ManualRecipeCost, RecipeCost, AccountCode, MemPoints, PrintZero, UpdateStatus, FeatureCode, COSTACCOUNTCODE, PRINTCONSOLIDATE, INVACCOUNTCODE, REPGROUP, SNUM, LASTUPDATE, LASTSTORE, RANGESTART, RANGEEND, PRICED, PRICEE, PRICEF, PRICEG, PRICEH, PRICEI, PRICEJ, PLINK, CourseType, PrintPriority, TearWeight, WebUseWebModifyScr, WebModifyScr1, WebModifyScr2, WebModifyScr3, WebModifyScr4, WebModifyScr5, WebUseWebQuestion, WebQuestion1, WebQuestion2, WebQuestion3, WebQuestion4, WebQuestion5, SizeUp, SizeDown, LabelCapacity, PrepLocation, Covers, ReportProdNum, Shift1, Shift2, Shift3, Shift4, Shift5, Shift6, Shift7, Shift8, Shift9, Shift10, FontName, FontSize, FontStyle, ConfigNum, KioskModifyScr1, KioskModifyScr2, KioskModifyScr3, KioskModifyScr4, KioskModifyScr5, KioskQuestion1, KioskQuestion2, KioskQuestion3, KioskQuestion4, KioskQuestion5, KioskQuestion6, KioskQuestion7, KioskQuestion8, WebQuestion6, WebQuestion7, WebQuestion8, HoRepGroups, UnitDes, EmpPoints, GratExempt, PrepTemp, PDllName, CanCombine, Tax1Quan, Tax2Quan, Tax3Quan, Tax4Quan, Tax5Quan, PrintDes2, MemberProduct, QLink, UseGridSettings, ACExempt, Surcharge
                                  FROM DBA.Product";

        public string PROMO = @"SELECT PROMONUM, DESCRIPT, AMOUNT, SECLEVEL, ISACTIVE, PERCENT, ISMANUAL, TAX1, TAX2, TAX3, TAX4, TAX5, MEMBERONLY, AMOUNTB, AMOUNTC, PERCENTB, PERCENTC, SCHEDULE, ISAMOUNT, SwipeStart, USEALLCATS, TWOFORONE, AccountCode, POINTREQ, UpdateStatus, OneOnly, REPGROUP, SNUM, LASTUPDATE, LASTSTORE, RANGESTART, RANGEEND, PRODNUM, ISAUTOPROD, AUTOPRODNUM, AUTOPRODQUAN, MEMBEREXIST, MarketingCode, EnableOnWeb, PLINK, LongCDesc, RevCenter, CouponKind, FixedPrice, MaxAmount, MinQuan, MinCost, AutoCalc, HoRepGroups, DllName, AutoApply, RequireVoucher, CalcMethod, UseReqItems, SecLock, NoMemPoints, XValue, YValue, PromoCatID, AutoProdPrice, QLink, AppliesToGrat
                                FROM DBA.promo";

        public string METHODPAY = @"SELECT METHODNUM, CURRENCY, AUTHREQR, ISACTIVE, EXCHANGE, DESCRIPT, SecLevel, NumDecimals, SwipeStarts, AccountCode, UpdateStatus, AccountCodeChange, AskForTip, PreAuth, PrintOnRec, TipCharge, PAYIN, REPGROUP, SNUM, LASTUPDATE, LASTSTORE, PLINK, ForbidOnWeb, AccCodeOverShort, AccCodePayInOut, CurBankBalance, ShowCalculatedPayment, TenderSettlement, DllName, NoDrawer, CanRetip, NoSwipe, IsEFT, HasDenoms, Picture, HoRepGroups, AskCashBack, KeepChange, DisableExpiry, CardNumFormat, AuthSlipWithReceipt, MinValue, MaxValue, AuthSlipReprint, LCURoundUp, AskCVV, LCU, MemGiftOnly, NotInPaymtList, PromptNote, PMReportNum, AllowVoids, NotInGiftCardList, NoAuthSlip, AskSignature, CurrSymbol, NoPreAuthCheck, QLink, NumMerchPaySlip, AuthHow, MaxOnNSF, ForceTender, DispOrder
                                    FROM DBA.MethodPay";

        public string PUNCHPAYROLL = @"SELECT PUNCHINDEX, PUNCHIN, PUNCHOUT, PAYRATE, ORIGPAYRATE, JOBTYPE, EMPNUM, 
                                       OPENDATE, SHIFTINDEX, TIP, QUANVOID, VOIDSALES, OTHOURS, OTWAGE, SHIFTCOUNT, 
                                       BREAKCOUNT, MEALTAKEN, BREAKTAKEN, BREAKSCANCELLED , BREAKWAIVED, MEALWAIVED, 
                                       TOTALHOURS, BREAKUNPAIDHOURS,  BREAKPAIDHOURS,  BREAKHOURS, PAIDHOURS,
                                       REGHOURS,  REGRATE, TOTALWAGE, AVERAGEDPAYRATE, REVCENTER, NUMNOSALE, TILLBALANCE
                                       FROM DBA.PUNCHPAYROLL WHERE PUNCHINDEX IS NOT NULL";

        public string STOCKTAKEDETAIL = @"SELECT UNIQUEID, STOCKTAKENUM, INVENNUM, STARTINV, ENDINV, ROUND(POSUSED,2) AS POSUSED,
                                           SPILL, TRANSFERIN, TRANSFEROUT, OTHER, STARTDATE, ENDDATE, WHOENTER,
                                           ROUND(PURCHASED,2) AS PURCHASED, BATCHUSED, ROUND(OVERSHORT,2) AS OVERSHORT, ROUND(SHORTVALUE,2) AS SHORTVALUE,
                                           WAREHOUSENUM, REASON, SNUM,
                                           PLINK, STORETRANSFEROUT, QTYINTRANSFER, QLINK,(select max(opendate) from dba.dayinfo) as opendate
                                           FROM DBA.STOCKTAKEDETAIL WHERE UNIQUEID IS NOT NULL";

        public string EMPLOYEE = @"SELECT EMPNUM, EMPNAME, EMPLASTNAME, DATEENTERED,
                                   ADRESS1, ADRESS2, STARTWORK, ENDWORK, ISACTIVE, POSNAME,
                                   HOURLYWAGE, GENDER, SIN, SNUM
                                   FROM DBA.EMPLOYEE";

        public string MEMBER = @"select memcode , firstname , lastname , adress1, adress2, hometele,
                                 lastvisit, lasttrans, lasttrans2, lasttrans3,lastorderDate, snum 
                                 from dba.member WHERE HOMETELE IS NOT NULL";

        public string PUNCHCLOCK = @"SELECT UNIQUEID ,PUNCHIN ,PUNCHOUT, PAYRATE, JOBTYPE ,
                                     EMPNUM,STORENUM  ,OPENDATE, MEALTIME,snum FROM dba.PUNCHCLOCK where ";

        public string SALESTYPE = @"select saletypeindex,descript,isactive,snum from dba.salestype";
    }
}
