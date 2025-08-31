using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net.Http;
using System.Windows;

namespace LoggerWPF
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost? _host;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _host = Host.CreateDefaultBuilder()
                .UseContentRoot(AppContext.BaseDirectory)
                .ConfigureAppConfiguration(cfg =>
                {
                    // CreateDefaultBuilder already loads appsettings.json,
                    // but this keeps your explicit behavior and reloads.
                    cfg.SetBasePath(AppContext.BaseDirectory);
                    cfg.AddJsonFile("appsettings.json",
                        optional: false, reloadOnChange: true);
                })
                .ConfigureServices((ctx, services) =>
                {
                    // Register all your services/VMS/windows ONCE here
                    AddServices(services);
                })
                .Build();
            
            DIContainer.Initialize(_host.Services);
            
            var mainWindow = DIContainer.Get<MainWindow>();
            mainWindow.DataContext = DIContainer.Get<MainViewModel>();

            MainWindow = mainWindow;
            mainWindow.Show();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddHttpClient();
            
            services.AddSingleton<IHbConfClientApiService, HbConfClientApiService>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host?.Dispose(); 
            base.OnExit(e);
        }
    }
}
