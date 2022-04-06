﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace View.Converters;

[ValueConversion(typeof(int), typeof(string))]
internal sealed class MinConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var min = (byte)value;
        var result = System.Convert.ToString(min);

        if (result.Length < 2)
        {
            result = $"0{result}";
        }

        return result;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var valid = byte.TryParse((string)value, out var min);

        if (!valid)
        {
            return DependencyProperty.UnsetValue;
        }

        if (min is > 60 or < 0)
        {
            min = 60;
        }

        return min;
    }
}