

using Microsoft.Extensions.Logging;

namespace LoggerWPF.Core;

public partial class SettingsViewModel : ObservableObject
{
    private readonly IHbConfClientApiService _apiService;
    private readonly ILogger<SettingsViewModel> _logger;
    public SettingsViewModel(IHbConfClientApiService apiService, ILogger<SettingsViewModel> logger)
    {
        _apiService = apiService;
        _logger = logger;
        Options = new ObservableCollection<string>();
    }

    [ObservableProperty]
    private string _message = "Settings View";

    [ObservableProperty]
    private ObservableCollection<string> _options;

    [ObservableProperty]
    private string _selectedOption;
    
    public async Task InitializeAsync()
    {
        await LoadConfigsAsync();
    }


    private async Task LoadConfigsAsync()
    {
        try
        {
            var configs = await _apiService.GetAllAsync();
            if (configs != null)
            {
                Options.Clear();
                foreach (var config in configs)
                {
                    // Options.Add(config);
                }
                SelectedOption = Options.FirstOrDefault() ?? "None";
                Message = "Configs loaded successfully";
            }
            else
            {
                Message = "No configs found";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading configs: {ex.Message}");
            Message = "Failed to load configs";
        }
    }

    partial void OnSelectedOptionChanged(string value)
    {
        Console.WriteLine($"Selected option changed to: {value}");
        SaveOptionAsync(value).GetAwaiter().GetResult();
    }

    private async Task SaveOptionAsync(string option)
    {
        //todo complete save
        // try
        // {
        //     // var config = new LogConfig { ProfileName = "Default", SelectedOption = option };
        //     // await _apiService.AddAsync(config);
        //     _message = "Option saved successfully";
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine($"Error saving option: {ex.Message}");
        //     _message = "Failed to save option";
        // }
    }
    
}
