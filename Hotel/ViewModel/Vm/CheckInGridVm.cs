using System.Windows.Forms;
using System.Windows.Input;
using Hotel.View;
using Hotel.ViewModel.VmEntities;
using Hotel.VmEntities;
using ViewModel.Services;

namespace Hotel.ViewModel;

public class CheckInGridVm:BaseVm
{
    private CheckInService chIn;
    private PayService payServ;
    
    private List<CheckIn> _totalList;
    private CheckIn _selectedCheckIn;
    
    
    public CheckIn SelectedCheckIn
    {
        get => _selectedCheckIn;
        set
        {
            _selectedCheckIn = value;
            OnPropertyChanged(nameof(SelectedCheckIn));
           
        } 
    }
    
    public ICommand Edit
    {
        get;
    }
    public ICommand Pay
    {
        get;
    }
    public ICommand Finish
    {
        get;
    }
    public ICommand Details
    {
        get;
    }
    public List<CheckIn> TotalList
    {
        get => _totalList;
        set 
        {
            _totalList = value;
            OnPropertyChanged(nameof(TotalList));
           
        } 
    }
    public CheckInGridVm()
    {
        chIn = new CheckInService();
        payServ = new PayService();
        TotalList = chIn.GetAllCheckIns();
        Edit = new VmCommand(ExecuteEditCommand, CanExecuteEditCommand);
        Pay = new VmCommand(ExecutePayCommand, CanExecutePayCommand);
        Finish = new VmCommand(ExecuteFinishCommand, CanExecuteFinishCommand);
        Details = new VmCommand(ExecuteDetailsCommand, CanExecuteDetailsCommand);
    }
    
  
    private void ExecuteDetailsCommand(object obj)
    {
        CheckInDetails details = new CheckInDetails((int)SelectedCheckIn.Id);
        if (details.ShowDialog() == true)
        {
        }
    }

    private bool CanExecuteDetailsCommand(object obj)
    {
        return SelectedCheckIn!=null;
    }
    private void ExecuteFinishCommand(object obj)
    {
        if (SelectedCheckIn.Status == "Оплачено")
        {
            if(SelectedCheckIn.EndDate<=DateOnly.FromDateTime(DateTime.Today))
            {
                FinishConfirmationDialog win = new FinishConfirmationDialog();
                if (win.ShowDialog() == true)
                {
                    chIn.FinishCheckIn((int)SelectedCheckIn.Id);
                    TotalList = chIn.GetAllCheckIns();
                }
            }
            else MessageBox.Show("Пока рано выселять!");
        }
        else MessageBox.Show("Сначала нужно оплатить!");
    }

    private bool CanExecuteFinishCommand(object obj)
    {
        return SelectedCheckIn!=null;
    }
    private void ExecuteEditCommand(object obj)
    {
        
    }
    private bool CanExecuteEditCommand(object obj)
    {
        return SelectedCheckIn!=null;
    }

    private int _paySum;

    public int PaySum
    {
        get => _paySum;
        set 
        {
            _paySum = value;
            OnPropertyChanged(nameof(PaySum));
           
        } 
    }
    private void ExecutePayCommand(object obj)
    {
        if (SelectedCheckIn.ToPay != 0)
        {
            PayDialog win = new PayDialog();
            win.DataContext = this;
            if (win.ShowDialog() == true)
            {
                payServ.Pay((int)SelectedCheckIn.Id,_paySum);
                TotalList = chIn.GetAllCheckIns();
            }
        }
        else MessageBox.Show("Уже оплачено!");
    }
    private bool CanExecutePayCommand(object obj)
    {
        return SelectedCheckIn!=null;
    }
}