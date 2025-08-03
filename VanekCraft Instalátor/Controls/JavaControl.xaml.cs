using System.Windows.Controls;
using System.Windows;
using System.Diagnostics;
using System;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VanekCraftInstaller.Controls
{
    public partial class JavaControl : UserControl, INotifyPropertyChanged
    {
        private const string JAVA_DOWNLOAD_URL = "https://cdn.vanekgroup.eu/instalator/OpenJDK21U.msi";
        private const int REQUIRED_JAVA_VERSION = 17;
        
        private bool _isJavaReady = false;
        public bool IsJavaReady 
        { 
            get => _isJavaReady; 
            set 
            { 
                _isJavaReady = value; 
                OnPropertyChanged(nameof(IsJavaReady)); 
            } 
        }

        public JavaControl()
        {
            InitializeComponent();
            CheckJava();
        }
        
        private void InstallJavaButton_Click(object sender, RoutedEventArgs e)
        {
            _ = InstallJavaAsync();
        }
        
        private void CheckJava()
        {
            JavaStatusText.Text = "Kontrolujem Java instalaci...";
            JavaStatusIcon.Text = "⏳";
            JavaVersionText.Text = "";
            InstallJavaButton.Visibility = Visibility.Collapsed;
            
            // Zkusíme více způsobů detekce Java
            if (TryCheckJavaCommand() || TryCheckJavaPath() || TryCheckJavaRegistry())
            {
                return; // Java nalezena
            }
            
            ShowJavaNotFound();
        }
        
        private bool TryCheckJavaCommand()
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "java",
                        Arguments = "-version",
                        UseShellExecute = false,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    }
                };
                
                process.Start();
                string output = process.StandardError.ReadToEnd();
                process.WaitForExit();
                
                if (process.ExitCode == 0 && IsJavaVersionValid(output))
                {
                    JavaStatusText.Text = "Java je nainstalována";
                    JavaStatusIcon.Text = "✓";
                    JavaStatusIcon.Foreground = System.Windows.Media.Brushes.LightGreen;
                    JavaVersionText.Text = ExtractJavaVersion(output);
                    IsJavaReady = true;
                    return true;
                }
            }
            catch { }
            return false;
        }
        
        private bool TryCheckJavaPath()
        {
            try
            {
                string[] commonPaths = {
                    @"C:\Program Files\Eclipse Adoptium",
                    @"C:\Program Files\Java",
                    @"C:\Program Files (x86)\Java",
                    @"C:\Program Files\OpenJDK"
                };
                
                foreach (string basePath in commonPaths)
                {
                    if (Directory.Exists(basePath))
                    {
                        var jdkDirs = Directory.GetDirectories(basePath)
                            .Where(d => Path.GetFileName(d).Contains("jdk") || Path.GetFileName(d).Contains("java"))
                            .OrderByDescending(d => d);
                            
                        foreach (string jdkDir in jdkDirs)
                        {
                            string javaExe = Path.Combine(jdkDir, "bin", "java.exe");
                            if (File.Exists(javaExe))
                            {
                                JavaStatusText.Text = "Java je nainstalována";
                                JavaStatusIcon.Text = "✓";
                                JavaStatusIcon.Foreground = System.Windows.Media.Brushes.LightGreen;
                                JavaVersionText.Text = $"Nalezeno: {jdkDir}";
                                IsJavaReady = true;
                                return true;
                            }
                        }
                    }
                }
            }
            catch { }
            return false;
        }
        
        private bool TryCheckJavaRegistry()
        {
            try
            {
                using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\JDK"))
                {
                    if (key != null)
                    {
                        var versions = key.GetSubKeyNames().OrderByDescending(v => v);
                        foreach (string version in versions)
                        {
                            if (version.StartsWith("1") || int.TryParse(version.Split('.')[0], out int majorVersion) && majorVersion >= REQUIRED_JAVA_VERSION)
                            {
                                JavaStatusText.Text = "Java je nainstalována";
                                JavaStatusIcon.Text = "✓";
                                JavaStatusIcon.Foreground = System.Windows.Media.Brushes.LightGreen;
                                JavaVersionText.Text = $"Registry: Java {version}";
                                IsJavaReady = true;
                                return true;
                            }
                        }
                    }
                }
            }
            catch { }
            return false;
        }
        
        private bool IsJavaVersionValid(string output)
        {
            try
            {
                var lines = output.Split('\n');
                if (lines.Length > 0)
                {
                    var versionLine = lines[0];
                    var parts = versionLine.Split(' ');
                    foreach (var part in parts)
                    {
                        if (part.Contains("\"") && (part.Contains("1.") || part.Contains(".")))
                        {
                            var version = part.Trim('"');
                            if (version.StartsWith("1."))
                            {
                                // Java 8 format: 1.8.0_xxx
                                if (int.TryParse(version.Split('.')[1], out int majorVersion))
                                {
                                    return majorVersion >= 8; // Java 8 = version 8
                                }
                            }
                            else
                            {
                                // Java 9+ format: 17.0.1, 21.0.1
                                if (int.TryParse(version.Split('.')[0], out int majorVersion))
                                {
                                    return majorVersion >= REQUIRED_JAVA_VERSION;
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            return false;
        }
        
        private string ExtractJavaVersion(string output)
        {
            var lines = output.Split('\n');
            return lines.Length > 0 ? lines[0].Trim() : "Neznámá verze";
        }
        
        private void ShowJavaNotFound()
        {
            JavaStatusText.Text = "Java 17+ nebyla nalezena";
            JavaStatusIcon.Text = "✗";
            JavaStatusIcon.Foreground = System.Windows.Media.Brushes.Red;
            JavaVersionText.Text = "Vyžadována Java 17 nebo novější";
            InstallJavaButton.Visibility = Visibility.Visible;
            IsJavaReady = false;
        }
        
        private void RefreshEnvironmentVariables()
        {
            try
            {
                // Refresh PATH for current process
                string systemPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);
                string userPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
                string currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
                
                string newPath = systemPath;
                if (!string.IsNullOrEmpty(userPath))
                {
                    newPath += ";" + userPath;
                }
                
                Environment.SetEnvironmentVariable("PATH", newPath, EnvironmentVariableTarget.Process);
                
                // Refresh JAVA_HOME if exists
                string javaHome = Environment.GetEnvironmentVariable("JAVA_HOME", EnvironmentVariableTarget.Machine);
                if (!string.IsNullOrEmpty(javaHome))
                {
                    Environment.SetEnvironmentVariable("JAVA_HOME", javaHome, EnvironmentVariableTarget.Process);
                }
            }
            catch { }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private async Task InstallJavaAsync()
        {
            try
            {
                InstallJavaButton.IsEnabled = false;
                InstallJavaButton.Content = "Probíhá instalace...";
                JavaStatusIcon.Visibility = Visibility.Collapsed;
                LoadingIcon.Visibility = Visibility.Visible;
                JavaProgressBar.Visibility = Visibility.Visible;
                ProgressText.Visibility = Visibility.Visible;
                
                // Download
                ProgressText.Text = "Stahuji Java 21...";
                JavaProgressBar.Value = 10;
                
                string tempFile = Path.Combine(Path.GetTempPath(), "OpenJDK21U.msi");
                
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(JAVA_DOWNLOAD_URL);
                    response.EnsureSuccessStatusCode();
                    
                    JavaProgressBar.Value = 50;
                    ProgressText.Text = "Ukládám soubor...";
                    
                    await File.WriteAllBytesAsync(tempFile, await response.Content.ReadAsByteArrayAsync());
                }
                
                // Install
                JavaProgressBar.Value = 70;
                ProgressText.Text = "Instaluji Java 21...";
                
                var installProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "msiexec",
                        Arguments = $"/i \"{tempFile}\" /quiet /norestart",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                
                installProcess.Start();
                await Task.Run(() => installProcess.WaitForExit());
                
                JavaProgressBar.Value = 90;
                ProgressText.Text = "Dokončuji instalaci...";
                
                // Cleanup
                try { File.Delete(tempFile); } catch { }
                
                // Refresh environment variables
                RefreshEnvironmentVariables();
                
                JavaProgressBar.Value = 100;
                ProgressText.Text = "Hotovo!";
                
                await Task.Delay(2000); // Delší čekání pro dokončení instalace
                
                // Recheck
                LoadingIcon.Visibility = Visibility.Collapsed;
                JavaStatusIcon.Visibility = Visibility.Visible;
                JavaProgressBar.Visibility = Visibility.Collapsed;
                ProgressText.Visibility = Visibility.Collapsed;
                CheckJava();
                
                // Po úspěšné instalaci skryj tlačítko
                if (IsJavaReady)
                {
                    InstallJavaButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    InstallJavaButton.Content = "Nainstalovat Java 21";
                    InstallJavaButton.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                JavaStatusText.Text = "Chyba při instalaci Java";
                LoadingIcon.Visibility = Visibility.Collapsed;
                JavaStatusIcon.Visibility = Visibility.Visible;
                JavaStatusIcon.Text = "✗";
                JavaStatusIcon.Foreground = System.Windows.Media.Brushes.Red;
                JavaVersionText.Text = $"Chyba: {ex.Message}";
                
                JavaProgressBar.Visibility = Visibility.Collapsed;
                ProgressText.Visibility = Visibility.Collapsed;
                InstallJavaButton.Content = "Nainstalovat Java 21";
                InstallJavaButton.IsEnabled = true;
            }
        }
    }
}