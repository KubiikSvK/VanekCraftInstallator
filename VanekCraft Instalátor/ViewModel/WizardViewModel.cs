using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using VanekCraftInstaller.Controls;

namespace VanekCraftInstaller.ViewModel
{
    public class WizardViewModel : INotifyPropertyChanged
    {
        private int _currentStepIndex = 0;
        private Control? _currentContent;

        public ObservableCollection<WizardStep> Steps { get; set; } = new();
        
        public Control? CurrentContent
        {
            get => _currentContent;
            set
            {
                _currentContent = value;
                OnPropertyChanged(nameof(CurrentContent));
            }
        }

        public bool CanGoBack => _currentStepIndex > 0;
        public bool CanGoNext => _currentStepIndex < Steps.Count - 1;
        public bool IsLastStep => _currentStepIndex == Steps.Count - 1;
        
        public LauncherControl.LauncherType SelectedLauncher { get; set; } = LauncherControl.LauncherType.Official;

        public WizardViewModel()
        {
            InitializeSteps();
            LoadCurrentStep();
        }

        private void InitializeSteps()
        {
            Steps = new ObservableCollection<WizardStep>
            {
                new WizardStep { Title = "Úvod", IsCompleted = false, IsActive = true },
                new WizardStep { Title = "Licence", IsCompleted = false, IsActive = false },
                new WizardStep { Title = "Java", IsCompleted = false, IsActive = false },
                new WizardStep { Title = "Launcher", IsCompleted = false, IsActive = false },
                new WizardStep { Title = "Instalace", IsCompleted = false, IsActive = false },
                new WizardStep { Title = "Fabric", IsCompleted = false, IsActive = false },
                new WizardStep { Title = "Modpack", IsCompleted = false, IsActive = false },
                new WizardStep { Title = "Čištění", IsCompleted = false, IsActive = false },
                new WizardStep { Title = "Dokončeno", IsCompleted = false, IsActive = false }
            };
        }

        public void NextStep()
        {
            if (CanGoNext)
            {
                // Kontrola licence na kroku 1
                if (_currentStepIndex == 1 && CurrentContent is Controls.LicenceControl licenceControl)
                {
                    if (!licenceControl.IsLicenceAccepted)
                    {
                        System.Windows.MessageBox.Show(
                            "Musíte souhlasit s licenčními podmínkami pro pokračování.",
                            "Licence",
                            System.Windows.MessageBoxButton.OK,
                            System.Windows.MessageBoxImage.Warning);
                        return;
                    }
                }
                
                // Kontrola Java na kroku 2
                if (_currentStepIndex == 2 && CurrentContent is Controls.JavaControl javaControl)
                {
                    if (!javaControl.IsJavaReady)
                    {
                        System.Windows.MessageBox.Show(
                            "Java 17+ musí být nainstalována pro pokračování.",
                            "Java",
                            System.Windows.MessageBoxButton.OK,
                            System.Windows.MessageBoxImage.Warning);
                        return;
                    }
                }
                
                // Uložení výběru launcheru na kroku 3
                if (_currentStepIndex == 3 && CurrentContent is Controls.LauncherControl launcherControl)
                {
                    SelectedLauncher = launcherControl.SelectedLauncher;
                }
                
                Steps[_currentStepIndex].IsCompleted = true;
                Steps[_currentStepIndex].IsActive = false;
                _currentStepIndex++;
                Steps[_currentStepIndex].IsActive = true;
                LoadCurrentStep();
                OnPropertyChanged(nameof(CanGoBack));
                OnPropertyChanged(nameof(CanGoNext));
                OnPropertyChanged(nameof(IsLastStep));
                OnPropertyChanged(nameof(Steps));
            }
        }

        public void PreviousStep()
        {
            if (CanGoBack)
            {
                Steps[_currentStepIndex].IsActive = false;
                _currentStepIndex--;
                Steps[_currentStepIndex].IsCompleted = false;
                Steps[_currentStepIndex].IsActive = true;
                LoadCurrentStep();
                OnPropertyChanged(nameof(CanGoBack));
                OnPropertyChanged(nameof(CanGoNext));
                OnPropertyChanged(nameof(IsLastStep));
                OnPropertyChanged(nameof(Steps));
            }
        }

        private void LoadCurrentStep()
        {
            CurrentContent = _currentStepIndex switch
            {
                0 => new Controls.IntroControl(),
                1 => new Controls.LicenceControl(),
                2 => new Controls.JavaControl(),
                3 => new Controls.LauncherControl(),
                4 => new Controls.InstallControl(),
                5 => new Controls.FabricControl(),
                6 => new Controls.ModpackControl(),
                7 => new Controls.CleanupControl(),
                8 => new Controls.FinishControl(),
                _ => new Controls.IntroControl()
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class WizardStep : INotifyPropertyChanged
    {
        private string _title = string.Empty;
        private bool _isCompleted;
        private bool _isActive;

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set { _isCompleted = value; OnPropertyChanged(nameof(IsCompleted)); }
        }

        public bool IsActive
        {
            get => _isActive;
            set { _isActive = value; OnPropertyChanged(nameof(IsActive)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}