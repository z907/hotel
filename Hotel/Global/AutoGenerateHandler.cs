using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;


namespace Hotel.Global;

public static class AutoGenerateHandler
{
    public static void HideIdOnAutogeneration(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        if (e.PropertyName.Contains("Id"))
        {
            e.Column.Visibility =  Visibility.Hidden;
        }
    }
    public static void RenameColumnsOnAutogeneration(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        //  get property descriptions
        PropertyDescriptor pd = e.PropertyDescriptor as PropertyDescriptor;
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties (pd.ComponentType );
        //  get specific descriptor
        PropertyDescriptor property = properties.Find ( e.PropertyName, false );
        if (property.DisplayName != property.Name) e.Column.Header = property.DisplayName;
        else e.Column.Visibility = Visibility.Hidden;
        if (e.PropertyType == typeof(DateOnly?))
        {
            (e.Column as DataGridTextColumn).Binding.StringFormat="dd.MM.yyyy";
        }
    }
}