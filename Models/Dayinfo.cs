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

    public float? Salesnet { get; set; }

    public float? Tax1 { get; set; }

    public float? Salesgross { get; set; }

    public int? Numtrans { get; set; }

    public int? Numcoupons { get; set; }

    public float? Couponvalue { get; set; }

    public float? Numvoids { get; set; }

    public float? Voidvalue { get; set; }

    public int? Numclients { get; set; }

    public float? Clientsales { get; set; }

    public int? Pointsawarded { get; set; }

    public float? Sumgroup1 { get; set; }

    public float? Sumgroup2 { get; set; }

    public float? Sumgroup3 { get; set; }

    public float? Sumgroup4 { get; set; }

    public float? Sumgroup5 { get; set; }

    public float? Sumgroup6 { get; set; }

    public float? Sumgroup7 { get; set; }

    public float? Sumgroup8 { get; set; }

    public float? Sumgroup9 { get; set; }

    public float? Sumgroup10 { get; set; }

    public string? Reexport { get; set; }

    public string? Opendate { get; set; } = null!;

    public int Snum { get; set; }

    public int Uiid { get; set; }
}
