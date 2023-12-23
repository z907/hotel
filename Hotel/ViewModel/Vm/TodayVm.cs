using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Hotel.Global;
using Hotel.View;
using Hotel.VmEntities;
using Model.Context;
using Model.Entities;
using Ninject;
using ViewModel.Services;

namespace Hotel.ViewModel;

public class TodayVm:BaseVm
{
    private ReservationService res;
    private MidnightTimer _timer = new MidnightTimer();
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
    public ICommand Delete
    {
        get;
    }
    public ICommand Details
    {
        get;
    }
    public ICommand CheckIn
    {
        get;
    }
    public List<DisplayReservation> TodayList
    {
        get => _todayList;
        set 
        {
            _todayList = value;
            OnPropertyChanged(nameof(TodayList));
           
        } 
    }
    public TodayVm()
    {
        res = new ReservationService();
        res.CancelExpiredReservations();
        TodayList = res.GetTodayReservation();
        Edit = new VmCommand(ExecuteEditCommand, CanExecuteEditCommand);
        Add = new VmCommand(ExecuteAddCommand, CanExecuteAddCommand);
        Delete = new VmCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
        Details = new VmCommand(ExecuteDetailsCommand, CanExecuteDetailsCommand);
        CheckIn= new VmCommand(ExecuteCheckInCommand, CanExecuteCheckInCommand);
        _timer.TimeReached += new TimeReachedEventHandler(this.MidnightReached);
        _timer.Start();
    }

    private void MidnightReached(DateTime Time)
    {
        res.CancelExpiredReservations();
        TodayList = res.GetTodayReservation();
    }
    private void ExecuteCheckInCommand(object obj)
    {
        if (SelectedRes.StartDate.Value.Year != DateTime.Now.Year || SelectedRes.StartDate.Value.Month !=DateTime.Now.Month|| SelectedRes.StartDate.Value.Day != DateTime.Now.Day)
        {
            MessageBox.Show("Невозможно заселить!");
            return;
        }
        СheckInDialog checkDialog = new СheckInDialog((int)_selectedRes.Id);
        if (checkDialog.ShowDialog() == true)
        {
           TodayList = res.GetTodayReservation();
        }
    }

    private bool CanExecuteCheckInCommand(object obj)
    {
        return SelectedRes!=null;
    }
    private void ExecuteDetailsCommand(object obj)
    {
        DetailsWindow dwin = new DetailsWindow((int)_selectedRes.Id);
        if (dwin.ShowDialog() == true)
        {
        }
    }

    private bool CanExecuteDetailsCommand(object obj)
    {
        return SelectedRes!=null;
    }
    private void ExecuteDeleteCommand(object obj)
    {
        DeleteConfirmationDialog deleteDialog = new DeleteConfirmationDialog();
        if (deleteDialog.ShowDialog() == true)
        {
            res.CancelReservation((int)_selectedRes.Id);
            TodayList = res.GetTodayReservation();
        }
    }

    private bool CanExecuteDeleteCommand(object obj)
    {
        return SelectedRes!=null;
    }
    private void ExecuteEditCommand(object obj)
    {
        EditDialog edit = new EditDialog((int)_selectedRes.Id);
        if (edit.ShowDialog() == true)
        {
            TodayList = res.GetTodayReservation();
        }
    }
    private bool CanExecuteEditCommand(object obj)
    {
        return SelectedRes!=null;
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