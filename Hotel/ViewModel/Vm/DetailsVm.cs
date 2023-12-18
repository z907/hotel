using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Hotel.View;
using Hotel.VmEntities;
using Model.Entities;
using ViewModel.Services;

namespace Hotel.ViewModel;

public class DetailsVm:BaseVm
{
    private ReservationService resServ;
    private CustomerService custServ;
    private AdditionalServicesService serviceServ;
    private CheckService checkServ;
    private CostService costServ;
    private AttributeService attrServ;

    private bool isCustomerNew = false;
    private bool _serviceBilliards; 
    private bool _serviceSauna; 
    private bool _serviceBar;
    private  bool _Editable=false;
    private Visibility _addButtonVisibility=Visibility.Collapsed;
    
    public Visibility AddButtonVisibility
    {
        get => _addButtonVisibility;
        set
        {
            _addButtonVisibility = value;
            OnPropertyChanged(nameof(AddButtonVisibility));
        } 
    }
    public bool Editable
    {
        get => _Editable;
        set
        {
            _Editable = value;
            if (value) AddButtonVisibility = Visibility.Visible;
            else  AddButtonVisibility = Visibility.Collapsed;
            OnPropertyChanged(nameof(Editable));
        } 
    }
    public DetailsVm()
    {
        OK = new VmCommand(ExecOk, CanExecOk);
        ChooseExisting = new VmCommand(ExecuteChooseExisting,CanExecuteChooseExisting);
        CreateNewCustomer = new VmCommand(ExecuteCreateNewCustomer, CanExecuteCreateNewCustomer);
        resServ = new ReservationService();
        checkServ = new CheckService();
        custServ = new CustomerService();
        serviceServ = new AdditionalServicesService();
        costServ = new CostService();
        attrServ = new AttributeService();
    }
    public bool ServiceBilliards
    {
        get => _serviceBilliards;
        set
        {
            _serviceBilliards = value;
            UpdateCost();
            OnPropertyChanged(nameof(ServiceBilliards));
        } 
    }
    public bool ServiceBar
    {
        get => _serviceBar;
        set
        {
            _serviceBar = value;
            UpdateCost();
            OnPropertyChanged(nameof(ServiceBar));
        } 
    }
    public bool ServiceSauna
    {
        get => _serviceSauna;
        set
        {
            _serviceSauna = value;
            UpdateCost();
            OnPropertyChanged(nameof(ServiceSauna));
        } 
    }
    
    private DisplayReservation _reservation;
    public DisplayReservation Reservation
    {
        get => _reservation;
        set
        {
            _reservation = value;
            UpdateCost();
            OnPropertyChanged(nameof(Reservation));
        } 
    }

    public DateOnly? StartDate
    {
        get => _reservation.StartDate;
        set
        {
            _reservation.StartDate = value;
            UpdateCost();
            OnPropertyChanged(nameof(Reservation));
        } 
    }
    public DateOnly? EndDate
    {
        get => _reservation.EndDate;
        set
        {
            _reservation.EndDate = value;
            UpdateCost();
            OnPropertyChanged(nameof(Reservation));
        } 
    }
    private Customer _customer;
    public Customer Customer
    {
        get => _customer;
        set
        {
            if(value is not null) _customer = value;
            OnPropertyChanged(nameof(Customer));
           
        } 
    }
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
    public void LoadData(int resId)
    {
        LoadComboBoxes();
        LoadEntity(resId);
        LoadServices(resId);
    }

    private void LoadServices(int resId)
    {
        var list = serviceServ.GetAdditionalServicesForReservation(resId);
        ServiceBar=list.Exists(r => r.Service.Name == "Безлимитный бар");
        ServiceSauna=list.Exists(r => r.Service.Name == "Посещение сауны");
        ServiceBilliards=list.Exists(r => r.Service.Name == "Бильярд");
    }
    private void LoadEntity(int resId)
    {
        Reservation = resServ.GetReservationById(resId);
        Customer = custServ.GetCustomerById((int)Reservation.CustomerId);
    }
    private void LoadComboBoxes()
    {
        attrServ = new AttributeService();
        cap = attrServ.GetCapacities();
        qal = attrServ.GetQualities();
        rview = attrServ.GetViews();
    }
    public ICommand OK
    {
        get;
    }
    public ICommand ChooseExisting
    {
        get;
    }

    public ICommand CreateNewCustomer
    {
        get;
    }
    private void ExecuteCreateNewCustomer(object obj)
    {
        Customer = new Customer();
        isCustomerNew = true;
    }
    private bool CanExecuteCreateNewCustomer(object obj)
    {
        return true;
    }
    private void ExecuteChooseExisting(object obj)
    {
        ChooseExistingCustomer chooseEx = new ChooseExistingCustomer();
        chooseEx.DataContext = this;
        if (chooseEx.ShowDialog() == true)
        {
            BindingExpression be = chooseEx.gr.GetBindingExpression(DataGrid.SelectedItemProperty);
            be.UpdateSource();
            isCustomerNew = false;
        }
    }
    private bool CanExecuteChooseExisting(object obj)
    {
        return true;
    }
    private void ExecOk(object obj)
    {
        if (!Editable) (obj as Window).DialogResult = true;
        else
        {
            if(UpdateReservation()) (obj as Window).DialogResult = true;
        }
    }
    private bool CanExecOk(object obj)
    {
        return true;
    }

    private void UpdateCost()
    {
        Reservation.TotalCost = costServ.CountTotalCost(Reservation, ServiceBar, ServiceSauna, ServiceBilliards);
        OnPropertyChanged(nameof(Reservation));
    }
    private bool UpdateReservation()
    {
        if (ValidateInput())
        {
            if (checkServ.CheckForFreeRoomsUpdate(Reservation))
            {
                if(isCustomerNew) custServ.AddCustomer(Customer);
                custServ.GetId(Customer);
                resServ.UpdateReservation(Reservation);
                serviceServ.RemakeServices(ServiceBar, ServiceSauna, ServiceBilliards,(int)Reservation.Id);
                return true;
            }
        }
       return false;
    }

    private bool ValidateInput()
    {
        if (Customer == null || Reservation == null)
        {
            MessageBox.Show("Заполните поля!");
            return false;
        }

        if (Customer.Name == "" || Customer.Surname == "" || Customer.LastName == ""
            || Customer.PhoneNumber == "" || Customer.Email == "" || Customer.Name == null || Customer.Surname == null || Customer.LastName == null
            || Customer.PhoneNumber == null || Customer.Email == null || Reservation.Id == null
            || Reservation.RoomQuality == "" || Reservation.RoomViewType == "" || Reservation.RoomQuality == null || Reservation.RoomViewType == null || Reservation.RoomCapacity == null
            || Reservation.TotalCost == null || Reservation.StartDate == null || Reservation.EndDate == null)
        {
            MessageBox.Show("Заполните поля!");
            return false;
        }
        if(Reservation.StartDate>Reservation.EndDate) 
        {
            MessageBox.Show("Некорректные даты!");
            return false;
        }
        return true;
    }
}