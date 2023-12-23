using System.Windows;
using System.Windows.Input;

namespace Hotel.View;

public partial class FinishConfirmationDialog : Window
{
    public FinishConfirmationDialog()
    {
        InitializeComponent();
    }
    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }
}