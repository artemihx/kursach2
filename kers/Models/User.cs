using System;
using System.Collections.Generic;

namespace kers.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public virtual ICollection<Passporttouser> Passporttousers { get; set; } = new List<Passporttouser>();
    
    public static User curUser { get; set; }
}
