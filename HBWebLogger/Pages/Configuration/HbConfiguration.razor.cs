
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
        });
        StateHasChanged();
    }

    private void StartedEditingItem(HamBlocks.Library.Models.LogConfig config)
    {
        Console.WriteLine($"Editing started for {config.ProfileName}");
    }

    private void CanceledEditingItem(HamBlocks.Library.Models.LogConfig config)
    {
        Console.WriteLine($"Editing canceled for {config.ProfileName}");
    }

    private async Task CommittedItemChanges(HamBlocks.Library.Models.LogConfig config)
    {
        Console.WriteLine($"Changes committed for {config.ProfileName} {commitCount++}");
        // Example: Update database via API
        // await ConfServ.UpdateAsync(config);
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
}
