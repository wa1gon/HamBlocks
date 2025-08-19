#if false
namespace HBWebLogger.Pages.Configuration;
using MudBlazor;

public partial class ConfigurationDialog : ComponentBase
{
    [Parameter] public HamBlocks.Library.Models.LogConfig Config { get; set; } = new LogConfig
    {
        ProfileName = string.Empty,
        Callsign = string.Empty
        // Set other required properties if needed
    };

    private MudDataGrid<LogConfig> dataGrid;
    private List<LogConfig> configList;
    
    [CascadingParameter] IMudDialogInstance MudDialog { get; set; }

    protected override void OnInitialized()
    {
        configList = new List<LogConfig> { Config };
    }

    private void Save() => MudDialog.Close(DialogResult.Ok(Config));
    private void Cancel() => MudDialog.Cancel();
    private void AddRow()
    {
        configList.Add(new LogConfig
        {
            ProfileName = string.Empty,
            Callsign = string.Empty
            // Set other required properties if needed
        });
        StateHasChanged();
    }
}
#endif
