using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using HbLibrary.Extensions;
using Microsoft.Extensions.Logging;

namespace LoggerWPF.Core;

public partial class SettingsViewModel : ObservableObject
{
    private readonly IHbConfClientApiService _apiService;
    private readonly ILogger<SettingsViewModel> _logger;

    public SettingsViewModel(IHbConfClientApiService apiService,
                             ILogger<SettingsViewModel> logger)
    {
        _apiService = apiService;
        _logger = logger;
        Options = new ObservableCollection<string>();
    }

    [ObservableProperty]
    private string message = "Settings View";

    [ObservableProperty]
    private ObservableCollection<string> options;
    
    [ObservableProperty]
    private ObservableCollection<LogConfig> configs;
    
    [ObservableProperty]
    private bool isMainVisable = false;
    
    [ObservableProperty]
    private bool isRigControlsVisable = false;
    
    [ObservableProperty]
    private bool isSpotssVisable = false;
    
    [ObservableProperty]
    private string? selectedOption;

    [ObservableProperty] private LogConfig selectedConfig = new();

    // Toolkit generates this hook for you. It must be exactly this name/signature.
    partial void OnSelectedOptionChanged(string? value)
    {
        if (value.IsNullOrEmpty() == true) return;
        if (value.ToLower() == "new config")
        {

            isMainVisable = true;
        }

        // fire-and-forget is fine here; log errors inside the async method
        // _ = SaveOptionAsync(value);

    }

    public async Task InitializeAsync()
    {
        await LoadConfigsAsync();
    }

    private async Task LoadConfigsAsync()
    {
        try
        {
            var configs = await _apiService.GetAllAsync();
            Options.Clear();
            Options.Add("None");
            Options.Add("New Config");
            if (configs.Any() == true) IsMainVisable = true;
            if (configs is not null)
            {
                foreach (var cfg in configs)
                {
                    // add whatever display string you want
                    Options.Add(cfg.ProfileName); // <-- assuming LogConfig.ProfileName
                }
            }

            SelectedOption = Options.FirstOrDefault() ?? "None";
            Message = "Configs loaded successfully";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading configs");
            Message = "Failed to load configs";
        }
    }

    private async Task SaveOptionAsync(string option)
    {
        try
        {
            _logger.LogInformation("Saving option {Option}", option);
            // TODO: call your API service as needed
            // await _apiService.AddAsync(new LogConfig { ProfileName = option }, ct);
            Message = "Option saved successfully";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving option");
            Message = "Failed to save option";
        }
    }
}
