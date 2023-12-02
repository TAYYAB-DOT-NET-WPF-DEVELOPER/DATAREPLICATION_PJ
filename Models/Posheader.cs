using System;
using System.Collections.Generic;

namespace DataIntegration.Models;

public partial class Posheader
{
    public int Transact { get; set; }

    public DateTime? Timestart { get; set; }

    public DateTime? Timeend { get; set; }

    public int? Numcust { get; set; }

    public int? Tax1 { get; set; }

    public int? Tax2 { get; set; }

    public int? Tax1able { get; set; }

    public int? Tax2able { get; set; }

    public int? Nettotal { get; set; }

    public int? Whostart { get; set; }

    public int? Whoclose { get; set; }

    public int? Issplit { get; set; }

    public int? Saletypeindex { get; set; }

    public int? Status { get; set; }

    public int? Finaltotal { get; set; }

    public int? Storenum { get; set; }

    public DateTime? Opendate { get; set; }

    public int? Memcode { get; set; }

    public int? Totalpoints { get; set; }

    public int? Pointsapplied { get; set; }

    public int? Isdelivery { get; set; }

    public int? Tax1exempt { get; set; }

    public int? Mealtime { get; set; }

    public int? Isinternet { get; set; }

    public int? Revcenter { get; set; }

    public int? Numprintedfinal { get; set; }

    public string? Refid { get; set; }

    public int Snum { get; set; }

    public DateTime? Opdate { get; set; }

    public DateTime? Tstart { get; set; }

    public DateTime? Tend { get; set; }

    public string? Sts { get; set; }
}
