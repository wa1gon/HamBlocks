
namespace Logger.Core.ViewModels;

public partial class SettingsViewModel : PageViewModel
{
  public string Test { get; set; } = "Settings";

  // [ObservableProperty]
  // private List<string> _locationPaths;

  public SettingsViewModel()
  {
    // PageName = ApplicationPageNames.Settings;
    
  }
}
