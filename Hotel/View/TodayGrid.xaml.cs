using System.ComponentModel;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;
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
   
}