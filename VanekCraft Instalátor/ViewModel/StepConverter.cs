using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;

namespace VanekCraftInstaller.ViewModel
{
    public class StepTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is WizardStep step)
            {
                string prefix = step.IsCompleted ? "✓ " : step.IsActive ? "► " : "  ";
                return $"{prefix}{step.Title}";
            }
            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StepColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is WizardStep step)
            {
                if (step.IsCompleted) return new SolidColorBrush(Color.FromRgb(46, 204, 113)); // Zelená
                if (step.IsActive) return new SolidColorBrush(Color.FromRgb(230, 126, 34)); // Oranžová
                return new SolidColorBrush(Color.FromRgb(149, 165, 166)); // Šedá
            }
            return new SolidColorBrush(Colors.Gray);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StepFontSizeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is WizardStep step)
            {
                if (step.IsCompleted) return 12.0; // Předchozí
                if (step.IsActive) return 16.0; // Aktuální
                return 14.0; // Následující
            }
            return 14.0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StepFontFamilyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is WizardStep step)
            {
                if (step.IsCompleted) return new FontFamily("pack://application:,,,/Assets/Fonts/#Ubuntu Mono");
                if (step.IsActive) return new FontFamily("pack://application:,,,/Assets/Fonts/#Ubuntu");
                return new FontFamily("pack://application:,,,/Assets/Fonts/#Ubuntu");
            }
            return new FontFamily("pack://application:,,,/Assets/Fonts/#Ubuntu");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StepFontWeightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is WizardStep step)
            {
                if (step.IsActive) return FontWeights.Bold;
                return FontWeights.Normal;
            }
            return FontWeights.Normal;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StepFontStyleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is WizardStep step)
            {
                if (step.IsActive) return FontStyles.Italic;
                return FontStyles.Normal;
            }
            return FontStyles.Normal;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}