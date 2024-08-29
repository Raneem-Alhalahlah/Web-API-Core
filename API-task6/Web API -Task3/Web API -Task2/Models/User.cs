using System;
using System.Collections.Generic;

namespace Web_API__Task2.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public int? Password { get; set; }

    public string? Email { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
