using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Hotel.View;
using Model.Entities;
using ViewModel.Services;

namespace Hotel.ViewModel;

public class MainVm:BaseVm
{
    private string _userName;
    private bool _isUserAdmin;
    private UserService logServ=new UserService();
    public bool IsUserAdmin
    {
        get => _isUserAdmin;
        set
        {
            _isUserAdmin = value;
            OnPropertyChanged(nameof(IsUserAdmin));
        }
    }
   
    public ICommand ShowTodayListCommand
    {
        get;
    }
    public ICommand ShowCheckIns
    {
        get;
    }
    public ICommand OccupancyReport
    {
        get;
    }
    public ICommand IncReport
    {
        get;
    }
    public ICommand ShowRooms
    {
        get;
    }
    public MainVm()
    {
        UserName = Thread.CurrentPrincipal.Identity.Name;
        ShowTodayListCommand = new VmCommand(ExecuteShowTodayListCommand, CanExecuteShowTodayListCommand);
        ShowCheckIns = new VmCommand(ExecuteShowCheckIns, CanExecuteShowCheckIns);
        OccupancyReport = new VmCommand(ShowOccupancyReport,CanShowOccupancyReport);
        IncReport=new VmCommand(ShowIncomeReport,CanShowIncomeReport);
        ShowRooms=new VmCommand(ShowRoomsCommand,CanShowRoomsCommand);
        IsUserAdmin = CheckRole();
    }

    private bool CheckRole()
    {
       return Thread.CurrentPrincipal.IsInRole("Администратор");
    }
    private void ShowRoomsCommand(object obj)
    {
        Grid gr = obj as Grid;
        gr.Children.Clear();
        RoomGrid item = new RoomGrid();
        gr.Children.Add(item);
    }
    private bool CanShowRoomsCommand(object obj)
    {
        return true;
    }
    private void ShowIncomeReport(object obj)
    {
        Grid gr = obj as Grid;
        gr.Children.Clear();
        IncomeReport item = new IncomeReport();
        gr.Children.Add(item);
    }
    private bool CanShowIncomeReport(object obj)
    {
        return true;
    }
    private void ShowOccupancyReport(object obj)
    {
        Grid gr = obj as Grid;
        gr.Children.Clear();
        OccupancyReport item = new OccupancyReport();
        gr.Children.Add(item);
    }
    private bool CanShowOccupancyReport(object obj)
    {
        return true;
    }
    private void ExecuteShowCheckIns(object obj)
    {
        Grid gr = obj as Grid;
        gr.Children.Clear();
        CheckInGrid item = new CheckInGrid();
        gr.Children.Add(item);
    }
    private bool CanExecuteShowCheckIns(object obj)
    {
        return true;
    }
    private void ExecuteShowTodayListCommand(object obj)
    {
         Grid gr = obj as Grid;
         gr.Children.Clear();
        TodayPanel item = new TodayPanel();
         gr.Children.Add(item);
    }
    private bool CanExecuteShowTodayListCommand(object obj)
    {
        return true;
    }
    
    public string UserName
    {
        get => _userName;
        set => _userName = value;
    }
}