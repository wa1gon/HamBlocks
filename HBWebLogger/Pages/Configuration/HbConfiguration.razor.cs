
namespace HBWebLogger.Pages.Configuration;

public partial class HbConfiguration: ComponentBase
{
    private List<HBConfiguration>? _configs = [];
    private HBConfiguration _editConfig = new HBConfiguration
    {
        ProfileName = "DefaultProfile",
        Callsign = "N0CALL"
        // Set other properties as needed
    };
    private bool _dialogOpen = false;
    private bool _isEdit = false;
    private MudForm? _form;
    [Inject] public HbConfigurationApiService? ConfigService { get; set; }

    protected override async Task OnInitializedAsync()
    {   
        _configs = await ConfigService!.GetAllAsync();
    }

    private void AddConfig()
    {
        _editConfig = new HBConfiguration
        {
            ProfileName = "DefaultProfile", // must be set, even if default is the same
            Callsign = "N0CALL"
        };
        _isEdit = false;
        _dialogOpen = true;
    }

    private void EditConfig(HBConfiguration config)
    {
        _editConfig = new HBConfiguration
        {
            ProfileName = config.ProfileName,
            Callsign = config.Callsign,
            // Copy other fields
        };
        _isEdit = true;
        _dialogOpen = true;
    }

    private async Task SaveConfig()
    {
        if (_isEdit)
            await ConfigService!.UpdateAsync(_editConfig);
        else
            await ConfigService!.AddAsync(_editConfig);

        _configs = await ConfigService!.GetAllAsync();
        _dialogOpen = false;
        StateHasChanged();
    }

    private async Task DeleteConfig(HBConfiguration config)
    {
        await ConfigService!.DeleteAsync(config.ProfileName);
        _configs = await ConfigService.GetAllAsync();
        StateHasChanged();
    }
}
