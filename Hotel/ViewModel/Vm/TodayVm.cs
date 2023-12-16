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
    
    private List<DisplayReservation> _todayList;
    private DisplayReservation _selectedRes;
    
    
    public DisplayReservation SelectedRes
    {
        get => _selectedRes;
        set
        {
            _selectedRes = value;
            OnPropertyChanged(nameof(SelectedRes));
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
        AddDialog add = new AddDialog();
      
        
        if (add.ShowDialog() == true)
        {
            TodayList = res.GetTodayReservation();
        }
    }
    private bool CanExecuteAddCommand(object obj)
    {
        return true;
    }

}