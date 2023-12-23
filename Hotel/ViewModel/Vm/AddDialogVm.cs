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
   
    private ChooseCustomerControl cust ;
    private ChooseServicesControl serv;
    private CostService costServ;
    private bool _serviceBilliards; 
    private bool _serviceSauna; 
    private bool _serviceBar; 
    private PickDateControl date;
    private int _totalCost;
    
    private CheckService check;
    private ReservationService resServ;
    private CustomerService custServ;
    private AttributeService attrServ;
    private AdditionalServicesService serviceServ;
   public int TotalCost
   {
       get => _totalCost;
       set
       {
           _totalCost = value;
           OnPropertyChanged(nameof(TotalCost));
       } 
   }
   public bool ServiceBilliards
   {
       get => _serviceBilliards;
       set
       {
           _serviceBilliards = value;
           OnPropertyChanged(nameof(ServiceBilliards));
           UpdateCost();
       } 
   }
   public bool ServiceBar
   {
       get => _serviceBar;
       set
       {
           _serviceBar = value;
           OnPropertyChanged(nameof(ServiceBar));
           UpdateCost();
       } 
   }
   public bool ServiceSauna
   {
       get => _serviceSauna;
       set
       {
           _serviceSauna = value;
           OnPropertyChanged(nameof(ServiceSauna));
           UpdateCost();
       } 
   }
    
    private DisplayReservation _newRes=new DisplayReservation();
    public DisplayReservation NewRes
    {
        get => _newRes;
        set
        {
            _newRes = value;
            OnPropertyChanged(nameof(NewRes));
        } 
    }
    private Customer _newCustomer=new Customer();
    public Customer Customer
    {
        get => _newCustomer;
        set
        {
           if(value is not null) _newCustomer = value;
            OnPropertyChanged(nameof(Customer));
        } 
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
        
        Forward = new VmCommand(ExecuteForwardCommand, CanExecuteForwardCommand);
        Backward = new VmCommand(ExecuteBackwardCommand, CanExecuteBackwardCommand);
       
        date = new PickDateControl();
        date.DataContext = this;
        cust = new ChooseCustomerControl();
        cust.DataContext = this;
        serv = new ChooseServicesControl();
        serv.DataContext = this;
        check = new CheckService();
        resServ = new ReservationService();
        custServ = new CustomerService();
        attrServ = new AttributeService();
        serviceServ = new AdditionalServicesService();
        costServ = new CostService();
    }

   
    private void ExecuteForwardCommand(object obj)
    {
        Grid gr=obj as Grid;
        switch (windowStep)
        {
            case 1:
                date = gr.Children[0] as PickDateControl;
                if (ValidateReservation())
                {
                    windowStep++;
                   gr.Children.Clear();
                  gr.Children.Add(cust);
                }
                break;
            case 2:
                if (ValidateCustomer())
                {
                    windowStep++;
                    UpdateCost();
                    gr.Children.Clear();
                    gr.Children.Add(serv);
                }
                break;
            case 3:
               
                Window win = Window.GetWindow(gr);
                AddNewReservation();
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
    
    private bool ValidateReservation()
    {
        if (_newRes.StartDate == null || _newRes.EndDate == null || _newRes.RoomQuality == null ||
            _newRes.RoomViewType == null || _newRes.RoomCapacity == null)
        {
            MessageBox.Show("Заполните все поля!");
            return false;
        }
        if( _newRes.EndDate < _newRes.StartDate || _newRes.StartDate<new DateOnly(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day))
        {
            MessageBox.Show("Некорректные даты!");
            return false;
        }
        bool flag=check.CheckForFreeRooms(_newRes);
        if(!flag) MessageBox.Show("Свободных комнат нет");
        return flag;
    }
    
    private bool ValidateCustomer()
    {
        if(_newCustomer==null) 
        {
            MessageBox.Show("Заполните все поля!");
            return false;
        }
        if (_newCustomer.Name == null || _newCustomer.Surname == null || _newCustomer.LastName == null ||
            _newCustomer.Email == null || _newCustomer.PhoneNumber == null)
        {
            MessageBox.Show("Заполните все поля!");
            return false;
        }

        return true;
    }

    private void AddNewReservation()
    {
        custServ.AddCustomer(_newCustomer);
        _newRes.CustomerId = custServ.GetId(_newCustomer);
        _newRes.TotalCost = TotalCost;
        resServ.AddReservation(_newRes);
        AddChosenServices(resServ.GetId(_newRes));
    }

    private void UpdateCost()
    {
        TotalCost = costServ.CountTotalCost(NewRes, ServiceBar, ServiceSauna, ServiceBilliards);
    }
    private void CountTotalCost()
    {
       int roomQualityCost = (int)attrServ.GetQualityByName(_newRes.RoomQuality).Price;
       int roomViewCost = (int)attrServ.GetViewByName(_newRes.RoomViewType).Price;
       int roomCapacityCost = (int)attrServ.GetCapacityByValue((int)_newRes.RoomCapacity).Price;
       int duration =_newRes.EndDate.Value.DayNumber - _newRes.StartDate.Value.DayNumber+1;
       TotalCost = duration * (roomQualityCost + roomViewCost + roomCapacityCost)+Convert.ToInt32(ServiceBar)*serviceServ.GetAdditionalServiceByName("Безлимитный бар").Price
           +Convert.ToInt32(ServiceSauna)*serviceServ.GetAdditionalServiceByName("Посещение сауны").Price
           +Convert.ToInt32(ServiceBilliards)*serviceServ.GetAdditionalServiceByName("Бильярд").Price;
    }

    private void AddChosenServices(int resId)
    {
        if (ServiceBar) serviceServ.AddServiceStatement(resId,serviceServ.GetAdditionalServiceByName("Безлимитный бар").Id);
        if (ServiceSauna) serviceServ.AddServiceStatement(resId,serviceServ.GetAdditionalServiceByName("Посещение сауны").Id);;
        if (ServiceBilliards) serviceServ.AddServiceStatement(resId,serviceServ.GetAdditionalServiceByName("Бильярд").Id);;
    }
    private bool CanExecuteBackwardCommand(object obj)
    {
        return true;
    }
}