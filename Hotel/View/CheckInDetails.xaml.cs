using System.Windows;
using System.Windows.Input;
using Hotel.ViewModel;

namespace Hotel.View;

public partial class CheckInDetails : Window
{
    public CheckInDetails()
    {
        InitializeComponent();
    }
    public CheckInDetails(int chId)
    {
        InitializeComponent();
        (this.DataContext as CheckInDetailsVm).Load(chId);
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