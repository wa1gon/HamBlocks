namespace LoggerAvalonia.View.Factories;


public class PageFactory(Func<ApplicationPageNames, PageViewModel> func)
{
	public PageViewModel GetPageViewModel(ApplicationPageNames pageName) => func.Invoke(pageName);
}
