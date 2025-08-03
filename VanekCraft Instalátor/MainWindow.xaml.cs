using System.Windows;

namespace VanekCraftInstaller
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Ukončit instalaci (levé tlačítko)
        private void CancelInstallation_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Opravdu chceš ukončit instalaci?",
                "Potvrzení",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        // Spustit instalaci (pravé tlačítko)
        private void StartInstallation_Click(object sender, RoutedEventArgs e)
        {
            var splashWindow = new SplashWindow();
            splashWindow.Show();
            this.Close(); // Zavře uvítací okno
        }

        // Zpracování zavíracího tlačítka "X"
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CancelInstallation_Click(sender, e);
        }
    }
}