using System;
using System.Collections.Generic;

namespace DataIntegration.Models;

public partial class Posbank
{
    public int Uniqueid { get; set; }

    public int? Punchindex { get; set; }

    public int? Methodnum { get; set; }

    public int? Calctendered { get; set; }

    public int? Exchangerate { get; set; }

    public string? Dateentered { get; set; }

    public int? Entrytype { get; set; }

    public string? Refcode { get; set; }

    public DateTime? Opendate { get; set; }

    public int? Whoauth { get; set; }

    public string? Plink { get; set; }

    public int? Reasonid { get; set; }

    public int? Safenum { get; set; }

    public int? Floatnum { get; set; }

    public int? Denomnum { get; set; }

    public int? Wasprocessed { get; set; }

    public int? Isactive { get; set; }

    public int? Statnum { get; set; }

    public int? Memcode { get; set; }

    public string? Qlink { get; set; }

    public int? Updatestatus { get; set; }

    public int? CloudReflectionupdate { get; set; }

    public int Snum { get; set; }
}
