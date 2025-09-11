using realworld_avalonia.Data;

namespace realworld_avalonia.ViewModels;

public partial class ProcessViewModel : PageViewModel
{
  public string Test { get; set; } = "Process";
  public ProcessViewModel()
  {
      PageName = ApplicationPageNames.Process;
  }
}