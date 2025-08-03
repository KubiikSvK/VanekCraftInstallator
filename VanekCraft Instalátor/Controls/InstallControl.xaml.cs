using System.Windows.Controls;
using System.Windows;
using System.Diagnostics;
using System;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;

namespace VanekCraftInstaller.Controls
{
    public partial class InstallControl : UserControl
    {
        private const string OFFICIAL_LAUNCHER_URL = "https://cdn.vanekgroup.eu/instalator/MinecraftInstaller.exe";
        private const string SK_LAUNCHER_URL = "https://cdn.vanekgroup.eu/instalator/SKLauncher.exe";
        
        private LauncherControl.LauncherType _selectedLauncher;
        private bool _isInstalling = false;

        public InstallControl()
        {
            InitializeComponent();
            Loaded += InstallControl_Loaded;
        }
        
        private void InstallControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Získání výběru launcheru z WizardViewModel
            var wizardViewModel = DataContext as ViewModel.WizardViewModel;
            if (wizardViewModel != null)
            {
                _selectedLauncher = wizardViewModel.SelectedLauncher;
            }
            else
            {
                _selectedLauncher = LauncherControl.LauncherType.Official; // Default
            }
            
            CheckLauncher();
        }
        
        private void InstallButton_Click(object sender, RoutedEventArgs e)
        {
            _ = InstallLauncherAsync();
        }
        
        private void CheckLauncher()
        {
            LauncherTypeText.Text = _selectedLauncher == LauncherControl.LauncherType.Official 
                ? "Oficiální Minecraft Launcher" 
                : "SK Launcher";
                
            StatusText.Text = "Kontroluji přítomnost launcheru...";
            StatusIcon.Text = "⏳";
            PathText.Text = "";
            InstallButton.Visibility = Visibility.Collapsed;
            
            try
            {
                string launcherPath = _selectedLauncher == LauncherControl.LauncherType.Official 
                    ? FindOfficialLauncher() 
                    : FindSKLauncher();
                
                if (!string.IsNullOrEmpty(launcherPath))
                {
                    StatusText.Text = "Launcher je nainstalován";
                    StatusIcon.Text = "✓";
                    StatusIcon.Foreground = System.Windows.Media.Brushes.LightGreen;
                    PathText.Text = $"Umístění: {launcherPath}";
                }
                else
                {
                    ShowLauncherNotFound();
                }
            }
            catch (Exception ex)
            {
                StatusText.Text = "Chyba při kontrole launcheru";
                StatusIcon.Text = "✗";
                StatusIcon.Foreground = System.Windows.Media.Brushes.Red;
                PathText.Text = $"Chyba: {ex.Message}";
            }
        }
        
