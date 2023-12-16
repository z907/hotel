using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Hotel.View;

namespace Hotel.ViewModel;

public class ChooseCustomerVm:BaseVm
{
    public ChooseCustomerVm()
    {
        ChooseExistingCommand = new VmCommand(ExecuteChooseExistingCommand, CanExecuteChooseExistingCommand);
    }

    
    public ICommand ChooseExistingCommand
    {
        get;
    }
    private void ExecuteChooseExistingCommand(object obj)
    {
        ChooseExistingCustomer chooseEx = new ChooseExistingCustomer();
        chooseEx.DataContext = obj;
        if (chooseEx.ShowDialog() == true)
        {
            BindingExpression be = chooseEx.gr.GetBindingExpression(DataGrid.SelectedItemProperty);
            be.UpdateSource();
        }
        
    }
    private bool CanExecuteChooseExistingCommand(object obj)
    {
        return true;
    }
}