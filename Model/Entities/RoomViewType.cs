﻿using System;
using System.Collections.Generic;

namespace Model.Entities;

public partial class RoomViewType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Price { get; set; }

    public virtual ICollection<RoomAttribute> RoomAttributes { get; set; } = new List<RoomAttribute>();
}
