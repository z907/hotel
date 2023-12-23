using System.Windows;
using System.Windows.Input;
using Hotel.ViewModel;

namespace Hotel;

public partial class AddDialog : Window
{
    
    public AddDialog()
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
   
}