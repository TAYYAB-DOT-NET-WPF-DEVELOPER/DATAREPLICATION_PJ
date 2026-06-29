using System;
using System.Collections.Generic;

namespace DataIntegration.Models;

public partial class Punchpayroll
{
    public int Punchindex { get; set; }

    public string? Punchin { get; set; }

    public string? Punchout { get; set; }

    public int? Payrate { get; set; }

    public int? Origpayrate { get; set; }

    public int? Jobtype { get; set; }

    public int? Empnum { get; set; }

    public DateTime? Opendate { get; set; }

    public int? Shiftindex { get; set; }

    public int? Tip { get; set; }

    public int? Quanvoid { get; set; }

    public int? Voidsales { get; set; }

    public int? Othours { get; set; }

    public int? Otwage { get; set; }

    public int? Shiftcount { get; set; }

    public int? Breakcount { get; set; }

    public int? Mealtaken { get; set; }

    public int? Breaktaken { get; set; }

    public int? Breakscancelled { get; set; }

    public int? Breakwaived { get; set; }

    public int? Mealwaived { get; set; }

    public float? Totalhours { get; set; }

    public int? Breakunpaidhours { get; set; }

    public int? Breakpaidhours { get; set; }

    public int? Breakhours { get; set; }

    public float? Paidhours { get; set; }

    public float? Reghours { get; set; }

    public float? Regrate { get; set; }

    public float? Totalwage { get; set; }

    public float? Averagedpayrate { get; set; }

    public int? Revcenter { get; set; }

    public int? Numnosale { get; set; }

    public float? Tillbalance { get; set; }

    public int Storeid { get; set; }
}
