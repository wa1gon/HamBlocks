
namespace HBWebLogger.Pages.Configuration;

public partial class HbConfiguration : ComponentBase
{
    private List<HamBlocks.Library.Models.LogConfig> configList = new();
    private int commitCount = 0;
    [Inject]
    public HbConfigurationApiService? ConfServ { get; set; }

    protected override async Task OnInitializedAsync()
    {
        // Fetch configurations from the API
        configList = await ConfServ!.GetAllAsync() ?? new List<LogConfig>();
        Console.WriteLine($"Loaded {configList.Count} configurations");
    }

    private void AddRow()
    {
        configList.Add(new ()
        {
            ProfileName = "new-profile",
            Callsign = "NOCALL",
            IsDirty = true
        });
        StateHasChanged();
    }

    private void StartedEditingItem(LogConfig config)
    {
        Console.WriteLine($"Editing started for {config.ProfileName}");
    }
    
    private EventCallback<DataGridRowClickEventArgs<LogConfig>> RowClickCallback =>
        EventCallback.Factory.Create<DataGridRowClickEventArgs<LogConfig>>(this, ConfigRowClicked);

    private void ConfigRowClicked(DataGridRowClickEventArgs<LogConfig> args)
    {
        // Your logic here
    }

    private void CanceledEditingItem(LogConfig config)
    {
        Console.WriteLine($"Editing canceled for {config.ProfileName}");
    }

    private async Task CommittedItemChanges(LogConfig config)
    {
        config.Callsign = config.Callsign?.ToUpper() ?? "NOCALL";
        Console.WriteLine($"Changes committed for {config.ProfileName} {commitCount++}");
        await Task.CompletedTask;
    }
    public async Task SaveChanges()
    {
        foreach (var config in configList)
        {
            await CommittedItemChanges(config);
        }
        Console.WriteLine("All changes saved.");
    }
    public void CancelChanges()
    {
        // Reload configs from API to discard changes
        _ = OnInitializedAsync();
        Console.WriteLine("Changes canceled.");
    }
    void OnCallsignChanged(LogConfig item, string value)
    {
        item.Callsign = value?.ToUpper() ?? "NOCALL";
        item.IsDirty = true;
    }   

}
