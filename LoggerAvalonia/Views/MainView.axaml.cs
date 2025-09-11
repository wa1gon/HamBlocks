using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Diagnostics;
namespace LoggerAvalonia.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        #if DEBUG
        this.AttachedToVisualTree += (_, __) =>
        {
            TopLevel.GetTopLevel(this)?.AttachDevTools();
        };
        #endif
        InitializeComponent();
    }
}
