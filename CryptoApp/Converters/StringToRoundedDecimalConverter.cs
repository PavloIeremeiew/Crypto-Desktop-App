using System.Globalization;
using System.Windows.Data;

namespace CryptoApp.Converters
{
    public class StringToRoundedDecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value is string priceString)
            {
                if (float.TryParse(priceString, NumberStyles.Any, culture, out float price))
                {
                    int decimalPlaces = int.Parse((string)parameter);
                    return Math.Round(price, decimalPlaces).ToString();
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
