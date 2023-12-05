using System;
using System.Collections.Generic;

namespace Model.Entities;

public partial class Payment
{
    public int Id { get; set; }

    public DateOnly? Date { get; set; }

    public int? Sum { get; set; }

    public int? ReservationId { get; set; }

    public virtual Reservation? Reservation { get; set; }
}
