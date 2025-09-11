using realworld_avalonia.Data;

namespace realworld_avalonia.ViewModels;

public partial class ActionsViewModel : PageViewModel
{
  public string Test { get; set; } = "Actions";
  public ActionsViewModel()
  {
      PageName = ApplicationPageNames.Actions;
  }
}