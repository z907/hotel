using System;
using System.Collections.Generic;

namespace Model.Entities;

public partial class RoomAttribute
{
    public int Id { get; set; }

    public int? QualityId { get; set; }

    public int? CapacityId { get; set; }

    public int? ViewId { get; set; }

    public int? Price { get; set; }

    public virtual RoomCapacity? Capacity { get; set; }

    public virtual RoomQuality? Quality { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public virtual RoomViewType? View { get; set; }
}
