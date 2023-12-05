using System;
using System.Collections.Generic;

namespace Model.Entities;

public partial class RoomCapacity
{
    public int Id { get; set; }

    public int? Value { get; set; }

    public int? Price { get; set; }

    public virtual ICollection<RoomAttribute> RoomAttributes { get; set; } = new List<RoomAttribute>();
}
