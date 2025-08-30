namespace LoggerWPF.Core;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private string message   = "Settings View";    
  
    void foo()
    {
        Message = "foo";
    }
    
}
