using System.Windows;
using Hotel.Global;
using Hotel.ViewModel;

namespace Hotel.View;

public partial class ChooseExistingCustomer : Window
{
    public ChooseExistingCustomer()
    {
        InitializeComponent();
        gr.AutoGeneratingColumn += AutoGenerateHandler.RenameColumnsOnAutogeneration;
    }
    
}