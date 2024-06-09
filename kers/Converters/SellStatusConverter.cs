using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;
using kers.Models;

namespace kers.Converters;

public class SellStatusConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool check)
        {
            return check switch
            {
                true => null,
                false => "Мест нет!"
            };
        }
        return null;
    }
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}