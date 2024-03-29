﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace View.Converters;

[ValueConversion(typeof(float), typeof(float))]
internal sealed class BrightnessToPercent : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => Math.Round(System.Convert.ToSingle(value) * 100);
    

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => System.Convert.ToSingle(value) * 0.01;
}