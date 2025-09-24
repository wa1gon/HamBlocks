
namespace Logger.Core.ViewModel;

public partial class MacrosViewModel : PageViewModel
{
  public string Test { get; set; } = "Macros";
  public MacrosViewModel()
  {
      PageName = ApplicationPageNames.Macros;
  }
}
