using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Hotel.ViewModel.VmEntities;
using Microsoft.Win32;
using Model.Entities;
using ViewModel.Services;
using MessageBox = System.Windows.Forms.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Hotel.ViewModel;

public class AddRoomDialogVm:BaseVm
{
    private List<RoomCapacity> cap;
    private List<RoomViewType> rview;
    private List<RoomQuality> qal;
    private AttributeService attr;
    private DisplayRoom _newRoom=new DisplayRoom();
    private RoomService roomServ = new RoomService();
    private BitmapImage selImage;
    public BitmapImage SelImage
    {
        get => selImage;
        set
        {
            selImage = value;
            OnPropertyChanged(nameof(SelImage));
        } 
    }
    public DisplayRoom NewRoom
    {
        get => _newRoom;
        set
        {
            _newRoom = value;
            OnPropertyChanged(nameof(NewRoom));
        } 
    }
    
    public ICommand SelectImage { get; }
    public ICommand OK { get; }

    private void ExecuteOkCommand(object obj)
    {
        if (ValidateInput())
        {
            roomServ.AddRoom(NewRoom);
            (obj as Window).DialogResult = true;
        }
    }
    private bool CanExecuteOkCommand(object obj)
    {
        return true;
    }
    
    private void ExecuteSelectImage(object obj)
    {
        OpenFileDialog fileDialog = new OpenFileDialog();
        fileDialog.Filter = "Image files|*.bmp;*.jpg;*.png;*.jpeg;";
        fileDialog.FilterIndex = 1;

        if (fileDialog.ShowDialog() == true)
        {
            NewRoom.Image = RoomService.LoadImage(File.ReadAllBytes(fileDialog.FileName)) ;
            SelImage = NewRoom.Image;
        }
    }
    
    private bool CanExecuteSelectImage(object obj)
    {
        return true;
    }
    
    private bool ValidateInput()
    {
        if (NewRoom == null)
        {
            MessageBox.Show("Заполните поля!");
            return false;
        }
        if (NewRoom.Number == null || NewRoom.Image == null 
            || NewRoom.Quality == null || NewRoom.Capacity == null || NewRoom.ViewType == null)
        {
            MessageBox.Show("Заполните поля!");
            return false;
        }
        if(roomServ.CheckNumber((int)NewRoom.Number))
        {
            MessageBox.Show("Комната с этим номером уже существует");
            return false;
        }

        return true;
    }
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
    public AddRoomDialogVm()
    {
        LoadComboBoxes();
        OK = new VmCommand(ExecuteOkCommand, CanExecuteOkCommand);
        SelectImage = new VmCommand(ExecuteSelectImage, CanExecuteSelectImage);
    }
    private void LoadComboBoxes()
    {
        attr = new AttributeService();
        cap = attr.GetCapacities();
        qal = attr.GetQualities();
        rview = attr.GetViews();
    }
}