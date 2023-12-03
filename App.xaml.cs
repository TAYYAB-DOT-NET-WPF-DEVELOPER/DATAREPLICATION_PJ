using DataIntegration.Models;
using DataIntegration.Services;
using DataIntegration.Stores;
using DataIntegration.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace DataIntegration
{
    public partial class App : Application
    {
        private IServiceProvider _serviceprovider;

        public App()
        {
            ConfigureServices();
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

            _serviceprovider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow = _serviceprovider.GetRequiredService<MainWindow>();
            MainWindow.Show();
        }

    }
}
