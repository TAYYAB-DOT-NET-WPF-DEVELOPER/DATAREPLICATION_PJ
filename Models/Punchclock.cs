using System;
using System.Collections.Generic;

namespace DataIntegration.Models;

public partial class Punchclock
{
    public int Uniqueid { get; set; }

    public string? Punchin { get; set; }

    public string? Punchout { get; set; }

    public float? Payrate { get; set; }

    public int? Jobtype { get; set; }

    public int? Empnum { get; set; }

    public int? Storenum { get; set; }

    public DateTime? Opendate { get; set; }

    public int? Mealtime { get; set; }

    public int Storeid { get; set; }
}
