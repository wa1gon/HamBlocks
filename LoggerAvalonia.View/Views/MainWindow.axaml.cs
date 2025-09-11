using Avalonia.Controls;
using Avalonia.Input;
using realworld_avalonia.ViewModels;

namespace realworld_avalonia.Views;

public partial class MainWindow : Window
{
  public MainWindow()
  {
    InitializeComponent();
  }

  private void ImageOnPressed(object? sender, PointerPressedEventArgs e)
  {
    if (e.ClickCount != 2) return;
    
    (DataContext as MainWindowViewModel)?.SideMenuResizeCommand.Execute(null);
  }
}