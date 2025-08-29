using CommunityToolkit.Mvvm.Input;

namespace LoggerWPF.Core;



public partial class MainViewModel : ObservableObject
{
    [RelayCommand]
    public void SettingsMenuClick()
    {
        Console.WriteLine(nameof(SettingsMenuClickCommand));
        // CurrentViewModel = _settingsViewModel;
    }

    public object CurrentViewModel { get; set; }

}
