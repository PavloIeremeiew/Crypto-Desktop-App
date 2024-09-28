using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CryptoApp.Converters
{
    class PercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is string priceString)
            {
                if (double.TryParse(priceString, NumberStyles.Any, culture, out double price))
                {
                    price = Math.Round(price, 1);
                    return $"{(price > 0 ? "+" : "")}{price}%";
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
