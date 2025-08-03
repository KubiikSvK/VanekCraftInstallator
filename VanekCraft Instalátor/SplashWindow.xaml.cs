using System.Windows;
using System.Windows.Threading;
using System;

namespace VanekCraftInstaller
{
    public partial class SplashWindow : Window
    {
        private readonly DispatcherTimer timer;
        private readonly int totalSeconds = 2;
        private int elapsedMs = 0;

        public SplashWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            elapsedMs += 100;
            double percent = (double)elapsedMs / (totalSeconds * 1000) * 100;
            progressBar.Value = percent;

            if (elapsedMs >= totalSeconds * 1000)
            {
                timer.Stop();
                try
                {
                    var installerWindow = new InstallerWindow();
                    installerWindow.Show();
                    this.Close();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Chyba: {ex.Message}", "Error");
                }
            }
        }
    }
}