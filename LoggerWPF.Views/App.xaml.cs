namespace LoggerWPF;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    
    public static IServiceProvider? ServiceProvider { get; private set; }
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
        var services = new ServiceCollection();
        services.AddHttpClient("SharedClient", client =>
        {
            client.BaseAddress = new Uri("https://api.example.com/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        services.AddSingleton<MainViewModel>();
        services.AddTransient<SettingsViewModel>();
        services.AddTransient<HbConfClientApiService>();
        
        ServiceProvider = services.BuildServiceProvider();
        var mainWindow = ServiceProvider.GetService<MainWindow>();
        mainWindow?.Show();
    }
}
