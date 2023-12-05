using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Hotel.Global;

public class DateConverter:IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        DateOnly? d1 = value as DateOnly?;
        var d2 = (DateOnly)d1;
       return d2.ToDateTime(new TimeOnly(0));
    }
         
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        DateTime? d1 = value as DateTime?;
        if (d1.HasValue)
            return DateOnly.FromDateTime((DateTime)d1);
        else return null;
    }
}