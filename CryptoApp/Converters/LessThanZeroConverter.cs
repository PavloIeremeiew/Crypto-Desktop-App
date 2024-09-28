using System.Globalization;
using System.Windows.Data;

namespace CryptoApp.Converters
{
    class LessThanZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                if (Double.TryParse(stringValue, NumberStyles.Any, culture, out double result))
                {
                    return (double)result < 0;
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
