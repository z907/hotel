using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace Hotel.ViewModel.VmEntities;

public class DisplayRoom
{
    public int? Id { get; set; }
    [DisplayName("Номер комнаты")]
    public int? Number { get; set; }
    
    [DisplayName("Вместимость")]
    public int? Capacity  { get; set; }
    [DisplayName("Тип комнаты")]
    public string? Quality  { get; set; }
    [DisplayName("Тип вида")]
    public string? ViewType  { get; set; }
    
    [DisplayName("Пригодна")]
    public string? Valid { get; set; }
    [DisplayName("Цена")]
    public int? Price { get; set; }
    
    [DisplayName("Фото")]
    public BitmapImage? Image { get; set; }
}