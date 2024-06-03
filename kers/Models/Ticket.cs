using System;
using System.Collections.Generic;

namespace kers.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public int Fktripid { get; set; }

    public int Fkpassportid { get; set; }

    public virtual Passport Fkpassport { get; set; } = null!;

    public virtual Trip Fktrip { get; set; } = null!;

    public virtual ICollection<Ticketontrip> Ticketontrips { get; set; } = new List<Ticketontrip>();
}
