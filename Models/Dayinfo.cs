using System;
using System.Collections.Generic;

namespace DataIntegration.Models;

public partial class Dayinfo
{
    public string? Transdate { get; set; }

    public string? Timestart { get; set; }

    public string? Timeend { get; set; }

    public string? Transend { get; set; }

    public string? Weather { get; set; }

    public string? Whoopen { get; set; }

    public string? Whoclose { get; set; }

    public string? Whoopenname { get; set; }

    public string? Whoclosename { get; set; }

    public string? Temperature { get; set; }

    public string? Empwages { get; set; }

    public string? Numshifts { get; set; }

    public int? Storenum { get; set; }

    public int? Salesnet { get; set; }

    public int? Tax1 { get; set; }

    public int? Salesgross { get; set; }

    public int? Numtrans { get; set; }

    public int? Numcoupons { get; set; }

    public int? Couponvalue { get; set; }

    public int? Numvoids { get; set; }

    public int? Voidvalue { get; set; }

    public int? Numclients { get; set; }

    public int? Clientsales { get; set; }

    public int? Pointsawarded { get; set; }

    public int? Sumgroup1 { get; set; }

    public int? Sumgroup2 { get; set; }

    public int? Sumgroup3 { get; set; }

    public int? Sumgroup4 { get; set; }

    public int? Sumgroup5 { get; set; }

    public int? Sumgroup6 { get; set; }

    public int? Sumgroup7 { get; set; }

    public int? Sumgroup8 { get; set; }

    public int? Sumgroup9 { get; set; }

    public int? Sumgroup10 { get; set; }

    public string? Reexport { get; set; }

    public string Opendate { get; set; } = null!;

    public int? Repgroup { get; set; }

    public int Snum { get; set; }

    public string? Lastupdate { get; set; }

    public int? Laststore { get; set; }

    public int? Uiid { get; set; }
}
