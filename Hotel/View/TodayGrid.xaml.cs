using System.ComponentModel;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Hotel.Global;
using Hotel.ViewModel;


namespace Hotel.View;

public partial class TodayPanel : UserControl
{
    public TodayPanel()
    {
        InitializeComponent();
        this.TodayDataGrid.AutoGeneratingColumn += AutoGenerateHandler.HideIdOnAutogeneration;
        this.TodayDataGrid.AutoGeneratingColumn += AutoGenerateHandler.RenameColumnsOnAutogeneration;
    }

    private void dataGrid1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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