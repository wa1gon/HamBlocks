using realworld_avalonia.Data;

namespace realworld_avalonia.ViewModels;

public partial class ReporterViewModel : PageViewModel
{
  public string Test { get; set; } = "Reporter";
  public ReporterViewModel()
  {
      PageName = ApplicationPageNames.Reporter;
  }
}