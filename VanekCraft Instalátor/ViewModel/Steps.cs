using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace VanekCraftInstaller
{
    public enum InstallerStep
    {
        Uvod,
        Licence,
        Java,
        Launcher,
        Instalace,
        Fabric,
        Modpack,
        Uklid,
        Dokonceni
    }

    public class StepToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                InstallerStep.Uvod => "1. Úvod",
                InstallerStep.Licence => "2. Licenční podmínky",
                InstallerStep.Java => "3. Kontrola závislostí",
                InstallerStep.Launcher => "4. Volba Launcheru",
                InstallerStep.Instalace => "5. Instalace Launcheru",
                InstallerStep.Fabric => "6. Instalace Fabric API",
                InstallerStep.Modpack => "7. Modpack",
                InstallerStep.Uklid => "8. Úklid",
                InstallerStep.Dokonceni => "9. Dokončeno",
                _ => "Neznámý krok"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public enum StepState
    {
        Completed,
        Active,
        Pending
    }
}