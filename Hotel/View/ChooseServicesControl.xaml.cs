using System.Windows.Controls;
using Hotel.ViewModel;

namespace Hotel.View;

public partial class ChooseServicesControl : UserControl
{
    public ChooseServicesControl()
    {
        InitializeComponent();
    }
    // public ChooseServicesControl(Action recount)
    // {
    //     InitializeComponent();
    //     ChooseServicesVm csv = this.Resources["csv"] as ChooseServicesVm;
    //     csv.SetRecountAction(recount);
    // }
}