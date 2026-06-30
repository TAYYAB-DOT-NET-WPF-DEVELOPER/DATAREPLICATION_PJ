using DataIntegration.DbContexts;
using DataIntegration.Services;
using DataIntegration.Stores;
using DataIntegration.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing; // Required for Icon
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms; // Required for NotifyIcon
using Application = System.Windows.Application;

namespace DataIntegration
{
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;
        private NotifyIcon _trayIcon;
        private Window _mainWindow;

        public App()
        {
            ConfigureServices();
            LoggingConfiguration.ConfigureLogging();
        }

        private void ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDbContextFactory<OracleDbContext>();
            services.AddSingleton<DefaultValues>();
            services.AddSingleton<IMainService, MainService>();
            services.AddSingleton<LandingViewVM>();
            services.AddSingleton<SettingsVM>();
            services.AddTransient<MainWindowVM>();
            services.AddTransient(s => new MainWindow
            {
                DataContext = s.GetRequiredService<MainWindowVM>()
            });

            _serviceProvider = services.BuildServiceProvider();
        }

        private System.Windows.Threading.DispatcherTimer _restartTimer;
        private System.Windows.Threading.DispatcherTimer _whatsAppTimer;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // If this is the first run, show the setup window
            if (!System.IO.File.Exists("setup_done.txt"))
            {
                var setupWindow = new Views.SetupWindow();
                if (setupWindow.ShowDialog() != true)
                {
                    Environment.Exit(0);
                    return;
                }
            }

            // Configure the 3-hour automatic restart timer
            ConfigureRestartTimer();

            // Configure the 6-hour WhatsApp notification timer
            ConfigureWhatsAppTimer();

            // Initialize and set up system tray icon
            _trayIcon = new NotifyIcon
            {
                Icon = new Icon("Logo.ico"),
              
                Visible = true,
                Text = "Data Integration"
            };

            _trayIcon.DoubleClick += (sender, args) =>
            {
                ToggleMainWindowVisibility();
            };

            // Initialize and hide the main window
            _mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            _mainWindow.WindowState = WindowState.Minimized;
            _mainWindow.ShowInTaskbar = false;
            _mainWindow.Visibility = Visibility.Hidden;

            // Show the main window when needed
            _trayIcon.MouseClick += (sender, args) =>
            {
                if (args.Button == MouseButtons.Left)
                {
                    ToggleMainWindowVisibility();
                }
            };
        }

        private void ConfigureRestartTimer()
        {
            _restartTimer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromHours(3)
            };
            _restartTimer.Tick += (sender, args) => RestartApplication();
            _restartTimer.Start();
        }

        private void RestartApplication()
        {
            try
            {
                // Get the path of the actual running .exe file
                string exeFile = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

                // Start a new instance
                System.Diagnostics.Process.Start(exeFile);

                // Forcibly exit the current instance immediately to bypass any closing dialogs / password checks
                Environment.Exit(0);
            }
            catch (Exception)
            {
                Environment.Exit(0);
            }
        }

        private void ConfigureWhatsAppTimer()
        {
            _whatsAppTimer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromHours(6)
            };
            _whatsAppTimer.Tick += async (sender, args) => await CheckAndSendWhatsAppNotificationAsync();
            _whatsAppTimer.Start();

            // Also check immediately on startup (after 5 seconds)
            Task.Run(async () =>
            {
                await Task.Delay(5000);
                await CheckAndSendWhatsAppNotificationAsync();
            });
        }

        private async Task CheckAndSendWhatsAppNotificationAsync()
        {
            try
            {
                if (System.IO.File.Exists("LATEST_ERROR.txt"))
                {
                    // Check if we already sent a message today
                    string lastSentDateFile = "LAST_WHATSAPP_SENT.txt";
                    string todayStr = DateTime.Today.ToString("yyyy-MM-dd");

                    if (System.IO.File.Exists(lastSentDateFile))
                    {
                        string lastSentDate = await System.IO.File.ReadAllTextAsync(lastSentDateFile).ConfigureAwait(false);
                        if (lastSentDate.Trim() == todayStr)
                        {
                            // Already sent a message today, skip
                            return;
                        }
                    }

                    string errorContent = await System.IO.File.ReadAllTextAsync("LATEST_ERROR.txt").ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(errorContent))
                    {
                        await Services.WhatsAppSender.SendErrorNotificationAsync(errorContent).ConfigureAwait(false);
                        
                        // Record that we successfully sent the message today
                        await System.IO.File.WriteAllTextAsync(lastSentDateFile, todayStr).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Failed to check and send WhatsApp notification.");
            }
        }

        private void ToggleMainWindowVisibility()
        {
            if (_mainWindow.Visibility == Visibility.Visible)
            {
                _mainWindow.WindowState = WindowState.Minimized;
                _mainWindow.Visibility = Visibility.Hidden;
            }
            else
            {
                _mainWindow.Visibility = Visibility.Visible;
                _mainWindow.WindowState = WindowState.Normal;
                _mainWindow.Activate();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _restartTimer?.Stop();
            _whatsAppTimer?.Stop();
            _trayIcon.Dispose();
            base.OnExit(e);
        }
    }
}
