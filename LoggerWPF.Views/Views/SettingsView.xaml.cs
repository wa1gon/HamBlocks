

namespace LoggerWPF.Views;

public partial class SettingsView : UserControl
{
    private SettingsViewModel _settingsViewModel;
    public SettingsView()
    {
        InitializeComponent();
        _settingsViewModel = DIContainer.Get<SettingsViewModel>();
        DataContext = _settingsViewModel;
      }
    private async void OnLoading(object sender, RoutedEventArgs routedEventArgs)
    {
        await _settingsViewModel.InitializeAsync();
    }
}
