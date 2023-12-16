using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model.Entities;

public partial class Customer
{
    public int? Id { get; set; }
    [DisplayName("Имя")]
    public string? Name { get; set; }
    [DisplayName("Фамилия")]
    public string? Surname { get; set; }
    [DisplayName("Отчество")]
    public string? LastName { get; set; }
    [DisplayName("Телефон")]
    public string? PhoneNumber { get; set; }
    [DisplayName("Эл. Почта")]
    public string? Email { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
