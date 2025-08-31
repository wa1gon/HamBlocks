namespace LoggerWPF;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    
    public static IServiceProvider? ServiceProvider { get; private set; }
    private IHost? _host;
    public App()
    {
        ServiceCollection services = new();
        
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(ServiceCollection services)
    {
        services.AddTransient<MainWindow>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(config =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((ctx, services) => { AddServices(services); })
            .Build();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.DataContext = _host.Services.GetRequiredService<MainViewModel>();
        MainWindow = mainWindow;
        mainWindow.Show();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddSingleton<IHbConfClientApiService, HbConfClientApiService>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();
        services.AddSingleton<SettingsViewModel>();
        DIContainer.ServiceProvider = services.BuildServiceProvider();
    }
}
