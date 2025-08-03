using System;
using System.Globalization;
using System.Windows.Data;

namespace VanekCraftInstaller.ViewModel
{
    public class BoolToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue && parameter is string paramString)
            {
                var texts = paramString.Split('|');
                if (texts.Length == 2)
                {
                    return boolValue ? texts[0] : texts[1];
                }
            }
            return "Další";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}