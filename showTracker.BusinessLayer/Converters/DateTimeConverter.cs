using System;
using System.Globalization;
using Xamarin.Forms;

namespace showTracker.BusinessLayer.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                if (dateTime == DateTime.Today)
                {
                    return "Today";
                }
                if (dateTime == DateTime.Today.AddDays(1))
                {
                    return "Tomorrow";
                }
                if (dateTime == DateTime.Today.AddDays(-1))
                {
                    return "Yesterday";
                }

                return dateTime.ToString("M");
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
