using System;
using System.ComponentModel;

namespace VanekCraftInstaller.ViewModels
{
    public class InstallerViewModel : INotifyPropertyChanged
    {
        private string _currentStepTitle = "Úvod";
        private string _currentStepDescription = "Vítejte v instalátoru";

        public string CurrentStepTitle
        {
            get => _currentStepTitle;
            set
            {
                _currentStepTitle = value;
                OnPropertyChanged(nameof(CurrentStepTitle));
            }
        }

        public string CurrentStepDescription
        {
            get => _currentStepDescription;
            set
            {
                _currentStepDescription = value;
                OnPropertyChanged(nameof(CurrentStepDescription));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}