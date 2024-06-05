using System;
using System.Collections.Generic;

namespace kers.Models;

public partial class Trip
{
    public int Id { get; set; }

    public int Routeid { get; set; }

    public int Statusid { get; set; }

    public int Autoid { get; set; }

    public DateTime Timestart { get; set; }

    public virtual Auto Auto { get; set; } = null!;

    public virtual Route Route { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
