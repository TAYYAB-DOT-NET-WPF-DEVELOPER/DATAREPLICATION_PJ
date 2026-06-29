using DataIntegration.DbContexts;
using DataIntegration.Services;
using DataIntegration.Stores;
using DataIntegration.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing; // Required for Icon
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
            services.AddDbContext<OracleDbContext>();
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

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

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
            _trayIcon.Dispose();
            base.OnExit(e);
        }
    }
}
