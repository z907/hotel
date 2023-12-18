using System.Globalization;
using System.Windows.Data;

namespace Hotel.Global;

public class BoolInverseConverter:IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return !(value as bool?);
    }
         
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return !(value as bool?);
    }
}