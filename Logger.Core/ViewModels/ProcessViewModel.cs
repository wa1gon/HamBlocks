namespace Logger.Core.ViewModels;

public partial class ProcessViewModel : PageViewModel
{
  public string Test { get; set; } = "Process";
  public ProcessViewModel()
  {
      PageName = ApplicationPageNames.Process;
  }
}
