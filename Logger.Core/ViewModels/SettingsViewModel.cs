using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HbLibrary.Extensions;
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
        Configs = new ObservableCollection<LogConfig>();
    }

    [ObservableProperty]
    private string message = "Settings View";

    [ObservableProperty]
    private ObservableCollection<string> options;

    [ObservableProperty]
    private ObservableCollection<LogConfig> configs;

    [ObservableProperty]
    private bool isMainVisible = false;

    [ObservableProperty]
    private bool isRigControlsVisible = false;

    [ObservableProperty]
    private bool isSpotsVisible = false;

    [ObservableProperty]
    private string? selectedOption;

    [ObservableProperty]
    private LogConfig selectedConfig = new();

    partial void OnSelectedOptionChanged(string? value)
    {
        if (value.IsNullOrEmpty()) return;
        _logger.LogInformation("Selected option changed to: {Option}", value);
        if (value.ToLower() == "new config")
        {
            IsMainVisible = true;
            SelectedConfig = new LogConfig();
        }
        else if (value != "None")
        {
            var config = Configs.FirstOrDefault(c => c.ProfileName == value);
            if (config != null)
            {
                SelectedConfig = config;
                IsMainVisible = true;
            }
        }
        else
        {
            IsMainVisible = false;
            SelectedConfig = new LogConfig();
        }
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
            Configs.Clear();
            Options.Add("None");
            Options.Add("New Config");
            if (configs.IsNullOrEmpty())
            {
                Message = "No configs found";
            }
            else
            {
                foreach (var cfg in configs)
                {
                    Configs.Add(cfg);
                    Options.Add(cfg.ProfileName);
                }
                SelectedOption = Options.FirstOrDefault() ?? "None";
                Message = "Configs loaded successfully";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading configs");
            Message = "Failed to load configs";
        }
    }

    [RelayCommand]
    private async Task SaveConfig()
    {
        try
        {
            if (SelectedConfig == null || SelectedOption.IsNullOrEmpty())
            {
                Message = "No configuration selected";
                return;
            }

            SelectedConfig.IsDirty = true;
            if (SelectedOption.ToLower() == "new config")
            {
                if (string.IsNullOrWhiteSpace(SelectedConfig.ProfileName))
                {
                    Message = "Profile Name is required";
                    return;
                }
                await _apiService.AddAsync(SelectedConfig);
                Configs.Add(SelectedConfig);
                Options.Add(SelectedConfig.ProfileName);
                Message = "Configuration saved successfully";
            }
            else
            {
                await _apiService.UpdateAsync(SelectedConfig);
                Message = "Configuration updated successfully";
            }
            SelectedOption = SelectedConfig.ProfileName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving configuration");
            Message = "Failed to save configuration";
        }
    }
}
