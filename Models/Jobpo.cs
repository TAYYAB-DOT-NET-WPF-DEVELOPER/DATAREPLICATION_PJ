using System;
using System.Collections.Generic;

namespace DataIntegration.Models;

public partial class Jobpo
{
    public int Jobpos { get; set; }

    public string? Descript { get; set; }

    public int? Deptnum { get; set; }

    public string? Isactive { get; set; }

    public int Storeid { get; set; }
}
