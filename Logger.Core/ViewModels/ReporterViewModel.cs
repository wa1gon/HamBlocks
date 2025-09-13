

namespace Logger.Core.ViewModels;

public partial class ReporterViewModel : PageViewModel
{
  public string Test { get; set; } = "Reporter";
  public ReporterViewModel()
  {
      PageName = ApplicationPageNames.Reporter;
  }
}
