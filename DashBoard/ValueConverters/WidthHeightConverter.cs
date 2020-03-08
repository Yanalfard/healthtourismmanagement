using System;
using System.Globalization;
using System.Windows;

namespace DashBoard.ValueConverters
{
    public class WidthHeightConverter : BaseValueConverter<WidthHeightConverter>
    {
        /// <summary>
        /// Width => Height
        /// </summary>
        /// <param name="value"><see cref="FrameworkElement.Width"/></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
