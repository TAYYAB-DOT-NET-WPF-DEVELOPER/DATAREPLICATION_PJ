using System;
using System.Collections.Generic;

namespace DataIntegration.Models;

public partial class XcubePayment
{
    public int Snum { get; set; }

    public DateTime Opendate { get; set; }

    public int? Currency { get; set; }

    public string? Methoddesc { get; set; }

    public int Methodnum { get; set; }

    public int? Tendered { get; set; }

    public int? Paycount { get; set; }

    public int? Voidedpaycount { get; set; }

    public int? Debit { get; set; }

    public int? Credit { get; set; }

    public int? Chargetip { get; set; }

    public int? Totaltender { get; set; }

    public int? Tipsurcharge { get; set; }

    public int? Tipsurchargevalue { get; set; }

    public int? Chargetipminustipsurcharge { get; set; }

    public int? Authreqrd { get; set; }
}
