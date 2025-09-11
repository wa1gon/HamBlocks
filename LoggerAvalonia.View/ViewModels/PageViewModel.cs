using CommunityToolkit.Mvvm.ComponentModel;
using realworld_avalonia.Data;

namespace realworld_avalonia.ViewModels;

public partial class PageViewModel : ViewModelBase
{
  [ObservableProperty]
  private ApplicationPageNames _pageName;
} 