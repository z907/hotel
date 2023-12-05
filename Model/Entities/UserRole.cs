using System;
using System.Collections.Generic;

namespace Model.Entities;

public partial class UserRole
{
    public int Id { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
