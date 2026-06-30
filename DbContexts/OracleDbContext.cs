using System;
using System.Collections.Generic;
using DataIntegration.Models;
using Microsoft.EntityFrameworkCore;
using POS.Models;

namespace DataIntegration.DbContexts;

public partial class OracleDbContext : DbContext
{
    public OracleDbContext()
    {
    }

    public OracleDbContext(DbContextOptions<OracleDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Dayinfo> Dayinfos { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<Jobpo> Jobpos { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<MethodPay> MethodPays { get; set; }
    public virtual DbSet<Promo> Promos { get; set; }
    public virtual DbSet<Posbank> Posbanks { get; set; }
    public virtual DbSet<Posdetail> Posdetails { get; set; }
    public virtual DbSet<PoshDelivery> PoshDeliveries { get; set; }
    public virtual DbSet<Posheader> Posheaders { get; set; }
    public virtual DbSet<Punchclock> Punchclocks { get; set; }
    public virtual DbSet<Punchpayroll> Punchpayrolls { get; set; }
    public virtual DbSet<Salestype> Salestypes { get; set; }
    public virtual DbSet<HowPaid> HowPaids { get; set; }
    public virtual DbSet<Stocktakedetail> Stocktakedetails { get; set; }
    public virtual DbSet<XcubePayinout> XcubePayinouts { get; set; }
    public virtual DbSet<XcubePayment> XcubePayments { get; set; }
    public virtual DbSet<Empdepartment> Empdepartments { get; set; }
    public virtual DbSet<Member> Members { get; set; }
   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // When the context is created through DI (AddDbContextFactory / AddDbContext),
        // the connection string is already supplied there, so do nothing here.
        if (optionsBuilder.IsConfigured)
            return;

        // Fallback: read from environment variable first
        var connectionString = Environment.GetEnvironmentVariable("ORACLE_CONNECTION_STRING");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            // If not set, construct it from App.config appSettings
            var dataSource = System.Configuration.ConfigurationManager.AppSettings["DataSource"];
            var userId = System.Configuration.ConfigurationManager.AppSettings["UserID"];
            var password = System.Configuration.ConfigurationManager.AppSettings["Password"];

            if (!string.IsNullOrWhiteSpace(dataSource) && !string.IsNullOrWhiteSpace(userId) && !string.IsNullOrWhiteSpace(password))
            {
                connectionString = $"User Id={userId};Password={password};Data Source={dataSource};";
            }
        }

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "No connection string configured. Please set the ORACLE_CONNECTION_STRING environment variable " +
                "or configure UserID, Password, and DataSource in App.config.");
        }

