using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using realworld_avalonia.Data;

namespace realworld_avalonia.ViewModels;

public partial class SettingsViewModel : PageViewModel
{
  public string Test { get; set; } = "Settings";

  [ObservableProperty]
  private List<string> _locationPaths;

  public SettingsViewModel()
  {
    PageName = ApplicationPageNames.Settings;

    LocationPaths = [
      @"C:\Users\Luke\Downloads\TestActions",
      @"C:\Users\Luke\Documents\BatchProcess",
      @"X:\Shared\BatchProcess\Templates",
    ];
  }
}