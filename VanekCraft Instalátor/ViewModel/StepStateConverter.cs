using System;
using System.Globalization;
using System.Windows.Data;

namespace VanekCraftInstaller.ViewModel
{
    public class StepStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "Active";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}