using System.Windows;
using System.Windows.Input;
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
    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }
    private void CloseClick(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
    }
   
}