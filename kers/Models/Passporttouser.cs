using System;
using System.Collections.Generic;

namespace kers.Models;

public partial class Passporttouser
{
    public int Id { get; set; }

    public int? Fkuserid { get; set; }

    public int? Fkpassportid { get; set; }

    public virtual Passport? Fkpassport { get; set; }

    public virtual User? Fkuser { get; set; }
}
