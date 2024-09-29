using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CryptoApp.Converters
{
    class NullStringFormatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return string.Empty;
            if (parameter is null)
                return value;
            if (float.TryParse(value.ToString(), NumberStyles.Any, culture, out float number))
            {
                return string.Format(parameter.ToString()!, number);
            }
                return string.Format(parameter.ToString()!, value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
