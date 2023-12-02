using System;
using System.Collections.Generic;

namespace DataIntegration.Models;

public partial class XcubePayinout
{
    public int? Snum { get; set; }

    public string? Opendate { get; set; }

    public string? Payinout { get; set; }

    public int? Calctendered { get; set; }

    public string? Reason { get; set; }

    public string? Refcode { get; set; }
}
