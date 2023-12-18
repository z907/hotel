using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace Hotel.ViewModel.VmEntities;

public class DisplayRoom
{
    public int? Id { get; set; }
    [DisplayName("Номер комнаты")]
    public int? Number { get; set; }
    [DisplayName("Фото")]
    public BitmapImage? Image { get; set; }
}