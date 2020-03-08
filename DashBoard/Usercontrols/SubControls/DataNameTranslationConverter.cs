using DashBoard.Models;
using System;
using System.Globalization;

namespace DashBoard
{
    /// <summary>
    /// Converter to translate data names from eng to persian
    /// </summary>
    public class DataNameTranslationConverter : BaseValueConverter<DataNameTranslationConverter>
    {
        private ModelUtility _modelUtility = new ModelUtility();
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string ans = "";
            _modelUtility.ArrTableLabel.TryGetValue(value.ToString(), out ans);
            return ans;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
