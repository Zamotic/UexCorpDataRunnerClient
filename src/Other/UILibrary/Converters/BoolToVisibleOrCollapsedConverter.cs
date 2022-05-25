﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows;


namespace UexCorpDataRunner.UILibrary.Converters;

[ValueConversion(typeof(Visibility), typeof(Boolean))]
public class BoolToVisibleOrCollapsedConverter : IValueConverter
{
    #region Constructors
    /// <summary>
    /// The default constructor
    /// </summary>
    public BoolToVisibleOrCollapsedConverter() { }
    #endregion

    #region IValueConverter Members
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        bool bValue = (bool)value;
        if (bValue)
            return Visibility.Visible;
        else
            return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        Visibility visibility = (Visibility)value;

        if (visibility == Visibility.Visible)
            return true;
        else
            return false;
    }
    #endregion
}

