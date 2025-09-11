using realworld_avalonia.Data;

namespace realworld_avalonia.ViewModels;

public partial class HistoryViewModel : PageViewModel
{
  public string Test { get; set; } = "History";
  public HistoryViewModel()
  {
      PageName = ApplicationPageNames.History;
  }
}