using Model.Entities;
using ViewModel.Services;

namespace Hotel.ViewModel;

public class PickDateVm:BaseVm
{
    private AttributeService attr;
    private List<RoomCapacity> cap;
    private List<RoomViewType> rview;
    private List<RoomQuality> qal;
    
   
    
    public List<RoomCapacity> Cap
    {
        get => cap;
        set
        {
            cap = value;
            OnPropertyChanged(nameof(Cap));
        } 
    }
    public List<RoomQuality> Qal
    {
        get => qal;
        set
        {
            qal = value;
            OnPropertyChanged(nameof(Qal));
        } 
    }
    public List<RoomViewType> Rview
    {
        get => rview;
        set
        {
            rview = value;
            OnPropertyChanged(nameof(Rview));
        } 
    }
    public PickDateVm()
    {
       
        LoadComboBoxes();
    }

    
    private void LoadComboBoxes()
    {
        attr = new AttributeService();
        cap = attr.GetCapacities();
        qal = attr.GetQualities();
        rview = attr.GetViews();
    }
}