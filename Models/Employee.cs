using System;
using System.Collections.Generic;

namespace DataIntegration.Models;

public partial class Employee
{
    public int Empnum { get; set; }

    public string? Empname { get; set; }

    public string? Emplastname { get; set; }

    public string? Dateentered { get; set; }

    public string? Adress1 { get; set; }

    public string? Adress2 { get; set; }

    public string? Startwork { get; set; }

    public string? Endwork { get; set; }

    public string? Isactive { get; set; }

    public string? Posname { get; set; }

    public int? Empposition { get; set; }

    public int? Hourlywage { get; set; }

    public string? Gender { get; set; }

    public int Storeid { get; set; }

    public string? Sin { get; set; }

    public string? Horepgroups { get; set; }
}
