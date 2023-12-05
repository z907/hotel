using System.Windows;
using System.Windows.Input;
using ViewModel.Services;

namespace Hotel.ViewModel;

public class EditDialogVm
{
    public ICommand Ok
    {
        get;
    }
    public EditDialogVm()
    {
        Ok = new VmCommand(ExecuteOkCommand, CanExecuteOkCommand);
    }
    private void ExecuteOkCommand(object obj)
    {
        Window win =obj as Window;
        win.DialogResult = true;
    }
    private bool CanExecuteOkCommand(object obj)
    {
        return true;
    }
}