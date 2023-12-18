using System.Windows;
using System.Windows.Input;
using Model.Entities;
using ViewModel.Services;

namespace Hotel.ViewModel;

public class ChooseExistingVm:BaseVm
{
    private CustomerService _customerService;
    private List<Customer> _customers;
   
    public List<Customer> Customers
    {
        get => _customers;
        set
        {
            _customers = value;
            OnPropertyChanged(nameof(Customers));
        }
    }
    public ICommand OK
    {
        get;
    }
    public ChooseExistingVm()
    {
        OK = new VmCommand(ExecOk, CanExecOk);
        _customerService = new CustomerService();
        _customers = _customerService.GetCustomerList();
    }
    
    private void ExecOk(object obj)
    {
            Window win = obj as Window;
            win.DialogResult = true;
    }
    private bool CanExecOk(object obj)
    {
        return true;
    }
}