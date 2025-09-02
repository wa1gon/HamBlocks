namespace LoggerAvalonia.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommonServices(this IServiceCollection collection)
    {
        // collection.AddSingleton<IRepository, Repository>();
        // collection.AddTransient<IBusinessService, BusinessService>();
        collection.AddTransient<MainWindowViewModel>(); // Example ViewModel
        return collection;
    }
}
