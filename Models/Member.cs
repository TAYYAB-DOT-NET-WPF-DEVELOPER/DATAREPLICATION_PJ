using System;
using System.Collections.Generic;

namespace POS.Models;

public partial class Member
{
    public decimal Memcode { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Adress1 { get; set; }

    public string? Adress2 { get; set; }

    public string? Hometele { get; set; }

    public string? Lastvisit { get; set; }

    public string? Lasttrans { get; set; }

    public string? Lasttrans2 { get; set; }

    public string? Lasttrans3 { get; set; }

    public string? Lastorderdate { get; set; }

    public decimal? Snum { get; set; }
}
