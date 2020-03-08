using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace DashBoard
{
    /// <summary>
    /// A Base Value Converter structure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
        where T: class, new()
    {
        /// <summary>
        /// A single static instance of this value converter
        /// </summary>
        private T _mConverter = null;

        /// <summary>
        /// provides a static instance of the value converter
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _mConverter ?? (_mConverter = new T());
        }

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
