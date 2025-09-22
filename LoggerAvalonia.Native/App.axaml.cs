using Logger.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LoggerAvalonia.Native;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var collection = new ServiceCollection();
        collection.AddLogging();
        collection.AddTransient<MainWindow>();
        var services = collection.BuildServiceProvider();
        // Resolve the ViewModel
        var vm = services.GetRequiredService<MainWindowViewModel>();

        // Handle application lifetime
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleView)
        {
            // singleView.MainView = new MainView
            // {
            //     DataContext = vm
            // };
        }


        base.OnFrameworkInitializationCompleted();
    }
}
