
namespace HBWebLogger.Pages.Configuration;

public partial class HbConfiguration : ComponentBase
{
    private List<HamBlocks.Library.Models.LogConfig> configList = new();
    private int commitCount = 0;
    private List<DxccEntity> entities = [];
    [Inject]
    public HbConfClientApiService? ConfServ { get; set; }
    [Inject]
    public DxccInfoClientService? DxccServ { get; set; }

    protected override async Task OnInitializedAsync()
    {
        // Fetch configurations from the API
        configList = await ConfServ!.GetAllAsync() ?? [];
        Console.WriteLine($"Loaded {configList.Count} configurations");
        entities = (await DxccServ!.GetAllAsync()).ToList();
    }

    private void AddRow()
    {
        var item = new LogConfig
        {
            ProfileName = "new-profile",
            Callsign = "NOCALL",
            IsDirty = true
        };
        configList.Add(item);
        StateHasChanged();
    }

    private static void StartedEditingItem(LogConfig config)
    {
        config.IsDirty = true;
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

        try
        {
            config.Callsign = config.Callsign?.ToUpper() ?? "NOCALL";
            config.IsDirty = true;
            Console.WriteLine($"Changes committed for {config.ProfileName} {commitCount++}");
            await Task.CompletedTask;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task SaveChanges()
    {
        foreach (var config in configList)
        {
            await ConfServ.AddAsync(config);
            config.IsDirty = false;
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
