using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Hotel.View;
using Hotel.VmEntities;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Model.Entities;
using ViewModel.Services;

namespace Hotel.ViewModel;

public class AddDialogVm:BaseVm
{
    private int windowStep=1;
    private CheckService check;
    public ChooseCustomerControl cust ;
    public ChooseServicesControl serv;
    public PickDateControl date;
    private DisplayReservation res=new DisplayReservation();
   
    
    public DisplayReservation Res
    {
        get;
        set;
    }
    public ICommand Forward
    {
        get;
    }
    public ICommand Backward
    {
        get;
    }
    public AddDialogVm()
    {
        check = new CheckService();
        Forward = new VmCommand(ExecuteForwardCommand, CanExecuteForwardCommand);
        Backward = new VmCommand(ExecuteBackwardCommand, CanExecuteBackwardCommand);
        date = new PickDateControl();
        //date.Resources.Add("Advm",this);
        cust = new ChooseCustomerControl();
        serv = new ChooseServicesControl();
        
    }
    
    private void ExecuteForwardCommand(object obj)
    {
        Grid gr=obj as Grid;
        switch (windowStep)
        {
            case 1:
                date = gr.Children[0] as PickDateControl;
                res.StartDate=DateOnly.FromDateTime(date.d1.SelectedDate.Value);
                res.EndDate=DateOnly.FromDateTime(date.d2.SelectedDate.Value);
                //check for correct date
                res.RoomViewType =date.c1.SelectedValue.ToString();
                res.RoomQuality = date.c2.SelectedValue.ToString();
                res.RoomCapacity = Convert.ToInt32(date.c3.SelectedValue);
                if (check.CheckForFreeRooms(res))
                {
                    windowStep++;
                    date=gr.Children[0] as PickDateControl;
                   gr.Children.Clear();
                  gr.Children.Add(cust);
                }
                else
                {
                    MessageBox.Show("Свободных комнат нет");
                }
               
                break;
            case 2: 
                windowStep++;
                cust=gr.Children[0] as ChooseCustomerControl;
                gr.Children.Clear();
                gr.Children.Add(serv);
                break;
            case 3:
               
                Window win = Window.GetWindow(gr);
                win.DialogResult = true;
                break;
        }
    }
    private bool CanExecuteForwardCommand(object obj)
    {
        return true;
    }
    private void ExecuteBackwardCommand(object obj)
    {
        Grid gr=obj as Grid;
        switch (windowStep)
        {
            case 1:
               
                Window win = Window.GetWindow(gr);
                win.DialogResult = false;
                break;
            case 2:
                windowStep--;
                cust=gr.Children[0] as ChooseCustomerControl;
                gr.Children.Clear();
                 gr.Children.Add(date);
                break;
            case 3:
                windowStep--;
                serv=gr.Children[0] as ChooseServicesControl;
                gr.Children.Clear();
                 gr.Children.Add(cust);
                break;
        }
    }
    private bool CanExecuteBackwardCommand(object obj)
    {
        return true;
    }
}