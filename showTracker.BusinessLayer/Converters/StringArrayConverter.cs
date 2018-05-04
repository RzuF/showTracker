using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace showTracker.BusinessLayer.Converters
{
    public class StringArrayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<string> enumerable)
            {
                return string.Join(", ", enumerable);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
