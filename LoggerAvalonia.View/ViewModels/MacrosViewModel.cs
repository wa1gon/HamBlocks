using Avalonia;
using realworld_avalonia.Data;

namespace realworld_avalonia.ViewModels;

public partial class MacrosViewModel : PageViewModel
{
  public string Test { get; set; } = "Macros";
  public MacrosViewModel()
  {
      PageName = ApplicationPageNames.Macros;
  }
}