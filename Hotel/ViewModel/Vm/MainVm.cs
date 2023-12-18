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
    
   
    public ICommand ShowTodayListCommand
    {
        get;
    }
    public MainVm()
    {
        UserName = Thread.CurrentPrincipal.Identity.Name;
        ShowTodayListCommand = new VmCommand(ExecuteShowTodayListCommand, CanExecuteShowTodayListCommand);
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