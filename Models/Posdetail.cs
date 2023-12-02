using System;
using System.Collections.Generic;

namespace DataIntegration.Models;

public partial class Posdetail
{
    public int Uniqueid { get; set; }

    public int? Transact { get; set; }

    public int? Prodnum { get; set; }

    public int? Whoorder { get; set; }

    public int? Whoauth { get; set; }

    public int? Costeach { get; set; }

    public int? Quan { get; set; }

    public DateTime? Timeord { get; set; }

    public int? Notax { get; set; }

    public int? Howordered { get; set; }

    public int? Status { get; set; }

    public int? Recpos { get; set; }

    public int? Prodtype { get; set; }

    public int? Applytax1 { get; set; }

    public int? Applytax2 { get; set; }

    public int? Reduceinventory { get; set; }

    public int? Storenum { get; set; }

    public int? Statnum { get; set; }

    public DateTime? Opendate { get; set; }

    public int? Mealtime { get; set; }

    public string? Linedes { get; set; }

    public int? Revcenter { get; set; }

    public int? Discount { get; set; }

    public int? Authcode { get; set; }

    public int Snum { get; set; }

    public int? Repgroup { get; set; }

    public int? Istestdata { get; set; }
}
