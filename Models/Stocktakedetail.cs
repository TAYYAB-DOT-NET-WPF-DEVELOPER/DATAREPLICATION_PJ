using System;
using System.Collections.Generic;

namespace DataIntegration.Models;

public partial class Stocktakedetail
{
    public int Uniqueid { get; set; }

    public int? Stocktakenum { get; set; }

    public int? Invennum { get; set; }

    public int? Startinv { get; set; }

    public int? Endinv { get; set; }

    public int? Posused { get; set; }

    public int? Spill { get; set; }

    public int? Transferin { get; set; }

    public int? Transferout { get; set; }

    public int? Other { get; set; }

    public string? Startdate { get; set; }

    public string? Enddate { get; set; }

    public string? Whoenter { get; set; }

    public string? Purchased { get; set; }

    public string? Batchused { get; set; }

    public string? Overshort { get; set; }

    public string? Shortvalue { get; set; }

    public string? Warehousenum { get; set; }

    public string? Reason { get; set; }

    public int Snum { get; set; }

    public string? Plink { get; set; }

    public string? Storetransferout { get; set; }

    public string? Qtyintransfer { get; set; }

    public string? Qlink { get; set; }

    public string? Laststore { get; set; }

    public string? Lastupdate { get; set; }

    public string? Repgroup { get; set; }

    public string? Horepgroups { get; set; }
}
