using System;
using System.Collections.Generic;

namespace Model.Entities;

public partial class Room
{
    public int Id { get; set; }

    public int? Number { get; set; }

    public int? RoomAttributesId { get; set; }

    public bool? Valid { get; set; }
    
    public byte[]? Image { get; set; }
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual RoomAttribute? RoomAttributes { get; set; }
}
