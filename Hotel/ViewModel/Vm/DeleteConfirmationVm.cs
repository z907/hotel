using System.Windows;
using System.Windows.Input;

namespace Hotel.ViewModel;

public class DeleteConfirmationVm:BaseVm
{
    public DeleteConfirmationVm()
    {
        Confirm = new VmCommand(ExecuteConfirmCommand, CanExecuteConfirmCommand);
        Cancel = new VmCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
    }
    public ICommand Confirm
    {
        get;
    }
    public ICommand Cancel
    {
        get;
    }
    private void ExecuteConfirmCommand(object obj)
    {
        (obj as Window).DialogResult = true;
    }

    private bool CanExecuteConfirmCommand(object obj)
    {
        return true;
    }
    private void ExecuteCancelCommand(object obj)
    {
        (obj as Window).DialogResult = false;
    }

    private bool CanExecuteCancelCommand(object obj)
    {
        return true;
    }
}