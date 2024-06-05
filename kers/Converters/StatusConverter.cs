using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace kers.Converters;

public class StatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int status)
        {
            return status switch
            {
                1 => Brushes.Green,
                2 => Brushes.Aqua,
                3 => Brushes.Gold,
                4 => Brushes.DarkGreen,
                5 => Brushes.Gray,
                6 => Brushes.DarkRed,
                _ => Brushes.Black
            };
        }

        return Brushes.Black;
    }
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}