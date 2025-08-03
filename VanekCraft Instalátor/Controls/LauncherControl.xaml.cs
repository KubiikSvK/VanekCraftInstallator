using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;

namespace VanekCraftInstaller.Controls
{
    public partial class LauncherControl : UserControl, INotifyPropertyChanged
    {
        public enum LauncherType
        {
            Official,
            SK
        }
        
        private LauncherType _selectedLauncher = LauncherType.Official;
        
        public LauncherType SelectedLauncher
        {
            get => _selectedLauncher;
            set
            {
                _selectedLauncher = value;
                OnPropertyChanged(nameof(SelectedLauncher));
                UpdateSelectionText();
            }
        }
        
        public LauncherControl()
        {
            InitializeComponent();
            UpdateSelectionText();
        }
        
        private void OfficialLauncher_Click(object sender, MouseButtonEventArgs e)
        {
            OfficialRadio.IsChecked = true;
            SKRadio.IsChecked = false;
            SelectedLauncher = LauncherType.Official;
        }
        
        private void SKLauncher_Click(object sender, MouseButtonEventArgs e)
        {
            SKRadio.IsChecked = true;
            OfficialRadio.IsChecked = false;
            SelectedLauncher = LauncherType.SK;
        }
        
        private void OfficialRadio_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            SelectedLauncher = LauncherType.Official;
        }
        
        private void SKRadio_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            SelectedLauncher = LauncherType.SK;
        }
        
        private void UpdateSelectionText()
        {
            if (SelectionText != null)
            {
                SelectionText.Text = SelectedLauncher == LauncherType.Official 
                    ? "Vybrán: Oficiální Minecraft Launcher"
                    : "Vybrán: SK Launcher";
            }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}