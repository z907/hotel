using System.Windows;
using Hotel.ViewModel;

namespace Hotel;

public partial class AddDialog : Window
{
    private AddDialogVm advm;
    public AddDialog(TodayVm tvm)
    {
        InitializeComponent();
        this.DataContext = tvm;
        advm = this.Resources["Advm"] as AddDialogVm;
        advm.date.DataContext = tvm;
        advm.cust.DataContext = tvm;
        advm.serv.DataContext = tvm;
    }
    
   
}