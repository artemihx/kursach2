using System;
using System.Collections.Generic;

namespace kers.Models;

public partial class Trip
{
    public int Id { get; set; }

    public int? Routeid { get; set; }

    public int? Statusid { get; set; }

    public DateTime? Timestart { get; set; }

    public DateTime? Timeend { get; set; }

    public virtual Route? Route { get; set; }

    public virtual Status? Status { get; set; }
}
