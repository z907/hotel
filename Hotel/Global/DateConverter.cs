using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Hotel.Global;

public class DateConverter:IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        DateOnly? d1 = value as DateOnly?;
        DateOnly d2;
        if (d1 != null)
           d2= (DateOnly)d1;
        else return null;
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