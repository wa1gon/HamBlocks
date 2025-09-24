namespace Logger.Core.ViewModel;

public partial class HistoryViewModel : PageViewModel
{
  public string Test { get; set; } = "History";
  public HistoryViewModel()
  {
      PageName = ApplicationPageNames.History;
  }
}
