using System.Windows;
using VanekCraftInstaller.ViewModel;

namespace VanekCraftInstaller
{
    public partial class InstallerWindow : Window
    {
        private WizardViewModel _viewModel;

        public InstallerWindow()
        {
            InitializeComponent();
            _viewModel = new WizardViewModel();
            DataContext = _viewModel;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.PreviousStep();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.IsLastStep)
            {
                // Zavřít instalátor
                this.Close();
            }
            else
            {
                _viewModel.NextStep();
            }
        }
    }
}