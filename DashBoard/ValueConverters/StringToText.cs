using System;
using System.Globalization;

namespace DashBoard.ValueConverters
{
    public class StringToText : BaseValueConverter<StringToText>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
