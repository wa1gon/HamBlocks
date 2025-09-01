using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HbLibrary.Extensions;
using Microsoft.Extensions.Logging;
using HamBlocks.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoggerWPF.Core;

public partial class SettingsViewModel : ObservableObject, IDataErrorInfo
{
    private readonly IHbConfClientApiService _apiService;
    private readonly ILogger<SettingsViewModel> _logger;
    private readonly Dictionary<string, string> _validationErrors = new();

    public SettingsViewModel(IHbConfClientApiService apiService, ILogger<SettingsViewModel> logger)
    {
        _apiService = apiService;
        _logger = logger;
        Options = new ObservableCollection<string>();
        Configs = new ObservableCollection<LogConfig>();
        CallBookConfigs = new ObservableCollection<CallBookConf>();
        RigCtlConfigs = new ObservableCollection<RigCtlConf>();
        DxClusterConfigs = new ObservableCollection<DxClusterConf>();
    }

    [ObservableProperty]
    private string message = "Settings View";

    [ObservableProperty]
    private ObservableCollection<string> options;

    [ObservableProperty]
    private ObservableCollection<LogConfig> configs;

    [ObservableProperty]
    private ObservableCollection<CallBookConf> callBookConfigs;

    [ObservableProperty]
    private ObservableCollection<RigCtlConf> rigCtlConfigs;

    [ObservableProperty]
    private ObservableCollection<DxClusterConf> dxClusterConfigs;

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

    [ObservableProperty]
    private CallBookConf? selectedCallBook;

    [ObservableProperty]
    private RigCtlConf? selectedRigCtl;

    [ObservableProperty]
    private DxClusterConf? selectedDxCluster;

    public bool HasErrors => _validationErrors.Any();

    // IDataErrorInfo implementation
    public string Error => string.Join("; ", _validationErrors.Values);

    public string this[string columnName]
    {
        get
        {
            string error = string.Empty;
            if (columnName.StartsWith("SelectedConfig."))
            {
                columnName = columnName.Replace("SelectedConfig.", "");
                if (SelectedConfig != null)
                {
                    error = ValidateLogConfig(columnName, SelectedConfig);
                    if (string.IsNullOrEmpty(error))
                        _validationErrors.Remove(columnName);
                    else
                        _validationErrors[columnName] = error;
                }
            }
            else if (columnName.StartsWith("SelectedCallBook."))
            {
                columnName = columnName.Replace("SelectedCallBook.", "");
                if (SelectedCallBook != null)
                {
                    error = ValidateCallBookConf(columnName, SelectedCallBook);
                    if (string.IsNullOrEmpty(error))
                        _validationErrors.Remove(columnName);
                    else
                        _validationErrors[columnName] = error;
                }
            }
            else if (columnName.StartsWith("SelectedRigCtl."))
            {
                columnName = columnName.Replace("SelectedRigCtl.", "");
                if (SelectedRigCtl != null)
                {
                    error = ValidateRigCtlConf(columnName, SelectedRigCtl);
                    if (string.IsNullOrEmpty(error))
                        _validationErrors.Remove(columnName);
                    else
                        _validationErrors[columnName] = error;
                }
            }
            else if (columnName.StartsWith("SelectedDxCluster."))
            {
                columnName = columnName.Replace("SelectedDxCluster.", "");
                if (SelectedDxCluster != null)
                {
                    error = ValidateDxClusterConf(columnName, SelectedDxCluster);
                    if (string.IsNullOrEmpty(error))
                        _validationErrors.Remove(columnName);
                    else
                        _validationErrors[columnName] = error;
                }
            }
            OnPropertyChanged(nameof(HasErrors));
            return error;
        }
    }

