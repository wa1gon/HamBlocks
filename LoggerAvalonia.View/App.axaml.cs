

using LoggerAvalonia.View.Factories;

namespace LoggerAvalonia.View;

public partial class App : Application
{
  public override void Initialize()
  {
    AvaloniaXamlLoader.Load(this);
  }

  // public static ServiceProvider ServiceProvider {get; private set;}

  public override void OnFrameworkInitializationCompleted()
  {
    IServiceCollection collection = new ServiceCollection();
    collection.AddSingleton<MainWindowViewModel>();
    collection.AddTransient<HomeViewModel>();
    collection.AddTransient<ProcessViewModel>();
    collection.AddTransient<ActionsViewModel>();
    collection.AddTransient<MacrosViewModel>();
    collection.AddTransient<ReporterViewModel>();
    collection.AddTransient<HistoryViewModel>();
    collection.AddTransient<SettingsViewModel>();

    // services.AddTransient<TService>(Func<IServiceProvider, TService> implementationFactory)
    collection.AddSingleton<Func<ApplicationPageNames, PageViewModel>>(
      (IServiceProvider x)
      => (ApplicationPageNames name)
        => name switch
    {
      ApplicationPageNames.Home => x.GetRequiredService<HomeViewModel>(),
      ApplicationPageNames.Process => x.GetRequiredService<ProcessViewModel>(),
      ApplicationPageNames.Macros => x.GetRequiredService<MacrosViewModel>(),
      ApplicationPageNames.Actions => x.GetRequiredService<ActionsViewModel>(),
      ApplicationPageNames.Reporter => x.GetRequiredService<ReporterViewModel>(),
      ApplicationPageNames.History => x.GetRequiredService<HistoryViewModel>(),
      ApplicationPageNames.Settings => x.GetRequiredService<SettingsViewModel>(),
      _ => throw new InvalidOperationException(),
    });
    collection.AddSingleton<PageFactory>();

    ServiceProvider services = collection.BuildServiceProvider();

    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
    {
      // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
      // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
      DisableAvaloniaDataAnnotationValidation();
      desktop.MainWindow = new MainWindow
      {
          // DataContext = new MainWindowViewModel(),
          DataContext = services.GetRequiredService<MainWindowViewModel>(),
      };
    }

    base.OnFrameworkInitializationCompleted();
  }

  private void DisableAvaloniaDataAnnotationValidation()
  {
    // Get an array of plugins to remove
    var dataValidationPluginsToRemove =
      BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

    // remove each entry found
    foreach (var plugin in dataValidationPluginsToRemove)
    {
      BindingPlugins.DataValidators.Remove(plugin);
    }
  }
}
