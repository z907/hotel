using System.Windows;
using System.Windows.Input;

namespace Hotel.View;

public partial class PayDialog : Window
{
    public PayDialog()
    {
        InitializeComponent();
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
    private void OkClick(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
    }
}