    private string ValidateLogConfig(string propertyName, LogConfig config)
    {
        switch (propertyName)
        {
            case nameof(LogConfig.ProfileName):
                if (string.IsNullOrWhiteSpace(config.ProfileName))
                    return "Profile Name is required.";
                if (config.ProfileName.Length > 50)
                    return "Profile Name must be 50 characters or less.";
                break;
            case nameof(LogConfig.Callsign):
                if (string.IsNullOrWhiteSpace(config.Callsign))
                    return "Callsign is required.";
                // if (!DxccCallInfo.IsValidCallSign(config.Callsign))
                //     return "Invalid Callsign format.";
                break;
            case nameof(LogConfig.StationName):
                if (!string.IsNullOrEmpty(config.StationName) && config.StationName.Length > 100)
                    return "Station Name must be 100 characters or less.";
                break;
            case nameof(LogConfig.GridSquare):
                if (!string.IsNullOrEmpty(config.GridSquare) && !Regex.IsMatch(config.GridSquare, @"^[A-R]{2}[0-9]{2}$"))
                    return "Grid Square must be in Maidenhead format (e.g., FN31).";
                break;
            case nameof(LogConfig.City):
                if (!string.IsNullOrEmpty(config.City) && config.City.Length > 100)
                    return "City must be 100 characters or less.";
                break;
            case nameof(LogConfig.County):
                if (!string.IsNullOrEmpty(config.County) && config.County.Length > 100)
                    return "County must be 100 characters or less.";
                break;
            case nameof(LogConfig.CountyCode):
                if (!string.IsNullOrEmpty(config.CountyCode) && config.CountyCode.Length > 100)
                    return "County Code must be 100 characters or less.";
                break;
            case nameof(LogConfig.State):
                if (!string.IsNullOrEmpty(config.State) && config.State.Length > 100)
                    return "State must be 100 characters or less.";
                break;
            case nameof(LogConfig.Dxcc):
                if (config.Dxcc < 0)
                    return "DXCC must be non-negative.";
                break;
            case nameof(LogConfig.ProKey):
                if (config.ProKey < 0)
                    return "Pro Key must be non-negative.";
                break;
        }
        return string.Empty;
    }

    private string ValidateCallBookConf(string propertyName, CallBookConf config)
    {
        switch (propertyName)
        {
            case nameof(CallBookConf.Name):
                if (string.IsNullOrWhiteSpace(config.Name))
                    return "Name is required.";
                if (config.Name.Length > 50)
                    return "Name must be 50 characters or less.";
                break;
            case nameof(CallBookConf.Host):
                if (string.IsNullOrWhiteSpace(config.Host))
                    return "Host is required.";
                if (!Regex.IsMatch(config.Host, @"^([a-zA-Z0-9-]+\.)*[a-zA-Z0-9-]+\.[a-zA-Z]{2,}$"))
                    return "Invalid hostname format (e.g., api.example.com).";
                break;
            case nameof(CallBookConf.Port):
                if (config.Port < 1 || config.Port > 65535)
                    return "Port must be between 1 and 65535.";
                break;
            case nameof(CallBookConf.UserName):
                if (!string.IsNullOrEmpty(config.UserName) && config.UserName.Length > 100)
                    return "Username must be 100 characters or less.";
                break;
            case nameof(CallBookConf.Password):
                if (!string.IsNullOrEmpty(config.Password) && config.Password.Length > 100)
                    return "Password must be 100 characters or less.";
                break;
            case nameof(CallBookConf.ApiKey):
                if (!string.IsNullOrEmpty(config.ApiKey) && config.ApiKey.Length > 100)
                    return "API Key must be 100 characters or less.";
                break;
        }
        return string.Empty;
    }

    private string ValidateRigCtlConf(string propertyName, RigCtlConf config)
    {
        switch (propertyName)
        {
            case nameof(RigCtlConf.Name):
                if (string.IsNullOrWhiteSpace(config.Name))
                    return "Name is required.";
                if (config.Name.Length > 50)
                    return "Name must be 50 characters or less.";
                break;
            case nameof(RigCtlConf.Host):
                if (string.IsNullOrWhiteSpace(config.Host))
                    return "Host is required.";
                if (!Regex.IsMatch(config.Host, @"^([a-zA-Z0-9-]+\.)*[a-zA-Z0-9-]+\.[a-zA-Z]{2,}$"))
                    return "Invalid hostname format (e.g., api.example.com).";
                break;
            case nameof(RigCtlConf.Port):
                if (config.Port < 1 || config.Port > 65535)
                    return "Port must be between 1 and 65535.";
                break;
            case nameof(RigCtlConf.TunerName):
                if (!string.IsNullOrEmpty(config.TunerName) && config.TunerName.Length > 100)
                    return "Tuner Name must be 100 characters or less.";
                break;
        }
        return string.Empty;
    }

