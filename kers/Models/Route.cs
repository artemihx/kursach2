using System;
using System.Collections.Generic;

namespace kers.Models;

public partial class Route
{
    public Route(int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}
