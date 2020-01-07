using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Codemash
{
    public class WeatherIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string weather)
            {
                if (weather.ToLower().Contains("cloud"))
                    return "☁️";
                else if (weather.ToLower().Contains("rain"))
                    return "🌧️";
                else
                    return "🌞";
            }
            else
            {
                return "🌞";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