    private string ValidateDxClusterConf(string propertyName, DxClusterConf config)
    {
        switch (propertyName)
        {
            case nameof(DxClusterConf.Host):
                if (string.IsNullOrWhiteSpace(config.Host))
                    return "Host is required.";
                if (!Regex.IsMatch(config.Host, @"^([a-zA-Z0-9-]+\.)*[a-zA-Z0-9-]+\.[a-zA-Z]{2,}$"))
                    return "Invalid hostname format (e.g., dx.example.com).";
                break;
            case nameof(DxClusterConf.Port):
                if (config.Port < 1 || config.Port > 65535)
                    return "Port must be between 1 and 65535.";
                break;
            case nameof(DxClusterConf.UserName):
                if (!string.IsNullOrEmpty(config.UserName) && config.UserName.Length > 100)
                    return "Username must be 100 characters or less.";
                break;
            case nameof(DxClusterConf.Password):
                if (!string.IsNullOrEmpty(config.Password) && config.Password.Length > 100)
                    return "Password must be 100 characters or less.";
                break;
        }
        return string.Empty;
    }

    partial void OnSelectedOptionChanged(string? value)
    {
        if (value.IsNullOrEmpty()) return;
        _logger.LogInformation("Selected option changed to: {Option}", value);
        if (value.ToLower() == "new config")
        {
            IsMainVisible = true;
            SelectedConfig = new LogConfig();
            CallBookConfigs.Clear();
            RigCtlConfigs.Clear();
            DxClusterConfigs.Clear();
            SelectedCallBook = null;
            SelectedRigCtl = null;
            SelectedDxCluster = null;
            _validationErrors.Clear();
        }
        else if (value != "None")
        {
            var config = Configs.FirstOrDefault(c => c.ProfileName == value);
            if (config != null)
            {
                SelectedConfig = config;
                IsMainVisible = true;
                CallBookConfigs.Clear();
                RigCtlConfigs.Clear();
                DxClusterConfigs.Clear();
                if (!config.Logbooks.IsNullOrEmpty())
                    foreach (var cb in config.Logbooks) CallBookConfigs.Add(cb);
                if (!config.RigControls.IsNullOrEmpty())
                    foreach (var rc in config.RigControls) RigCtlConfigs.Add(rc);
                if (!config.DxClusters.IsNullOrEmpty())
                    foreach (var dc in config.DxClusters) DxClusterConfigs.Add(dc);
                SelectedCallBook = CallBookConfigs.FirstOrDefault();
                SelectedRigCtl = RigCtlConfigs.FirstOrDefault();
                SelectedDxCluster = DxClusterConfigs.FirstOrDefault();
                _validationErrors.Clear();
                // Validate LogConfig
                foreach (var prop in typeof(LogConfig).GetProperties())
                {
                    var error = ValidateLogConfig(prop.Name, SelectedConfig);
                    if (!string.IsNullOrEmpty(error))
                        _validationErrors[prop.Name] = error;
                }
            }
        }
        else
        {
            IsMainVisible = false;
            SelectedConfig = new LogConfig();
            CallBookConfigs.Clear();
            RigCtlConfigs.Clear();
            DxClusterConfigs.Clear();
            SelectedCallBook = null;
            SelectedRigCtl = null;
            SelectedDxCluster = null;
            _validationErrors.Clear();
        }
        OnPropertyChanged(nameof(HasErrors));
    }

    partial void OnSelectedCallBookChanged(CallBookConf? value)
    {
        if (value != null)
        {
            _validationErrors.Clear();
            foreach (var prop in typeof(CallBookConf).GetProperties())
            {
                var error = ValidateCallBookConf(prop.Name, value);
                if (!string.IsNullOrEmpty(error))
                    _validationErrors[prop.Name] = error;
            }
            OnPropertyChanged(nameof(HasErrors));
        }
    }

    partial void OnSelectedRigCtlChanged(RigCtlConf? value)
    {
        if (value != null)
        {
            _validationErrors.Clear();
            foreach (var prop in typeof(RigCtlConf).GetProperties())
            {
                var error = ValidateRigCtlConf(prop.Name, value);
                if (!string.IsNullOrEmpty(error))
                    _validationErrors[prop.Name] = error;
            }
            OnPropertyChanged(nameof(HasErrors));
        }
    }

