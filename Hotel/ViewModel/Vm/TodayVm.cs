using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Hotel.VmEntities;
using Model.Context;
using Model.Entities;
using Ninject;
using ViewModel.Services;

namespace Hotel.ViewModel;

public class TodayVm:BaseVm
{
    private ReservationService res;
    private AttributeService attr;
    private List<DisplayReservation> _todayList;
    private DisplayReservation _selectedRes;
    
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
    public DisplayReservation SelectedRes
    {
        get => _selectedRes;
        set
        {
            _selectedRes = value;
            OnPropertyChanged(nameof(SelectedRes));
        } 
    }
    private DisplayReservation _newRes;
    public DisplayReservation NewRes
    {
        get => _newRes;
        set
        {
            _newRes = value;
            OnPropertyChanged(nameof(NewRes));
        } 
    }
    public ICommand Edit
    {
        get;
    }
    public ICommand Add
    {
        get;
    }
    public List<DisplayReservation> TodayList
    {
        get => _todayList;
        set => _todayList = value;
    }
    public TodayVm()
    {
        res = new ReservationService();
        TodayList = res.GetTodayReservation();
        Edit = new VmCommand(ExecuteEditCommand, CanExecuteEditCommand);
        Add = new VmCommand(ExecuteAddCommand, CanExecuteAddCommand);
        attr = new AttributeService();
        cap = attr.GetCapacities();
        qal = attr.GetQualities();
        rview = attr.GetViews();
    }
    private void ExecuteEditCommand(object obj)
    {
        EditDialog edit = new EditDialog();
       
        edit.DataContext = this;
        if (edit.ShowDialog() == true)
        {
            int a = 0;
            //HANDLE NEW DATA
            //EDIT IN DATABASE
        }
    }
    private bool CanExecuteEditCommand(object obj)
    {
        return true;
    }
    
    private void ExecuteAddCommand(object obj)
    {
        AddDialog add = new AddDialog(this);
        NewRes = new DisplayReservation();
        
        if (add.ShowDialog() == true)
        {
            //HANDLE NEW DATA
            //EDIT IN DATABASE
        }
    }
    private bool CanExecuteAddCommand(object obj)
    {
        return true;
    }

}