        private string FindOfficialLauncher()
        {
            string[] possiblePaths = {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Minecraft Launcher", "MinecraftLauncher.exe"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Minecraft Launcher", "MinecraftLauncher.exe"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Packages", "Microsoft.4297127D64EC6_8wekyb3d8bbwe")
            };
            
            foreach (string path in possiblePaths)
            {
                if (File.Exists(path))
                    return path;
                if (Directory.Exists(path))
                    return path + " (Microsoft Store verze)";
            }
            
            // Check all drives
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    try
                    {
                        string searchPath = Path.Combine(drive.RootDirectory.FullName, "Program Files", "Minecraft Launcher", "MinecraftLauncher.exe");
                        if (File.Exists(searchPath))
                            return searchPath;
                            
                        searchPath = Path.Combine(drive.RootDirectory.FullName, "Program Files (x86)", "Minecraft Launcher", "MinecraftLauncher.exe");
                        if (File.Exists(searchPath))
                            return searchPath;
                    }
                    catch { }
                }
            }
            
            return null;
        }
        
        private string FindSKLauncher()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string launcherPath = Path.Combine(appData, "vanekcraftlauncher", "sklauncher.jar");
            return File.Exists(launcherPath) ? launcherPath : null;
        }
        
        private void ShowLauncherNotFound()
        {
            StatusText.Text = "Launcher nebyl nalezen";
            StatusIcon.Text = "✗";
            StatusIcon.Foreground = System.Windows.Media.Brushes.Red;
            PathText.Text = "Launcher není nainstalován v systému";
            InstallButton.Visibility = Visibility.Visible;
            InstallButton.Content = _selectedLauncher == LauncherControl.LauncherType.Official 
                ? "Nainstalovat Minecraft Launcher" 
                : "Nainstalovat SK Launcher";
        }
        
        private async Task InstallLauncherAsync()
        {
            if (_isInstalling) return;
            _isInstalling = true;
            
            try
            {
                InstallButton.IsEnabled = false;
                InstallButton.Content = "Probíhá instalace...";
                StatusIcon.Visibility = Visibility.Collapsed;
                LoadingIcon.Visibility = Visibility.Visible;
                InstallProgressBar.Visibility = Visibility.Visible;
                ProgressText.Visibility = Visibility.Visible;
                
                if (_selectedLauncher == LauncherControl.LauncherType.Official)
                {
                    await InstallOfficialLauncherAsync();
                }
                else
                {
                    await InstallSKLauncherAsync();
                }
            }
            catch (Exception ex)
            {
                StatusText.Text = "Chyba při instalaci launcheru";
                LoadingIcon.Visibility = Visibility.Collapsed;
                StatusIcon.Visibility = Visibility.Visible;
                StatusIcon.Text = "✗";
                StatusIcon.Foreground = System.Windows.Media.Brushes.Red;
                PathText.Text = $"Chyba: {ex.Message}";
                
                InstallProgressBar.Visibility = Visibility.Collapsed;
                ProgressText.Visibility = Visibility.Collapsed;
                InstallButton.Content = "Nainstalovat launcher";
                InstallButton.IsEnabled = true;
            }
            finally
            {
                _isInstalling = false;
            }
        }
        
        private async Task InstallOfficialLauncherAsync()
        {
            // Download
            ProgressText.Text = "Stahuji Minecraft Launcher...";
            InstallProgressBar.Value = 20;
            
            string tempFile = Path.Combine(Path.GetTempPath(), "MinecraftInstaller.exe");
            
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://aka.ms/minecraftClientGameCoreWindows");
                response.EnsureSuccessStatusCode();
                
                InstallProgressBar.Value = 60;
                ProgressText.Text = "Ukládám soubor...";
                
                await File.WriteAllBytesAsync(tempFile, await response.Content.ReadAsByteArrayAsync());
            }
            
            // Spustit instalátor
            InstallProgressBar.Value = 80;
            ProgressText.Text = "Spouštím instalátor...";
            
            var installProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = tempFile,
                    UseShellExecute = true
                }
            };
            
            installProcess.Start();
            
            // Zobrazit výzvu uživateli
            InstallProgressBar.Value = 100;
            ProgressText.Text = "Čekám na dokončení instalace...";
            
            var result = MessageBox.Show(
                "Byl spuštěn oficiální Minecraft instalátor.\n\n" +
                "Prosím dokončete instalaci v oficiálním instalátoru a poté klikněte na OK pro pokračování.",
                "Minecraft Launcher",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Information);
            
            if (result == MessageBoxResult.OK)
            {
                // Cleanup
                try { File.Delete(tempFile); } catch { }
                
                // Recheck
                LoadingIcon.Visibility = Visibility.Collapsed;
                StatusIcon.Visibility = Visibility.Visible;
                InstallProgressBar.Visibility = Visibility.Collapsed;
                ProgressText.Visibility = Visibility.Collapsed;
                CheckLauncher();
                
                // Skryj tlačítko po úspěšné instalaci
                string launcherPath = FindOfficialLauncher();
                if (!string.IsNullOrEmpty(launcherPath))
                {
                    InstallButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    InstallButton.Content = "Nainstalovat launcher";
                    InstallButton.IsEnabled = true;
                }
            }
            else
            {
                LoadingIcon.Visibility = Visibility.Collapsed;
                StatusIcon.Visibility = Visibility.Visible;
                InstallProgressBar.Visibility = Visibility.Collapsed;
                ProgressText.Visibility = Visibility.Collapsed;
                InstallButton.Content = "Nainstalovat launcher";
                InstallButton.IsEnabled = true;
            }
        }
        
        private async Task InstallSKLauncherAsync()
        {
            var installer = new LauncherInstaller();
            
            // Stažení SK Launcher
            ProgressText.Text = "Stahuji SK Launcher...";
            InstallProgressBar.Value = 25;
            await installer.DownloadLauncher();
            
            // Stažení Minecraft verze
            ProgressText.Text = "Stahuji Minecraft verzi 1.21.3...";
            InstallProgressBar.Value = 50;
            await installer.DownloadVanillaVersion("1.21.3");
            
            // Dokončení
            InstallProgressBar.Value = 100;
            ProgressText.Text = "Hotovo!";
            
            await Task.Delay(1000);
            
            // Recheck
            LoadingIcon.Visibility = Visibility.Collapsed;
            StatusIcon.Visibility = Visibility.Visible;
            InstallProgressBar.Visibility = Visibility.Collapsed;
            ProgressText.Visibility = Visibility.Collapsed;
            
            StatusText.Text = "Launcher je nainstalován";
            StatusIcon.Text = "✓";
            StatusIcon.Foreground = System.Windows.Media.Brushes.LightGreen;
            PathText.Text = "Umístění: %AppData%\\vanekcraftlauncher\\sklauncher.jar";
            
            // Po úspěšné instalaci zobraz nick panel
            InstallButton.Visibility = Visibility.Collapsed;
            NickPanel.Visibility = Visibility.Visible;
            InstallButton.Content = "Vytvořit profil";
            InstallButton.Click -= InstallButton_Click;
            InstallButton.Click += CreateProfile_Click;
            InstallButton.Visibility = Visibility.Visible;
            InstallButton.IsEnabled = true;
        }
        
        private void CreateProfile_Click(object sender, RoutedEventArgs e)
        {
            string nick = NickTextBox.Text.Trim();
            if (string.IsNullOrEmpty(nick))
            {
                MessageBox.Show("Zadejte prosím svůj herní nick.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            try
            {
                var installer = new LauncherInstaller();
                installer.CreateOfflineProfile(nick);
                installer.CreateDesktopShortcut();
                
                StatusText.Text = "Profil byl úspěšně vytvořen";
                StatusIcon.Text = "✓";
                StatusIcon.Foreground = System.Windows.Media.Brushes.LightGreen;
                PathText.Text = "Zástupce byl vytvořen na ploše";
                
                InstallButton.Visibility = Visibility.Collapsed;
                NickPanel.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při vytváření profilu: {ex.Message}", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        

    }
}