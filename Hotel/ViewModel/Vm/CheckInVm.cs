using System.Windows;
using System.Windows.Input;
using Hotel.ViewModel.VmEntities;
using ViewModel.Services;

namespace Hotel.ViewModel;

public class CheckInVm:BaseVm
{
    private RoomService roomServ;
    private ReservationService reservationServ;
    private AttributeService attrServ;
    private List<DisplayRoom> _rooms;
    private int _resId;
    private DisplayRoom _selRoom;
    public DisplayRoom SelRoom
    {
        get => _selRoom;
        set
        {
            _selRoom = value;
            OnPropertyChanged(nameof(SelRoom));
        }
    }
   
    public List<DisplayRoom> Rooms
    {
        get => _rooms;
        set
        {
            _rooms = value;
            OnPropertyChanged(nameof(Rooms));
        }
    }
    public ICommand OK
    {
        get;
    }
    public CheckInVm()
    {
        OK = new VmCommand(ExecOk, CanExecOk);
        roomServ = new RoomService();
        reservationServ = new ReservationService();
    }

    public void LoadRooms(int resId)
    {
        _resId = resId;
      Rooms=roomServ.GetRoomsByType((int)reservationServ.GetAttributeIdById(resId));
    }
    
    private void ExecOk(object obj)
    {
        if (SelRoom != null)
        {
            reservationServ.MakeCheckIn((int)SelRoom.Id, _resId);
            (obj as Window).DialogResult = true;
        }
        else MessageBox.Show("Комната не выбрана!");

    }
    private bool CanExecOk(object obj)
    {
        return true;
    }
}