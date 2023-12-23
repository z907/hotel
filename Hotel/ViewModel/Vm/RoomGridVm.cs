using System.Windows.Forms;
using System.Windows.Input;
using Hotel.View;
using Hotel.ViewModel.VmEntities;
using ViewModel.Services;

namespace Hotel.ViewModel;

public class RoomGridVm:BaseVm
{
    private RoomService roomServ = new RoomService();
    public ICommand Add { get; }
    public ICommand EditImage { get; }
    public ICommand Delete { get; }
    public ICommand ToggleValid { get; }

    private DisplayRoom _selectedRoom;

    public DisplayRoom SelectedRoom
    {
        get => _selectedRoom;
        set
        {
            _selectedRoom = value;
            OnPropertyChanged(nameof(SelectedRoom));
        }
    }

    private List<DisplayRoom> _roomList;

    public List<DisplayRoom> RoomList
    {
        get => _roomList;
        set
        {
            _roomList = value;
            OnPropertyChanged(nameof(RoomList));
        }
    }
    public RoomGridVm()
    {
        Add = new VmCommand(ExecuteAdd,CanExecuteAdd);
        Delete = new VmCommand(ExecuteDelete,CanExecuteDelete);
        EditImage = new VmCommand(ExecuteEditImage,CanExecuteEditImage);
        ToggleValid = new VmCommand(ExecuteToggle,CanExecuteToggle);
       RoomList= roomServ.GetAllRooms();
    }

    public void ExecuteAdd(object obj)
    {
        AddRoomDialog win = new AddRoomDialog();
        if (win.ShowDialog() == true)
        {
            RoomList = roomServ.GetAllRooms();
        }
    }
    public bool CanExecuteAdd(object obj)
    {
        return true;
    }
    public void ExecuteDelete(object obj)
    {
        if (roomServ.DeleteRoom((int)SelectedRoom.Id)) RoomList = roomServ.GetAllRooms();
        else MessageBox.Show("На комнату ссылаются заселения, невозможно удалить!");
    }
    public bool CanExecuteDelete(object obj)
    {
        return SelectedRoom!=null;
    }
    public void ExecuteEditImage(object obj)
    {
        
    }
    public bool CanExecuteEditImage(object obj)
    {
        return SelectedRoom!=null;
    }
    public void ExecuteToggle(object obj)
    {
        roomServ.ToggleValid((int)SelectedRoom.Id);
        RoomList = roomServ.GetAllRooms();
    }
    public bool CanExecuteToggle(object obj)
    {
        return SelectedRoom!=null;
    }
}