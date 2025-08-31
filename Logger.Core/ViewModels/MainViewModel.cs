using CommunityToolkit.Mvvm.Input;
using LoggerWPF;

namespace LoggerWPF.Core;

public partial class MainViewModel : ObservableObject
{
    private readonly SettingsViewModel _settingsViewModel;
    // private readonly HomeViewModel _homeViewModel;

    // public MainViewModel(SettingsViewModel settingsViewModel, HomeViewModel homeViewModel)
    public MainViewModel()
    {
        // _settingsViewModel = DIContainer.Get<SettingsViewModel>();
        // _homeViewModel = homeViewModel;
        // CurrentViewModel = _homeViewModel;
        CurrentViewModel = new HomeViewModel();
    }

    [ObservableProperty]
    private object _currentViewModel;

    public async Task InitializeAsync()
    {
        // await _settingsViewModel.InitializeAsync();
    }

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
