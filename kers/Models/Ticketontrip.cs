using System;
using System.Collections.Generic;

namespace kers.Models;

public partial class Ticketontrip
{
    public int Id { get; set; }

    public int? Fkticketid { get; set; }

    public int? Fktripid { get; set; }

    public virtual Trip? Fktrip { get; set; }
}
