using CommunityToolkit.Mvvm.Input;
using LoggerWPF.Core;


namespace Logger.Core;

public partial class MainViewModel : ObservableObject
{
    // private readonly SettingsViewModel _settingsViewModel;
    // private readonly HomeViewModel _homeViewModel;

    // public MainViewModel(SettingsViewModel settingsViewModel, HomeViewModel homeViewModel)
    public MainViewModel()
    {
        CurrentViewModel = new HomeViewModel();
    }

    [ObservableProperty]
    private object _currentViewModel;

    [RelayCommand]
    void SettingsMenuClick()
    {
        
        Console.WriteLine(nameof(SettingsMenuClick));

        CurrentViewModel = DIContainer.Get<SettingsViewModel>();
    }

    [RelayCommand]
    void HomeMenuClick()
    {
        Console.WriteLine(nameof(HomeMenuClick));
        //todo
        CurrentViewModel = new HomeViewModel();
    }
}
