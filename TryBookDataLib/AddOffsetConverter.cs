using System.Globalization;
using System.Windows.Data;

namespace TryBookDataLib.Views;

public class AddOffsetConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double original && double.TryParse(parameter?.ToString(), out double offset))
            return original + offset;

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        Binding.DoNothing;
}
