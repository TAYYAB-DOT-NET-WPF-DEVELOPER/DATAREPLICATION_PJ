using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataIntegration.Models;

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

    public virtual DbSet<Posbank> Posbanks { get; set; }

    public virtual DbSet<Posdetail> Posdetails { get; set; }

    public virtual DbSet<Posheader> Posheaders { get; set; }

    public virtual DbSet<Punchclock> Punchclocks { get; set; }

    public virtual DbSet<Punchpayroll> Punchpayrolls { get; set; }

    public virtual DbSet<Salestype> Salestypes { get; set; }

    public virtual DbSet<Stocktakedetail> Stocktakedetails { get; set; }

    public virtual DbSet<XcubePayinout> XcubePayinouts { get; set; }

    public virtual DbSet<XcubePayment> XcubePayments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=103.217.178.67)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=SCAR)));User Id=PJPAR;Password=PJPAR;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("PJPAR")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Dayinfo>(entity =>
        {
            entity.HasKey(e => new { e.Opendate, e.Snum }).HasName("DAYINFO_PK");

            entity.ToTable("DAYINFO");

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
            entity.Property(e => e.Laststore)
                .HasColumnType("NUMBER")
                .HasColumnName("LASTSTORE");
            entity.Property(e => e.Lastupdate)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LASTUPDATE");
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
            entity.Property(e => e.Repgroup)
                .HasColumnType("NUMBER")
                .HasColumnName("REPGROUP");
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
                .HasColumnName("UIID");
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
                .HasColumnName("ADRESS1");
            entity.Property(e => e.Adress2)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("ADRESS2");
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
            entity.HasKey(e => new { e.Uniqueid, e.Snum }).HasName("POSDETAIL_PK");

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
        });

        modelBuilder.Entity<Posheader>(entity =>
        {
            entity.HasKey(e => new { e.Snum, e.Transact }).HasName("POSHEADER_PK");

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
        modelBuilder.HasSequence("ELECTRIC_UNITS_SEQ");
        modelBuilder.HasSequence("HRATTENDANCE_SEQ");
        modelBuilder.HasSequence("HRATTENDANCE_SEQ1");
        modelBuilder.HasSequence("HRCALENDAR_SEQ");
        modelBuilder.HasSequence("HRROSTER_SEQ");
        modelBuilder.HasSequence("SEND_SMS_SEQ");
        modelBuilder.HasSequence("SHAHROZ_TEST_SEQ");
        modelBuilder.HasSequence("TEST_HRATTENDANCE_SEQ");
        modelBuilder.HasSequence("TEST_PJ_SEQ");
        modelBuilder.HasSequence("TEST_PJ_SEQ1");
        modelBuilder.HasSequence("TEST_PJ_SEQ2");
        modelBuilder.HasSequence("WEEKLY_PJI_SEQ");
        modelBuilder.HasSequence("XCUBE_DISCOUNTDETAILS_SEQ");
        modelBuilder.HasSequence("XCUBE_DISCOUNTDETAILS_SEQ1");
        modelBuilder.HasSequence("XCUBE_DISCOUNTDETAILS_SEQ2");
        modelBuilder.HasSequence("XCUBE_DISCOUNTDETAILS_SEQ3");
        modelBuilder.HasSequence("XCUBE_DISCOUNTDETAILS_SEQ4");
        modelBuilder.HasSequence("XCUBE_HOURLYSUMMARYGROUP_SEQ");
        modelBuilder.HasSequence("XCUBE_PAYINOUT_SEQ");
        modelBuilder.HasSequence("XCUBE_VOIDDETAILS_SEQ");
        modelBuilder.HasSequence("XCUBE_VOIDDETAILS_SEQ1");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