        optionsBuilder.UseOracle(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("PJSALE")
            .UseCollation("USING_NLS_COMP");
        modelBuilder.Entity<Dayinfo>(entity =>
        {
            entity.HasKey(e => e.Uiid).HasName("PK_DAYINFO");
            entity.ToTable("DAY_INFO");
            entity.Property(e => e.Opendate)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("OPENDATE");
            entity.Property(e => e.Snum)
                .HasColumnType("NUMBER")
                .HasColumnName("SNUM");
            entity.Property(e => e.Clientsales)
                .HasColumnType("NUMBER")
                .HasColumnName("CLIENTSALES");
            entity.Property(e => e.Couponvalue)
                .HasColumnType("NUMBER")
                .HasColumnName("COUPONVALUE");
            entity.Property(e => e.Empwages)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMPWAGES");
            entity.Property(e => e.Numclients)
                .HasColumnType("NUMBER")
                .HasColumnName("NUMCLIENTS");
            entity.Property(e => e.Numcoupons)
                .HasColumnType("NUMBER")
                .HasColumnName("NUMCOUPONS");
            entity.Property(e => e.Numshifts)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NUMSHIFTS");
            entity.Property(e => e.Numtrans)
                .HasColumnType("NUMBER")
                .HasColumnName("NUMTRANS");
            entity.Property(e => e.Numvoids)
                .HasColumnType("NUMBER")
                .HasColumnName("NUMVOIDS");
            entity.Property(e => e.Pointsawarded)
                .HasColumnType("NUMBER")
                .HasColumnName("POINTSAWARDED");
            entity.Property(e => e.Reexport)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("REEXPORT");
            entity.Property(e => e.Salesgross)
                .HasColumnType("NUMBER")
                .HasColumnName("SALESGROSS");
            entity.Property(e => e.Salesnet)
                .HasColumnType("NUMBER")
                .HasColumnName("SALESNET");
            entity.Property(e => e.Storenum)
                .HasColumnType("NUMBER")
                .HasColumnName("STORENUM");
            entity.Property(e => e.Sumgroup1)
                .HasColumnType("NUMBER")
                .HasColumnName("SUMGROUP1");
            entity.Property(e => e.Sumgroup10)
                .HasColumnType("NUMBER")
                .HasColumnName("SUMGROUP10");
            entity.Property(e => e.Sumgroup2)
                .HasColumnType("NUMBER")
                .HasColumnName("SUMGROUP2");
            entity.Property(e => e.Sumgroup3)
                .HasColumnType("NUMBER")
                .HasColumnName("SUMGROUP3");
            entity.Property(e => e.Sumgroup4)
                .HasColumnType("NUMBER")
                .HasColumnName("SUMGROUP4");
            entity.Property(e => e.Sumgroup5)
                .HasColumnType("NUMBER")
                .HasColumnName("SUMGROUP5");
            entity.Property(e => e.Sumgroup6)
                .HasColumnType("NUMBER")
                .HasColumnName("SUMGROUP6");
            entity.Property(e => e.Sumgroup7)
                .HasColumnType("NUMBER")
                .HasColumnName("SUMGROUP7");
            entity.Property(e => e.Sumgroup8)
                .HasColumnType("NUMBER")
                .HasColumnName("SUMGROUP8");
            entity.Property(e => e.Sumgroup9)
                .HasColumnType("NUMBER")
                .HasColumnName("SUMGROUP9");
            entity.Property(e => e.Tax1)
                .HasColumnType("NUMBER")
                .HasColumnName("TAX1");
            entity.Property(e => e.Temperature)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TEMPERATURE");
            entity.Property(e => e.Timeend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TIMEEND");
            entity.Property(e => e.Timestart)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TIMESTART");
            entity.Property(e => e.Transdate)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TRANSDATE");
            entity.Property(e => e.Transend)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TRANSEND");
            entity.Property(e => e.Uiid)
                .HasColumnType("NUMBER")
                .HasColumnName("UID");
            entity.Property(e => e.Voidvalue)
                .HasColumnType("NUMBER")
                .HasColumnName("VOIDVALUE");
            entity.Property(e => e.Weather)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("WEATHER");
            entity.Property(e => e.Whoclose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("WHOCLOSE");
            entity.Property(e => e.Whoclosename)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("WHOCLOSENAME");
            entity.Property(e => e.Whoopen)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("WHOOPEN");
            entity.Property(e => e.Whoopenname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("WHOOPENNAME");
        });
        modelBuilder.Entity<HowPaid>(entity =>
        {
            entity.HasKey(e => new { e.HowPaidLink, e.SNum }).HasName("HOWPAID_PK");
            entity.ToTable("HOWPAID");
            entity.Property(e => e.HowPaidLink)
                .HasColumnType("NUMBER")
                .HasColumnName("HOWPAIDLINK");
            entity.Property(e => e.SNum)
                .HasColumnType("NUMBER")
                .HasColumnName("SNUM");
            entity.Property(e => e.TransDate)
                .HasColumnType("TIMESTAMP(6)")
                .HasColumnName("TRANSDATE");
            entity.Property(e => e.EmpNum)
                .HasColumnType("NUMBER")
                .HasColumnName("EMPNUM");
            entity.Property(e => e.Tender)
                .HasColumnType("FLOAT")
                .HasColumnName("TENDER");
            entity.Property(e => e.MethodNum)
                .HasColumnType("NUMBER")
                .HasColumnName("METHODNUM");
            entity.Property(e => e.Change)
                .HasColumnType("FLOAT")
                .HasColumnName("CHANGE");
            entity.Property(e => e.Authorized)
                .HasColumnType("NUMBER(38,0)")
                .HasColumnName("AUTHORIZED");
            entity.Property(e => e.AuthCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AUTHCODE");
            entity.Property(e => e.MemCode)
                .HasColumnType("NUMBER")
                .HasColumnName("MEMCODE");
            entity.Property(e => e.ExchangeRate)
                .HasColumnType("FLOAT")
                .HasColumnName("EXCHANGERATE");
            entity.Property(e => e.Transact)
                .HasColumnType("NUMBER")
                .HasColumnName("TRANSACT");
            entity.Property(e => e.PayType)
                .HasColumnType("NUMBER(38,0)")
                .HasColumnName("PAYTYPE");
            entity.Property(e => e.OpenDate)
                .HasColumnType("DATE")
                .HasColumnName("OPENDATE");
            entity.Property(e => e.PunchIndex)
                .HasColumnType("NUMBER")
                .HasColumnName("PUNCHINDEX");
            entity.Property(e => e.UpdateStatus)
                .HasColumnType("NUMBER(38,0)")
                .HasDefaultValue(1)
                .HasColumnName("UPDATESTATUS");
            entity.Property(e => e.Settled)
                .HasColumnType("NUMBER(38,0)")
                .HasColumnName("SETTLED");
            entity.Property(e => e.Status)
                .HasColumnType("NUMBER(38,0)")
                .HasColumnName("STATUS");

            entity.Property(e => e.Approved)
                .HasColumnType("NUMBER(38,0)")
                .HasColumnName("APPROVED");

            entity.Property(e => e.StatNum)
                .HasColumnType("NUMBER")
                .HasColumnName("STATNUM");

            entity.Property(e => e.IsPayInOut)
                .HasColumnType("NUMBER")
                .HasColumnName("ISPAYINOUT");

            entity.Property(e => e.PayReason)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PAYREASON");

            entity.Property(e => e.MealTime)
                .HasColumnType("NUMBER(38,0)")
                .HasColumnName("MEALTIME");

            entity.Property(e => e.RevCenter)
                .HasColumnType("NUMBER")
                .HasColumnName("REVCENTER");

            entity.Property(e => e.Voided)
                .HasColumnType("NUMBER(38,0)")
                .HasColumnName("VOIDED");

            entity.Property(e => e.VoidedLink)
                .HasColumnType("NUMBER")
                .HasColumnName("VOIDEDLINK");

            entity.Property(e => e.LcuDiff)
                .HasColumnType("FLOAT")
                .HasColumnName("LCUDIFF");

            entity.Property(e => e.EnforcedGrat)
                .HasColumnType("NUMBER(38,0)")
                .HasColumnName("ENFORCEDGRAT");

            entity.Property(e => e.GratAmount)
                .HasColumnType("FLOAT")
                .HasColumnName("GRATAMOUNT");

            entity.Property(e => e.OrigMethodNum)
                .HasColumnType("NUMBER")
                .HasColumnName("ORIGMETHODNUM");

            entity.Property(e => e.CardType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CARDTYPE");

           
        });
        modelBuilder.Entity<Promo>(entity =>
        {
            entity.HasKey(p => p.PromoNum).HasName("PROMO_PK");

            entity.ToTable("PROMO");

            entity.Property(p => p.PromoNum)
                .HasColumnType("integer")
                .HasColumnName("PROMONUM");

            entity.Property(p => p.Descript)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DESCRIPT");

            entity.Property(p => p.Amount)
                .HasColumnType("float")
                .HasColumnName("AMOUNT");

            entity.Property(p => p.SecLevel)
                .HasColumnType("smallint")
                .HasColumnName("SECLEVEL");

            entity.Property(p => p.IsActive)
                .HasColumnType("smallint")
                .HasColumnName("ISACTIVE");

            entity.Property(p => p.Percent)
                .HasColumnType("float")
                .HasColumnName("PERCENT");

            entity.Property(p => p.IsManual)
                .HasColumnType("smallint")
                .HasColumnName("ISMANUAL");

            entity.Property(p => p.Tax1)
                .HasColumnType("smallint")
                .HasColumnName("TAX1");

            entity.Property(p => p.Tax2)
                .HasColumnType("smallint")
                .HasColumnName("TAX2");

            entity.Property(p => p.Tax3)
                .HasColumnType("smallint")
                .HasColumnName("TAX3");

            entity.Property(p => p.Tax4)
                .HasColumnType("smallint")
                .HasColumnName("TAX4");

            entity.Property(p => p.Tax5)
                .HasColumnType("smallint")
                .HasColumnName("TAX5");

            entity.Property(p => p.MemberOnly)
                .HasColumnType("smallint")
                .HasColumnName("MEMBERONLY");

            entity.Property(p => p.AmountB)
                .HasColumnType("float")
                .HasColumnName("AMOUNTB");

            entity.Property(p => p.AmountC)
                .HasColumnType("float")
                .HasColumnName("AMOUNTC");

            entity.Property(p => p.PercentB)
                .HasColumnType("float")
                .HasColumnName("PERCENTB");

            entity.Property(p => p.PercentC)
                .HasColumnType("float")
                .HasColumnName("PERCENTC");

            entity.Property(p => p.RangeStart)
                .HasColumnType("date")
                .HasColumnName("RANGESTART");

            entity.Property(p => p.RangeEnd)
                .HasColumnType("date")
                .HasColumnName("RANGEEND");

            entity.Property(p => p.ProdNum)
                .HasColumnType("integer")
                .HasColumnName("PRODNUM");

            entity.Property(p => p.IsAutoProd)
                .HasColumnType("smallint")
                .HasColumnName("ISAUTOPROD");
        });


        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => new { e.Empnum, e.Storeid }).HasName("EMPLOYEE_PK");

            entity.ToTable("EMPLOYEE");

            entity.Property(e => e.Empnum)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("EMPNUM");
            entity.Property(e => e.Storeid)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("STOREID");
            entity.Property(e => e.Adress1)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("ADDRESS1");
            entity.Property(e => e.Adress2)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("ADDRESS2");
            entity.Property(e => e.Dateentered)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DATEENTERED");
            entity.Property(e => e.Emplastname)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("EMPLASTNAME");
            entity.Property(e => e.Empname)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("EMPNAME");
            entity.Property(e => e.Empposition)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("EMPPOSITION");
            entity.Property(e => e.Endwork)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ENDWORK");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("GENDER");
            entity.Property(e => e.Horepgroups)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("HOREPGROUPS");
            entity.Property(e => e.Hourlywage)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("HOURLYWAGE");
            entity.Property(e => e.Isactive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Posname)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("POSNAME");
            entity.Property(e => e.Sin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SIN");
            entity.Property(e => e.Startwork)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("STARTWORK");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => new { p.Prodnum }).HasName("PRODUCT_PK");

            entity.ToTable("PRODUCT");

            entity.Property(p => p.Prodnum)
                .HasColumnType("integer")
                .HasColumnName("PRODNUM");

            entity.Property(p => p.Descript)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("DESCRIPT");


            entity.Property(p => p.Reportno)
                .HasColumnType("integer")
                .HasColumnName("REPORTNO");

            entity.Property(p => p.Pricea)
                .HasColumnType("double")
                .HasColumnName("PRICEA");

            entity.Property(p => p.Priceb)
                .HasColumnType("double")
                .HasColumnName("PRICEB");

            entity.Property(p => p.Pricec)
                .HasColumnType("double")
                .HasColumnName("PRICEC");

            entity.Property(p => p.Tax1)
                .HasColumnType("smallint")
                .HasColumnName("TAX1");

            entity.Property(p => p.Tax2)
                .HasColumnType("smallint")
                .HasColumnName("TAX2");

            entity.Property(p => p.Tax3)
                .HasColumnType("smallint")
                .HasColumnName("TAX3");

            entity.Property(p => p.Tax4)
                .HasColumnType("smallint")
                .HasColumnName("TAX4");

            entity.Property(p => p.Tax5)
                .HasColumnType("smallint")
                .HasColumnName("TAX5");


            entity.Property(p => p.Seclevel)
                .HasColumnType("smallint")
                .HasColumnName("SECLEVEL");

            entity.Property(p => p.Texempt)
                .HasColumnType("smallint")
                .HasColumnName("TEXEMPT");

            entity.Property(p => p.Isactive)
                .HasColumnType("smallint")
                .HasColumnName("ISACTIVE");

            entity.Property(p => p.Prodtype)
                .HasColumnType("integer")
                .HasColumnName("PRODTYPE");

       

          

            entity.Property(p => p.Question1)
                .HasColumnType("integer")
                .HasColumnName("QUESTION1");

            entity.Property(p => p.Question2)
                .HasColumnType("integer")
                .HasColumnName("QUESTION2");

            entity.Property(p => p.Question3)
                .HasColumnType("integer")
                .HasColumnName("QUESTION3");

            entity.Property(p => p.Question4)
                .HasColumnType("integer")
                .HasColumnName("QUESTION4");

            entity.Property(p => p.Question5)
                .HasColumnType("integer")
                .HasColumnName("QUESTION5");


            entity.Property(p => p.Manualprice)
                .HasColumnType("smallint")
                .HasColumnName("MANUALPRICE");

            entity.Property(p => p.Pricemode)
                .HasColumnType("double")
                .HasColumnName("PRICEMODE");


          


            entity.Property(p => p.Updatestatus)
                .HasColumnType("smallint")
                .HasColumnName("UPDATESTATUS");

            entity.Property(p => p.Featurecode)
                .HasColumnType("float")
                .HasColumnName("FEATURECODE");

            entity.Property(p => p.Costaccountcode)
                .HasColumnType("integer")
                .HasColumnName("COSTACCOUNTCODE");

          
            entity.Property(p => p.Priced)
                .HasColumnType("double")
                .HasColumnName("PRICED");

            entity.Property(p => p.Pricee)
                .HasColumnType("double")
                .HasColumnName("PRICEE");

            entity.Property(p => p.Pricef)
                .HasColumnType("double")
                .HasColumnName("PRICEF");

            entity.Property(p => p.Priceg)
                .HasColumnType("double")
                .HasColumnName("PRICEG");

            entity.Property(p => p.Priceh)
                .HasColumnType("double")
                .HasColumnName("PRICEH");

            entity.Property(p => p.Pricei)
                .HasColumnType("double")
                .HasColumnName("PRICEI");

            entity.Property(p => p.Pricej)
                .HasColumnType("double")
                .HasColumnName("PRICEJ");

          
        

            // Define additional configurations here, if any
        });
        modelBuilder.Entity<MethodPay>(entity =>
        {
            entity.HasKey(m => m.MethodNum).HasName("METHODPAY_PK");

            entity.ToTable("METHODPAY");

            entity.Property(m => m.MethodNum)
                .HasColumnType("integer")
                .HasColumnName("METHODNUM");

            entity.Property(m => m.Currency)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CURRENCY");

            entity.Property(m => m.AuthReqR)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("AUTHREQR");

            entity.Property(m => m.IsActive)
                .HasColumnType("smallint")
                .HasColumnName("ISACTIVE");

            entity.Property(m => m.Exchange)
                .HasColumnType("float")
                .HasColumnName("EXCHANGE");

            entity.Property(m => m.Descript)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DESCRIPT");

            entity.Property(m => m.SecLevel)
                .HasColumnType("smallint")
                .HasColumnName("SECLEVEL");

            entity.Property(m => m.NumDecimals)
                .HasColumnType("integer")
                .HasColumnName("NUMDECIMALS");
        });

        modelBuilder.Entity<PoshDelivery>(entity =>
        {
            entity.HasKey(e => new { e.Transact, e.SNum, e.OpenDate }).HasName("POSHDELIVERY_PK");

            entity.ToTable("POSHDELIVERY_LIVE");

            entity.Property(e => e.Transact)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TRANSACT");
            entity.Property(e => e.EmpNum)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EMPNUM");
            entity.Property(e => e.MemCode)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("MEMCODE");
            entity.Property(e => e.OpenDate)
                .HasColumnType("DATE")
                .HasColumnName("OPENDATE");
            entity.Property(e => e.TimeOut)
                .HasColumnType("TIMESTAMP(6)")
                .HasColumnName("TIMEOUT");
            entity.Property(e => e.TimeIn)
                .HasColumnType("TIMESTAMP(6)")
                .HasColumnName("TIMEIN");
            entity.Property(e => e.PunchIndex)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PUNCHINDEX");
            entity.Property(e => e.DeliveryStatus)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("DELIVERYSTATUS");
           
            entity.Property(e => e.UpdateStatus)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("UPDATESTATUS");
            entity.Property(e => e.SNum)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SNUM");
            entity.Property(e => e.PLink)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PLINK");
            entity.Property(e => e.TripId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TRIPID");
            entity.Property(e => e.QLink)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("QLINK");
            entity.Property(e => e.Delivered)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DELIVERED");
        });


        modelBuilder.Entity<Jobpo>(entity =>
        {
            entity.HasKey(e => new { e.Jobpos, e.Storeid }).HasName("JOBPOS_PK");

            entity.ToTable("JOBPOS");

            entity.Property(e => e.Jobpos)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("JOBPOS");
            entity.Property(e => e.Storeid)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("STOREID");
            entity.Property(e => e.Deptnum)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("DEPTNUM");
            entity.Property(e => e.Descript)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("DESCRIPT");
            entity.Property(e => e.Isactive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("ISACTIVE");
        });

        modelBuilder.Entity<Posbank>(entity =>
        {
            entity.HasKey(e => new { e.Uniqueid, e.Snum }).HasName("POSBANK_PK");

            entity.ToTable("POSBANK");

            entity.Property(e => e.Uniqueid)
                .HasColumnType("NUMBER")
                .HasColumnName("UNIQUEID");
            entity.Property(e => e.Snum)
                .HasColumnType("NUMBER")
                .HasColumnName("SNUM");
            entity.Property(e => e.Calctendered)
                .HasColumnType("NUMBER")
                .HasColumnName("CALCTENDERED");
            entity.Property(e => e.CloudReflectionupdate)
                .HasColumnType("NUMBER")
                .HasColumnName("CLOUD_REFLECTIONUPDATE");
            entity.Property(e => e.Dateentered)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DATEENTERED");
            entity.Property(e => e.Denomnum)
                .HasColumnType("NUMBER")
                .HasColumnName("DENOMNUM");
            entity.Property(e => e.Entrytype)
                .HasColumnType("NUMBER")
                .HasColumnName("ENTRYTYPE");
            entity.Property(e => e.Exchangerate)
                .HasColumnType("NUMBER")
                .HasColumnName("EXCHANGERATE");
            entity.Property(e => e.Floatnum)
                .HasColumnType("NUMBER")
                .HasColumnName("FLOATNUM");
            entity.Property(e => e.Isactive)
                .HasColumnType("NUMBER")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Memcode)
                .HasColumnType("NUMBER")
                .HasColumnName("MEMCODE");
            entity.Property(e => e.Methodnum)
                .HasColumnType("NUMBER")
                .HasColumnName("METHODNUM");
            entity.Property(e => e.Opendate)
                .HasColumnType("DATE")
                .HasColumnName("OPENDATE");
            entity.Property(e => e.Plink)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("PLINK");
            entity.Property(e => e.Punchindex)
                .HasColumnType("NUMBER")
                .HasColumnName("PUNCHINDEX");
            entity.Property(e => e.Qlink)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("QLINK");
            entity.Property(e => e.Reasonid)
                .HasColumnType("NUMBER")
                .HasColumnName("REASONID");
            entity.Property(e => e.Refcode)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("REFCODE");
            entity.Property(e => e.Safenum)
                .HasColumnType("NUMBER")
                .HasColumnName("SAFENUM");
            entity.Property(e => e.Statnum)
                .HasColumnType("NUMBER")
                .HasColumnName("STATNUM");
            entity.Property(e => e.Updatestatus)
                .HasColumnType("NUMBER")
                .HasColumnName("UPDATESTATUS");
            entity.Property(e => e.Wasprocessed)
                .HasColumnType("NUMBER")
                .HasColumnName("WASPROCESSED");
            entity.Property(e => e.Whoauth)
                .HasColumnType("NUMBER")
                .HasColumnName("WHOAUTH");
        });

        modelBuilder.Entity<Posdetail>(entity =>
        {
            entity.HasKey(e => new { e.Uniqueid, e.Transact, e.Snum, e.Opendate }).HasName("POSDETAIL_PK");

            entity.ToTable("POSDETAIL");

            entity.Property(e => e.Uniqueid)
                .HasColumnType("NUMBER")
                .HasColumnName("UNIQUEID");
            entity.Property(e => e.Snum)
                .HasColumnType("NUMBER")
                .HasColumnName("SNUM");
            entity.Property(e => e.Applytax1)
                .HasColumnType("NUMBER")
                .HasColumnName("APPLYTAX1");
            entity.Property(e => e.Applytax2)
                .HasColumnType("NUMBER")
                .HasColumnName("APPLYTAX2");
            entity.Property(e => e.Authcode)
                .HasColumnType("NUMBER")
                .HasColumnName("AUTHCODE");
            entity.Property(e => e.Costeach)
                .HasColumnType("NUMBER")
                .HasColumnName("COSTEACH");
            entity.Property(e => e.Discount)
                .HasColumnType("NUMBER")
                .HasColumnName("DISCOUNT");
            entity.Property(e => e.Howordered)
                .HasColumnType("NUMBER")
                .HasColumnName("HOWORDERED");
            entity.Property(e => e.Istestdata)
                .HasColumnType("NUMBER")
                .HasColumnName("ISTESTDATA");
            entity.Property(e => e.Linedes)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LINEDES");
            entity.Property(e => e.Mealtime)
                .HasColumnType("NUMBER")
                .HasColumnName("MEALTIME");
            entity.Property(e => e.Notax)
                .HasColumnType("NUMBER")
                .HasColumnName("NOTAX");
            entity.Property(e => e.Opendate)
                .HasColumnType("DATE")
                .HasColumnName("OPENDATE");
            entity.Property(e => e.Prodnum)
                .HasColumnType("NUMBER")
                .HasColumnName("PRODNUM");
            entity.Property(e => e.Prodtype)
                .HasColumnType("NUMBER")
                .HasColumnName("PRODTYPE");
            entity.Property(e => e.Quan)
                .HasColumnType("NUMBER")
                .HasColumnName("QUAN");
            entity.Property(e => e.Recpos)
                .HasColumnType("NUMBER")
                .HasColumnName("RECPOS");
            entity.Property(e => e.Reduceinventory)
                .HasColumnType("NUMBER")
                .HasColumnName("REDUCEINVENTORY");
            entity.Property(e => e.Repgroup)
                .HasColumnType("NUMBER")
                .HasColumnName("REPGROUP");
            entity.Property(e => e.Revcenter)
                .HasColumnType("NUMBER")
                .HasColumnName("REVCENTER");
            entity.Property(e => e.Statnum)
                .HasColumnType("NUMBER")
                .HasColumnName("STATNUM");
            entity.Property(e => e.Status)
                .HasColumnType("NUMBER")
                .HasColumnName("STATUS");
            entity.Property(e => e.Storenum)
                .HasColumnType("NUMBER")
                .HasColumnName("STORENUM");
            entity.Property(e => e.Timeord)
                .HasColumnType("DATE")
                .HasColumnName("TIMEORD");
            entity.Property(e => e.Transact)
                .HasColumnType("NUMBER")
                .HasColumnName("TRANSACT");
            entity.Property(e => e.Whoauth)
                .HasColumnType("NUMBER")
                .HasColumnName("WHOAUTH");
            entity.Property(e => e.Whoorder)
                .HasColumnType("NUMBER")
                .HasColumnName("WHOORDER");
            entity.Property(e => e.Masteritem)
                .HasColumnType("NUMBER")
                .HasColumnName("MASTERITEM");
            entity.Property(e => e.Netcosteach)
                .HasColumnType("NUMBER")
                .HasColumnName("NETCOSTEACH");
            entity.Property(e => e.Recipecosteach)
                .HasColumnType("NUMBER")
                .HasColumnName("RECIPECOSTEACH");
        });

        modelBuilder.Entity<Posheader>(entity =>
        {
            entity.HasKey(e => new { e.Transact, e.Snum, e.Opendate }).HasName("POSHEADER_PK");

            entity.ToTable("POSHEADER");

            entity.Property(e => e.Snum)
                .HasColumnType("NUMBER")
                .HasColumnName("SNUM");
            entity.Property(e => e.Transact)
                .HasColumnType("NUMBER")
                .HasColumnName("TRANSACT");
            entity.Property(e => e.Finaltotal)
                .HasColumnType("NUMBER")
                .HasColumnName("FINALTOTAL");
            entity.Property(e => e.Isdelivery)
                .HasColumnType("NUMBER")
                .HasColumnName("ISDELIVERY");
            entity.Property(e => e.Isinternet)
                .HasColumnType("NUMBER")
                .HasColumnName("ISINTERNET");
            entity.Property(e => e.Issplit)
                .HasColumnType("NUMBER")
                .HasColumnName("ISSPLIT");
            entity.Property(e => e.Mealtime)
                .HasColumnType("NUMBER")
                .HasColumnName("MEALTIME");
            entity.Property(e => e.Memcode)
                .HasColumnType("NUMBER")
                .HasColumnName("MEMCODE");
            entity.Property(e => e.Nettotal)
                .HasColumnType("NUMBER")
                .HasColumnName("NETTOTAL");
            entity.Property(e => e.Numcust)
                .HasColumnType("NUMBER")
                .HasColumnName("NUMCUST");
            entity.Property(e => e.Numprintedfinal)
                .HasColumnType("NUMBER")
                .HasColumnName("NUMPRINTEDFINAL");
            entity.Property(e => e.Opdate)
                .HasColumnType("DATE")
                .HasColumnName("OPDATE");
            entity.Property(e => e.Opendate)
                .HasColumnType("DATE")
                .HasColumnName("OPENDATE");
            entity.Property(e => e.Pointsapplied)
                .HasColumnType("NUMBER")
                .HasColumnName("POINTSAPPLIED");
            entity.Property(e => e.Refid)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("REFID");
            entity.Property(e => e.Revcenter)
                .HasColumnType("NUMBER")
                .HasColumnName("REVCENTER");
            entity.Property(e => e.Saletypeindex)
                .HasColumnType("NUMBER")
                .HasColumnName("SALETYPEINDEX");
            entity.Property(e => e.Status)
                .HasColumnType("NUMBER")
                .HasColumnName("STATUS");
            entity.Property(e => e.Storenum)
                .HasColumnType("NUMBER")
                .HasColumnName("STORENUM");
            entity.Property(e => e.Sts)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("STS");
            entity.Property(e => e.Tax1)
                .HasColumnType("NUMBER")
                .HasColumnName("TAX1");
            entity.Property(e => e.Tax1able)
                .HasColumnType("NUMBER")
                .HasColumnName("TAX1ABLE");
            entity.Property(e => e.Tax1exempt)
                .HasColumnType("NUMBER")
                .HasColumnName("TAX1EXEMPT");
            entity.Property(e => e.Tax2)
                .HasColumnType("NUMBER")
                .HasColumnName("TAX2");
            entity.Property(e => e.Tax2able)
                .HasColumnType("NUMBER")
                .HasColumnName("TAX2ABLE");
            entity.Property(e => e.Tend)
                .HasColumnType("DATE")
                .HasColumnName("TEND");
            entity.Property(e => e.Timeend)
                .HasColumnType("DATE")
                .HasColumnName("TIMEEND");
            entity.Property(e => e.Timestart)
                .HasColumnType("DATE")
                .HasColumnName("TIMESTART");
            entity.Property(e => e.Totalpoints)
                .HasColumnType("NUMBER")
                .HasColumnName("TOTALPOINTS");
            entity.Property(e => e.Tstart)
                .HasColumnType("DATE")
                .HasColumnName("TSTART");
            entity.Property(e => e.Whoclose)
                .HasColumnType("NUMBER")
                .HasColumnName("WHOCLOSE");
            entity.Property(e => e.Whostart)
                .HasColumnType("NUMBER")
                .HasColumnName("WHOSTART");
        });

        modelBuilder.Entity<Punchclock>(entity =>
        {
            entity.HasKey(e => new { e.Uniqueid, e.Storeid }).HasName("PUNCHCLOCK_PK");

            entity.ToTable("PUNCHCLOCK");

            entity.Property(e => e.Uniqueid)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("UNIQUEID");
            entity.Property(e => e.Storeid)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("STOREID");
            entity.Property(e => e.Empnum)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("EMPNUM");
            entity.Property(e => e.Jobtype)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("JOBTYPE");
            entity.Property(e => e.Mealtime)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("MEALTIME");
            entity.Property(e => e.Opendate)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("OPENDATE");
            entity.Property(e => e.Payrate)
                .HasColumnType("NUMBER(20,4)")
                .HasColumnName("PAYRATE");
            entity.Property(e => e.Punchin)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PUNCHIN");
            entity.Property(e => e.Punchout)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PUNCHOUT");
            entity.Property(e => e.Storenum)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("STORENUM");
        });

        modelBuilder.Entity<Punchpayroll>(entity =>
        {
            entity.HasKey(e => new { e.Punchindex, e.Storeid }).HasName("PUNCHPAYROLL_PK");

            entity.ToTable("PUNCHPAYROLL");

            entity.Property(e => e.Punchindex)
                .HasColumnType("NUMBER")
                .HasColumnName("PUNCHINDEX");
            entity.Property(e => e.Storeid)
                .HasColumnType("NUMBER")
                .HasColumnName("STOREID");
            entity.Property(e => e.Averagedpayrate)
                .HasColumnType("NUMBER")
                .HasColumnName("AVERAGEDPAYRATE");
            entity.Property(e => e.Breakcount)
                .HasColumnType("NUMBER")
                .HasColumnName("BREAKCOUNT");
            entity.Property(e => e.Breakhours)
                .HasColumnType("NUMBER")
                .HasColumnName("BREAKHOURS");
            entity.Property(e => e.Breakpaidhours)
                .HasColumnType("NUMBER")
                .HasColumnName("BREAKPAIDHOURS");
            entity.Property(e => e.Breakscancelled)
                .HasColumnType("NUMBER")
                .HasColumnName("BREAKSCANCELLED");
            entity.Property(e => e.Breaktaken)
                .HasColumnType("NUMBER")
                .HasColumnName("BREAKTAKEN");
            entity.Property(e => e.Breakunpaidhours)
                .HasColumnType("NUMBER")
                .HasColumnName("BREAKUNPAIDHOURS");
            entity.Property(e => e.Breakwaived)
                .HasColumnType("NUMBER")
                .HasColumnName("BREAKWAIVED");
            entity.Property(e => e.Empnum)
                .HasColumnType("NUMBER")
                .HasColumnName("EMPNUM");
            entity.Property(e => e.Jobtype)
                .HasColumnType("NUMBER")
                .HasColumnName("JOBTYPE");
            entity.Property(e => e.Mealtaken)
                .HasColumnType("NUMBER")
                .HasColumnName("MEALTAKEN");
            entity.Property(e => e.Mealwaived)
                .HasColumnType("NUMBER")
                .HasColumnName("MEALWAIVED");
            entity.Property(e => e.Numnosale)
                .HasColumnType("NUMBER")
                .HasColumnName("NUMNOSALE");
            entity.Property(e => e.Opendate)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("OPENDATE");
            entity.Property(e => e.Origpayrate)
                .HasColumnType("NUMBER")
                .HasColumnName("ORIGPAYRATE");
            entity.Property(e => e.Othours)
                .HasColumnType("NUMBER")
                .HasColumnName("OTHOURS");
            entity.Property(e => e.Otwage)
                .HasColumnType("NUMBER")
                .HasColumnName("OTWAGE");
            entity.Property(e => e.Paidhours)
                .HasColumnType("NUMBER")
                .HasColumnName("PAIDHOURS");
            entity.Property(e => e.Payrate)
                .HasColumnType("NUMBER")
                .HasColumnName("PAYRATE");
            entity.Property(e => e.Punchin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PUNCHIN");
            entity.Property(e => e.Punchout)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PUNCHOUT");
            entity.Property(e => e.Quanvoid)
                .HasColumnType("NUMBER")
                .HasColumnName("QUANVOID");
            entity.Property(e => e.Reghours)
                .HasColumnType("NUMBER")
                .HasColumnName("REGHOURS");
            entity.Property(e => e.Regrate)
                .HasColumnType("NUMBER")
                .HasColumnName("REGRATE");
            entity.Property(e => e.Revcenter)
                .HasColumnType("NUMBER")
                .HasColumnName("REVCENTER");
            entity.Property(e => e.Shiftcount)
                .HasColumnType("NUMBER")
                .HasColumnName("SHIFTCOUNT");
            entity.Property(e => e.Shiftindex)
                .HasColumnType("NUMBER")
                .HasColumnName("SHIFTINDEX");
            entity.Property(e => e.Tillbalance)
                .HasColumnType("NUMBER")
                .HasColumnName("TILLBALANCE");
            entity.Property(e => e.Tip)
                .HasColumnType("NUMBER")
                .HasColumnName("TIP");
            entity.Property(e => e.Totalhours)
                .HasColumnType("NUMBER")
                .HasColumnName("TOTALHOURS");
            entity.Property(e => e.Totalwage)
                .HasColumnType("NUMBER")
                .HasColumnName("TOTALWAGE");
            entity.Property(e => e.Voidsales)
                .HasColumnType("NUMBER")
                .HasColumnName("VOIDSALES");
        });

        modelBuilder.Entity<Salestype>(entity =>
        {
            entity.HasKey(e => e.Saletypeindex).HasName("SALESTYPE_PK");

            entity.ToTable("SALESTYPE");

            entity.Property(e => e.Saletypeindex)
                .HasColumnType("NUMBER")
                .HasColumnName("SALETYPEINDEX");
            entity.Property(e => e.Descript)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("DESCRIPT");
            entity.Property(e => e.Isactive)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Snum)
                .HasColumnType("NUMBER")
                .HasColumnName("SNUM");
        });

        modelBuilder.Entity<Stocktakedetail>(entity =>
        {
            entity.HasKey(e => new { e.Uniqueid, e.Snum }).HasName("STOCKTAKEDETAIL_PK");

            entity.ToTable("STOCKTAKEDETAIL");

            entity.Property(e => e.Uniqueid)
                .HasColumnType("NUMBER(30,6)")
                .HasColumnName("UNIQUEID");
            entity.Property(e => e.Snum)
                .HasColumnType("NUMBER(30,6)")
                .HasColumnName("SNUM");
            entity.Property(e => e.Batchused)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("BATCHUSED");
            entity.Property(e => e.Enddate)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ENDDATE");
            entity.Property(e => e.Endinv)
                .HasColumnType("NUMBER(30,6)")
                .HasColumnName("ENDINV");
            entity.Property(e => e.Horepgroups)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("HOREPGROUPS");
            entity.Property(e => e.Invennum)
                .HasColumnType("NUMBER(30,6)")
                .HasColumnName("INVENNUM");
            entity.Property(e => e.Laststore)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("LASTSTORE");
            entity.Property(e => e.Lastupdate)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("LASTUPDATE");
            entity.Property(e => e.Other)
                .HasColumnType("NUMBER(30,6)")
                .HasColumnName("OTHER");
            entity.Property(e => e.Overshort)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("OVERSHORT");
            entity.Property(e => e.Plink)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PLINK");
            entity.Property(e => e.Posused)
                .HasColumnType("NUMBER(30,6)")
                .HasColumnName("POSUSED");
            entity.Property(e => e.Purchased)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("PURCHASED");
            entity.Property(e => e.Qlink)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("QLINK");
            entity.Property(e => e.Qtyintransfer)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("QTYINTRANSFER");
            entity.Property(e => e.Reason)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("REASON");
            entity.Property(e => e.Repgroup)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("REPGROUP");
            entity.Property(e => e.Shortvalue)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("SHORTVALUE");
            entity.Property(e => e.Spill)
                .HasColumnType("NUMBER(30,6)")
                .HasColumnName("SPILL");
            entity.Property(e => e.Startdate)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("STARTDATE");
            entity.Property(e => e.Startinv)
                .HasColumnType("NUMBER(30,6)")
                .HasColumnName("STARTINV");
            entity.Property(e => e.Stocktakenum)
                .HasColumnType("NUMBER(30,6)")
                .HasColumnName("STOCKTAKENUM");
            entity.Property(e => e.Storetransferout)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("STORETRANSFEROUT");
            entity.Property(e => e.Transferin)
                .HasColumnType("NUMBER(30,6)")
                .HasColumnName("TRANSFERIN");
            entity.Property(e => e.Transferout)
                .HasColumnType("NUMBER(30,6)")
                .HasColumnName("TRANSFEROUT");
            entity.Property(e => e.Warehousenum)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("WAREHOUSENUM");
            entity.Property(e => e.Whoenter)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("WHOENTER");
        });

        modelBuilder.Entity<XcubePayinout>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("XCUBE_PAYINOUT");

            entity.Property(e => e.Calctendered)
                .HasColumnType("NUMBER")
                .HasColumnName("CALCTENDERED");
            entity.Property(e => e.Opendate)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("OPENDATE");
            entity.Property(e => e.Payinout)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PAYINOUT");
            entity.Property(e => e.Reason)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("REASON");
            entity.Property(e => e.Refcode)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("REFCODE");
            entity.Property(e => e.Snum)
                .HasColumnType("NUMBER")
                .HasColumnName("SNUM");
        });

        modelBuilder.Entity<XcubePayment>(entity =>
        {
            entity.HasKey(e => new { e.Snum, e.Opendate, e.Methodnum }).HasName("XCUBE_PAYMENTS_PK");

            entity.ToTable("XCUBE_PAYMENTS");

            entity.Property(e => e.Snum)
                .HasColumnType("NUMBER")
                .HasColumnName("SNUM");
            entity.Property(e => e.Opendate)
                .HasColumnType("DATE")
                .HasColumnName("OPENDATE");
            entity.Property(e => e.Methodnum)
                .HasColumnType("NUMBER")
                .HasColumnName("METHODNUM");
            entity.Property(e => e.Authreqrd)
                .HasColumnType("NUMBER")
                .HasColumnName("AUTHREQRD");
            entity.Property(e => e.Chargetip)
                .HasColumnType("NUMBER")
                .HasColumnName("CHARGETIP");
            entity.Property(e => e.Chargetipminustipsurcharge)
                .HasColumnType("NUMBER")
                .HasColumnName("CHARGETIPMINUSTIPSURCHARGE");
            entity.Property(e => e.Credit)
                .HasColumnType("NUMBER")
                .HasColumnName("CREDIT");
            entity.Property(e => e.Currency)
                .HasColumnType("NUMBER")
                .HasColumnName("CURRENCY");
            entity.Property(e => e.Debit)
                .HasColumnType("NUMBER")
                .HasColumnName("DEBIT");
            entity.Property(e => e.Methoddesc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("METHODDESC");
            entity.Property(e => e.Paycount)
                .HasColumnType("NUMBER")
                .HasColumnName("PAYCOUNT");
            entity.Property(e => e.Tendered)
                .HasColumnType("NUMBER")
                .HasColumnName("TENDERED");
            entity.Property(e => e.Tipsurcharge)
                .HasColumnType("NUMBER")
                .HasColumnName("TIPSURCHARGE");
            entity.Property(e => e.Tipsurchargevalue)
                .HasColumnType("NUMBER")
                .HasColumnName("TIPSURCHARGEVALUE");
            entity.Property(e => e.Totaltender)
                .HasColumnType("NUMBER")
                .HasColumnName("TOTALTENDER");
            entity.Property(e => e.Voidedpaycount)
                .HasColumnType("NUMBER")
                .HasColumnName("VOIDEDPAYCOUNT");
        });

        modelBuilder.Entity<Empdepartment>(entity =>
        {
            entity.HasKey(e => new { e.Deptnum, e.Storeid }).HasName("EMPDEPARTMENTS_PK");

            entity.ToTable("EMPDEPARTMENTS");

            entity.Property(e => e.Deptnum)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("DEPTNUM");
            entity.Property(e => e.Storeid)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("STOREID");
            entity.Property(e => e.Descript)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("DESCRIPT");
            entity.Property(e => e.Isactive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("ISACTIVE");
        });
        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Memcode).HasName("MEMBER_PK");

            entity.ToTable("MEMBER");

            entity.Property(e => e.Memcode)
                .HasColumnType("NUMBER")
                .HasColumnName("MEMCODE");
            entity.Property(e => e.Adress1)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ADRESS1");
            entity.Property(e => e.Adress2)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ADRESS2");
            entity.Property(e => e.Firstname)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.Hometele).HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("HOMETELE");
            entity.Property(e => e.Lastname)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("LASTNAME");
            entity.Property(e => e.Lastorderdate)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("LASTORDERDATE");
            entity.Property(e => e.Lasttrans)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("LASTTRANS");
            entity.Property(e => e.Lasttrans2)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("LASTTRANS2");
            entity.Property(e => e.Lasttrans3)
                .HasMaxLength(500).IsUnicode(false)
                .HasColumnName("LASTTRANS3");
            entity.Property(e => e.Lastvisit)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("LASTVISIT");
            entity.Property(e => e.Snum)
                .HasColumnType("NUMBER")
                .HasColumnName("SNUM");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
