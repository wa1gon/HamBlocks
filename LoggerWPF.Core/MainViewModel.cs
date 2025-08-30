using CommunityToolkit.Mvvm.Input;

namespace LoggerWPF.Core;



public partial class MainViewModel : ObservableObject
{
    private readonly SettingsViewModel _settingsViewModel;
    [ObservableProperty]
    private object currentViewModel;
    public MainViewModel()
    {

        // CurrentViewModel = _homeViewModel;
    }
    [RelayCommand]
    public void SettingsMenuClick()
    {
        Console.WriteLine(nameof(SettingsMenuClickCommand));
        CurrentViewModel = new SettingsViewModel();
    }
}
