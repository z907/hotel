using System.ComponentModel;

namespace Hotel.ViewModel.VmEntities;

public class CheckIn
{
    public CheckIn()
    {
        
    }
    
    public int? Id { get; set; }
    
    public int? CustomerId { get; set; }
    [DisplayName("Дата начала")]
    public DateOnly? StartDate { get; set; }
    [DisplayName("Дата конца")]
    public DateOnly? EndDate { get; set; }
    
    public string? RoomViewType { get; set; }
   
    public string? RoomQuality { get; set; }
    
    public int? RoomCapacity { get; set; }
    
    public int? RoomId { get; set; }
    
    [DisplayName("Комната")]
    public int? RoomNum { get; set; }
    [DisplayName("Фамилия")]
    public string? CustomerSurname { get; set; }
    public int? StatusId { get; set; }
    [DisplayName("Статус")]
    public string Status { get; set; }
    [DisplayName("К оплате")]
    public int? ToPay { get; set; }
    [DisplayName("Стоимость")]
    public int? TotalCost { get; set; }
}