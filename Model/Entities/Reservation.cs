using System;
using System.Collections.Generic;

namespace Model.Entities;

public partial class Reservation
{
    public int Id { get; set; }

    public int? RoomAttributesId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int CustomerId { get; set; }

    public int? RoomId { get; set; }

    public int? TotalCost { get; set; }

    public int? Status { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Room? Room { get; set; }

    public virtual RoomAttribute? RoomAttributes { get; set; }

    public virtual ReservationStatus? StatusNavigation { get; set; }
}
