using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using Model.Entities;

namespace Hotel.VmEntities;

public class DisplayReservation
{
    public DisplayReservation()
    {
        
    }
    public DisplayReservation(DisplayReservation item)
    {
        this.Id = item.Id;
        this.CustomerId = item.CustomerId;
        this.StartDate = item.StartDate;
        this.EndDate = item.EndDate;
        this.RoomViewType = item.RoomViewType;
        this.RoomQuality = item.RoomQuality;
        this.RoomCapacity = item.RoomCapacity;
        this.CustomerSurname = item.CustomerSurname;
        this.TotalCost = item.TotalCost;
    }
    public int? Id { get; set; }

    public int? CustomerId { get; set; }
    [DisplayName("Дата начала")]
    public DateOnly? StartDate { get; set; }
    [DisplayName("Дата конца")]
    public DateOnly? EndDate { get; set; }
    [DisplayName("Вид из номера")]
    public string? RoomViewType { get; set; }
    [DisplayName("Тип номера")]
    public string? RoomQuality { get; set; }
    [DisplayName("Вместимость")]
    public int? RoomCapacity { get; set; }
    [DisplayName("Фамилия")]
    public string? CustomerSurname { get; set; }
    [DisplayName("Стоимость")]
    public int? TotalCost { get; set; }
    
}