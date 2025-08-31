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
                    AddServices(services, ctx.Configuration);
                })
                .Build();
            
            DIContainer.Initialize(_host.Services);
            
            var mainWindow = DIContainer.Get<MainWindow>();
            mainWindow.DataContext = DIContainer.Get<MainViewModel>();

            MainWindow = mainWindow;
            mainWindow.Show();
        }

        private static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<ApiOptions>()
                .Bind(configuration.GetSection("Api"))
                .Validate(o => Uri.IsWellFormedUriString(o.BaseUrl, UriKind.Absolute),
                    "Api:BaseUrl must be an absolute URL")
                .ValidateOnStart();

            // ViewModels / Windows
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();

            // Typed HttpClient for your API client (this also registers IHttpClientFactory)
            services.AddHttpClient<IHbConfClientApiService, HbConfClientApiService>(
                (sp, client) =>
                {
                    var opts = sp.GetRequiredService<IOptions<ApiOptions>>().Value;
                    var baseUrl = opts.BaseUrl.EndsWith("/") ? opts.BaseUrl : opts.BaseUrl + "/";
                    client.BaseAddress = new Uri(baseUrl);
                    client.Timeout = TimeSpan.FromSeconds(15);
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("HBWebLogger/1.0");
                });


        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host?.Dispose(); 
            base.OnExit(e);
        }
    }
}
