using System;
using System.Collections.Generic;

namespace kers.Models;

public partial class Passport
{
    public int Id { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string? Mname { get; set; }

    public string Serail { get; set; } = null!;

    public string Number { get; set; } = null!;

    public DateOnly Datetaken { get; set; }

    public virtual ICollection<Passporttouser> Passporttousers { get; set; } = new List<Passporttouser>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
