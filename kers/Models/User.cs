﻿using System;
using System.Collections.Generic;

namespace kers.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }
}
