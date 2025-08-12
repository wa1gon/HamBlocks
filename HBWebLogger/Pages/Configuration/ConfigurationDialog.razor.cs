namespace HBWebLogger.Pages.Configuration;

public partial class ConfigurationDialog: ComponentBase
{
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; }

    private void Submit() => MudDialog.Close(DialogResult.Ok(true));


    
    [Parameter] public HBConfiguration Config { get; set; } = new()
    {
        ProfileName = null,
        Callsign = null
    };
    private MudForm form;

    private void Save() => MudDialog.Close(DialogResult.Ok(Config));
    private void Cancel() => MudDialog.Cancel();
}
