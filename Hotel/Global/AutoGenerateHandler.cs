using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;


namespace Hotel.ViewModel;

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
        e.Column.Header = property.DisplayName;
    }
}