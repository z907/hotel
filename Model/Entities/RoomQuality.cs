using System;
using System.Collections.Generic;

namespace Model.Entities;

public partial class RoomQuality
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Price { get; set; }

    public virtual RoomAttribute? RoomAttribute { get; set; }
}
