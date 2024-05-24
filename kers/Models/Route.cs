using System;
using System.Collections.Generic;

namespace kers.Models;

public partial class Route
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
