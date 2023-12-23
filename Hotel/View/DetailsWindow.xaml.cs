using System.Windows;
using System.Windows.Input;
using Hotel.ViewModel;
using ViewModel.Services;

namespace Hotel.View;

public partial class DetailsWindow : Window
{
   
    public DetailsWindow()
    {
        InitializeComponent();
    }
    public DetailsWindow(int id)
    {
        InitializeComponent();
        (this.DataContext as DetailsVm).LoadData(id);
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