using System;
using System.Windows.Data;


namespace UexCorpDataRunner.UILibrary.Converters;

[ValueConversion(typeof(bool), typeof(int))]
public class CollectionCountToBooleanConverter : IValueConverter
{
    #region Constructors
    /// <summary>
    /// The default constructor
    /// </summary>
    public CollectionCountToBooleanConverter() { }
    #endregion

    #region IValueConverter Members
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value == null)
        {
            return false;
        }
        int? valueAsInt = value as int?;
        if (valueAsInt.HasValue == false)
        {
            return false;
        }
        if (valueAsInt.Value == 0)
        {
            return false;
        }
        return true;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
    #endregion
}

