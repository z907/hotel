using System.Windows;
using Hotel.ViewModel;

namespace Hotel;

public partial class EditDialog : Window
{
    public EditDialog()
    {
        InitializeComponent();
    }
    public EditDialog(int resId)
    {
        InitializeComponent();
        (this.DataContext as DetailsVm).LoadData(resId);
        (this.DataContext as DetailsVm).Editable = true;
    }
   
}