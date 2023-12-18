using System.Windows;
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
    
}