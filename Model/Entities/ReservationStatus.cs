using System;
using System.Collections.Generic;

namespace Model.Entities;

public partial class ReservationStatus
{
    public int Id { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