    partial void OnSelectedDxClusterChanged(DxClusterConf? value)
    {
        if (value != null)
        {
            _validationErrors.Clear();
            foreach (var prop in typeof(DxClusterConf).GetProperties())
            {
                var error = ValidateDxClusterConf(prop.Name, value);
                if (!string.IsNullOrEmpty(error))
                    _validationErrors[prop.Name] = error;
            }
            OnPropertyChanged(nameof(HasErrors));
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
            if (SelectedConfig == null || _validationErrors.Any())
            {
                Message = "Please correct validation errors: " + string.Join("; ", _validationErrors.Values);
                return;
            }

            SelectedConfig.IsDirty = true;
            if (SelectedOption.ToLower() == "new config")
            {
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

    [RelayCommand]
    private async Task SaveCallBook()
    {
        try
        {
            if (SelectedCallBook == null || _validationErrors.Any())
            {
                Message = "Please correct Call Book validation errors: " + string.Join("; ", _validationErrors.Values);
                return;
            }

            SelectedCallBook.isDirty = true;
            if (!CallBookConfigs.Contains(SelectedCallBook))
            {
                SelectedCallBook.LogConfigId = SelectedConfig.Id;
                // await _apiService.AddCallBookConfAsync(SelectedCallBook, SelectedConfig.Id);
                CallBookConfigs.Add(SelectedCallBook);
                SelectedConfig.Logbooks.Add(SelectedCallBook);
                Message = "Call Book saved successfully";
            }
            else
            {
                // await _apiService.UpdateCallBookConfAsync(SelectedCallBook);
                Message = "Call Book updated successfully";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving Call Book");
            Message = "Failed to save Call Book";
        }
    }

    [RelayCommand]
    private async Task SaveRigCtl()
    {
        try
        {
            if (SelectedRigCtl == null || _validationErrors.Any())
            {
                Message = "Please correct Rig Control validation errors: " + string.Join("; ", _validationErrors.Values);
                return;
            }

            SelectedRigCtl.isDirty = true;
            if (!RigCtlConfigs.Contains(SelectedRigCtl))
            {
                SelectedRigCtl.LogConfigId = SelectedConfig.Id;
                // await _apiService.AddRigCtlConfAsync(SelectedRigCtl, SelectedConfig.Id);
                RigCtlConfigs.Add(SelectedRigCtl);
                SelectedConfig.RigControls.Add(SelectedRigCtl);
                Message = "Rig Control saved successfully";
            }
            else
            {
                // await _apiService.UpdateRigCtlConfAsync(SelectedRigCtl);
                Message = "Rig Control updated successfully";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving Rig Control");
            Message = "Failed to save Rig Control";
        }
    }

    [RelayCommand]
    private async Task SaveDxCluster()
    {
        try
        {
            if (SelectedDxCluster == null || _validationErrors.Any())
            {
                Message = "Please correct DX Cluster validation errors: " + string.Join("; ", _validationErrors.Values);
                return;
            }

            SelectedDxCluster.isDirty = true;
            if (!DxClusterConfigs.Contains(SelectedDxCluster))
            {
                SelectedDxCluster.LogConfigId = SelectedConfig.Id;
                // await _apiService.AddDxClusterConfAsync(SelectedDxCluster, SelectedConfig.Id);
                DxClusterConfigs.Add(SelectedDxCluster);
                SelectedConfig.DxClusters.Add(SelectedDxCluster);
                Message = "DX Cluster saved successfully";
            }
            else
            {
                // await _apiService.UpdateDxClusterConfAsync(SelectedDxCluster);
                Message = "DX Cluster updated successfully";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving DX Cluster");
            Message = "Failed to save DX Cluster";
        }
    }

    [RelayCommand]
    private void AddNewCallBook()
    {
        SelectedCallBook = new CallBookConf("New Call Book", "api.example.com");
    }

    [RelayCommand]
    private void AddNewRigCtl()
    {
        SelectedRigCtl = new RigCtlConf("New Rig Control", "rig.example.com");
    }

    [RelayCommand]
    private void AddNewDxCluster()
    {
        SelectedDxCluster = new DxClusterConf("dx.example.com");
    }
}
