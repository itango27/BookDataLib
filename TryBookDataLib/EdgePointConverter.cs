using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TryBookDataLib.Views;

public class EdgePointConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is Point p)
            return parameter switch
            {
                "X" => p.X,
                "Y" => p.Y,
                _ => 0
            };

        return 0;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
