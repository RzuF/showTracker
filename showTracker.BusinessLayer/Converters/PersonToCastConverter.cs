using System;
using System.Globalization;
using Xamarin.Forms;

namespace showTracker.BusinessLayer.Converters
{
    public class PersonToCastConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"as {value}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
