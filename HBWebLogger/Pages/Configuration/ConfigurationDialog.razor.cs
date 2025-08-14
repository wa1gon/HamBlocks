namespace HBWebLogger.Pages.Configuration;
using MudBlazor;

public partial class ConfigurationDialog : ComponentBase
{
    [Parameter] public HBConfiguration Config { get; set; } = new HBConfiguration
    {
        ProfileName = string.Empty,
        Callsign = string.Empty
        // Set other required properties if needed
    };

    private MudDataGrid<HBConfiguration> dataGrid;
    private List<HBConfiguration> configList;
    
    [CascadingParameter] IMudDialogInstance MudDialog { get; set; }

    protected override void OnInitialized()
    {
        configList = new List<HBConfiguration> { Config };
    }

    private void Save() => MudDialog.Close(DialogResult.Ok(Config));
    private void Cancel() => MudDialog.Cancel();
    private void AddRow()
    {
        configList.Add(new HBConfiguration
        {
            ProfileName = string.Empty,
            Callsign = string.Empty
            // Set other required properties if needed
        });
        StateHasChanged();
    }
}
