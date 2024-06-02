using System;
using System.Collections.Generic;

namespace kers.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Passporttouser> Passporttousers { get; set; } = new List<Passporttouser>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    
    public static User curUser { get; set; }
}
