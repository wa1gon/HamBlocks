using realworld_avalonia.Data;

namespace realworld_avalonia.ViewModels;

public partial class HomeViewModel : PageViewModel
{
  public string Test { get; set; } = "Home";
  public HomeViewModel()
  {
      PageName = ApplicationPageNames.Home;
  }
}