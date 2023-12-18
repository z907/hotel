using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Hotel.Global;
using Hotel.ViewModel;

namespace Hotel.View;

public partial class СheckInDialog : Window
{
    public СheckInDialog()
    {
        InitializeComponent();
    }
    public СheckInDialog(int resId)
    {
        InitializeComponent();
        (this.DataContext as CheckInVm).LoadRooms(resId);
        //this.gr.AutoGeneratingColumn += AutoGenerateHandler.RenameColumnsOnAutogeneration;
    }
    private void Gr_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (sender != null)
        {
            DataGrid grid = sender as DataGrid;
            if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
            {
                DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                if (!dgr.IsMouseOver)
                {
                    (dgr as DataGridRow).IsSelected = false;
                }
            }
        }
    }
}