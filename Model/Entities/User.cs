using System;
using System.Collections.Generic;

namespace Model.Entities;

public partial class User
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public int? Role { get; set; }

    public virtual UserRole? RoleNavigation { get; set; }
}
