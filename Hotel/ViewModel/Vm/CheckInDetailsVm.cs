using System.Windows;
using System.Windows.Input;
using Hotel.ViewModel.VmEntities;
using ViewModel.Services;

namespace Hotel.ViewModel;

public class CheckInDetailsVm:BaseVm
{
    private CheckIn _selectedCheckIn;
    private CheckInService chService=new CheckInService();
    private AdditionalServicesService serviceServ=new AdditionalServicesService();
    private bool _sauna;
    private bool _bar;
    private bool _billiards;

    public bool Sauna
    {
        get => _sauna;
        set
        {
            _sauna = value;
            OnPropertyChanged(nameof(Sauna));
           
        } 
    }
    public bool Bar
    {
        get => _bar;
        set
        {
            _bar = value;
            OnPropertyChanged(nameof(Bar));
           
        } 
    }
    public bool Billiards
    {
        get => _billiards;
        set
        {
            _billiards = value;
            OnPropertyChanged(nameof(Billiards));
           
        } 
    }
    public ICommand OK
    {
        get;
    }
    public CheckInDetailsVm()
    {
        OK = new VmCommand(ExecOk, CanExecOk);
    }
    private void ExecOk(object obj)
    {
         (obj as Window).DialogResult = true;
    }
    private bool CanExecOk(object obj)
    {
        return true;
    }
    public CheckIn SelectedCheckIn
    {
        get => _selectedCheckIn;
        set
        {
            _selectedCheckIn = value;
            OnPropertyChanged(nameof(SelectedCheckIn));
           
        } 
    }
    public void Load(int chId)
    {
        SelectedCheckIn = chService.GetCheckInById(chId);
        LoadServices(chId);
    }
    private void LoadServices(int chId)
    {
        var list = serviceServ.GetAdditionalServicesForReservation(chId);
        Bar=list.Exists(r => r.Service.Name == "Безлимитный бар");
        Sauna=list.Exists(r => r.Service.Name == "Посещение сауны");
        Billiards=list.Exists(r => r.Service.Name == "Бильярд");
    }
}