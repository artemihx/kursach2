using System;
using System.Collections.Generic;

namespace kers.Models;

public partial class Auto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Carnumber { get; set; } = null!;

    public int Maxcountpassneger { get; set; }

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
