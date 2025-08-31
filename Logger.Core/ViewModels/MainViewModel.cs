using CommunityToolkit.Mvvm.Input;

namespace LoggerWPF.Core;

public partial class MainViewModel : ObservableObject
{
    // private readonly SettingsViewModel _settingsViewModel;
    // private readonly HomeViewModel _homeViewModel;

    // public MainViewModel(SettingsViewModel settingsViewModel, HomeViewModel homeViewModel)
    public MainViewModel()
    {
        // _settingsViewModel = settingsViewModel;
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
        // CurrentViewModel = _settingsViewModel;
        var httpclient = new HttpClient();
        httpclient.BaseAddress = new Uri("http://localhost:5000/");
        // CurrentViewModel = new SettingsViewModel(new HbConfClientApiService(httpclient));
    }

    [RelayCommand]
    void HomeMenuClick()
    {
        Console.WriteLine(nameof(HomeMenuClick));
        //todo
        // CurrentViewModel = _homeViewModel;
    }
}
