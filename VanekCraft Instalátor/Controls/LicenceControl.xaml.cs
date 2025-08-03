using System.Windows.Controls;
using System.ComponentModel;

namespace VanekCraftInstaller.Controls
{
    public partial class LicenceControl : UserControl, INotifyPropertyChanged
    {
        public bool IsLicenceAccepted => AcceptCheckBox?.IsChecked == true;
        
        public LicenceControl()
        {
            InitializeComponent();
            AcceptCheckBox.Checked += (s, e) => OnPropertyChanged(nameof(IsLicenceAccepted));
            AcceptCheckBox.Unchecked += (s, e) => OnPropertyChanged(nameof(IsLicenceAccepted));
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}