
using Logger.Core.Factories;

namespace Logger.Core.ViewModel;

public partial class MainWindowViewModel : ViewModelBase
{
    // public string Greeting { get; } = "Welcome to Avalonia!";
    [ObservableProperty]
    private bool _isExpanded = true;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HomeViewIsActive))]
    [NotifyPropertyChangedFor(nameof(ProcessViewIsActive))]
    [NotifyPropertyChangedFor(nameof(ActionsViewIsActive))]
    [NotifyPropertyChangedFor(nameof(MacrosViewIsActive))]
    [NotifyPropertyChangedFor(nameof(ReporterViewIsActive))]
    [NotifyPropertyChangedFor(nameof(HistoryViewIsActive))]
    [NotifyPropertyChangedFor(nameof(SettingsViewIsActive))]
    private PageViewModel _currentView;
    // private ViewModelBase _currentView;
    private PageFactory _pageFactory;

    public bool HomeViewIsActive => CurrentView.PageName == ApplicationPageNames.Home;
    public bool ProcessViewIsActive => CurrentView.PageName == ApplicationPageNames.Process;
    public bool ActionsViewIsActive => CurrentView.PageName == ApplicationPageNames.Actions;
    public bool MacrosViewIsActive => CurrentView.PageName == ApplicationPageNames.Macros;
    public bool ReporterViewIsActive => CurrentView.PageName == ApplicationPageNames.Reporter;
    public bool HistoryViewIsActive => CurrentView.PageName == ApplicationPageNames.History;
    public bool SettingsViewIsActive => CurrentView.PageName == ApplicationPageNames.Settings;

    // private readonly HomeViewModel _home;
    // private readonly ProcessViewModel _process;
    // private readonly ActionsViewModel _actions;
    // private readonly MacrosViewModel _macros;
    // private readonly ReporterViewModel _reporter;
    // private readonly HistoryViewModel _history;

    public MainWindowViewModel(PageFactory pageFactory)
    {
        _pageFactory = pageFactory;
        // CurrentView = _home;
        GoToHome();
    }

    [RelayCommand]
    private void SideMenuResize() => IsExpanded = !IsExpanded;

    [RelayCommand]
    private void GoToHome() => CurrentView = _pageFactory.GetPageViewModel(ApplicationPageNames.Home);

    [RelayCommand]
    private void GoToProcess() => CurrentView = _pageFactory.GetPageViewModel(ApplicationPageNames.Process);
    [RelayCommand]
    private void GoToActions() => CurrentView = _pageFactory.GetPageViewModel(ApplicationPageNames.Actions);
    [RelayCommand]
    private void GoToMacros() => CurrentView = _pageFactory.GetPageViewModel(ApplicationPageNames.Macros);
    [RelayCommand]
    private void GoToReporter() => CurrentView = _pageFactory.GetPageViewModel(ApplicationPageNames.Reporter);
    [RelayCommand]
    private void GoToHistory() => CurrentView = _pageFactory.GetPageViewModel(ApplicationPageNames.History);
    [RelayCommand]
    private void GoToSettings() => CurrentView = _pageFactory.GetPageViewModel(ApplicationPageNames.Settings);

}
