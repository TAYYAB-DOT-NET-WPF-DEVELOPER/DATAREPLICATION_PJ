using System;
using System.Collections.Generic;

namespace POS.Models;

public partial class Empdepartment
{
    public decimal Deptnum { get; set; }

    public string? Descript { get; set; }

    public string? Isactive { get; set; }

    public decimal Storeid { get; set; }
}