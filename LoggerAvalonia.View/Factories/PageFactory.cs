using System;
using realworld_avalonia.Data;
using realworld_avalonia.ViewModels;

namespace realworld_avalonia.Factories;

// public class PageFactory
// {
// 	private readonly Func<ApplicationPageNames, PageViewModel> pageFactory;
// 	public PageFactory(Func<ApplicationPageNames, PageViewModel> factory)
// 	{
// 			pageFactory = factory;
// 	}

// 	public PageViewModel GetPageViewModel(ApplicationPageNames pageName) => pageFactory.Invoke(pageName);
// }

public class PageFactory(Func<ApplicationPageNames, PageViewModel> func)
{
	public PageViewModel GetPageViewModel(ApplicationPageNames pageName) => func.Invoke(pageName);
}