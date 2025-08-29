namespace HBWebLogger.Pages.Configuration;

public partial class HbConfiguration : ComponentBase
{
    private const string NewItemValue = "--NEW--";
    private List<LogConfig> Configurations = new();
    private LogConfig CurrentConfig = new();
    private MudForm? Form;
    private string SelectedConfig = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        Configurations = await ApiService.GetAllAsync() ?? new List<LogConfig>();
    }

    private async Task OnSelectionChanged(string newValue)
    {
        if (newValue == NewItemValue)
        {
            CurrentConfig = new LogConfig();
        }
        else
        {
            var selected = Configurations.FirstOrDefault(c => c.ProfileName == newValue);
            if (selected != null)
                CurrentConfig = new LogConfig
                {
                    ProfileName = selected.ProfileName
                };
        }

        // var selectedOption = newValue;
        // Console.WriteLine($"Selection changed to: {selectedOption}");
        // Add custom logic here, e.g., call a service or update data
        await Task.CompletedTask; // Placeholder for async operations
        // StateHasChanged(); // Refresh UI if needed
    }

    private async Task HandleSubmit()
    {
        if (Configurations.Any(c => c.ProfileName == CurrentConfig.ProfileName))
        {
            await ApiService.UpdateAsync(CurrentConfig);
        }
        else
        {
            await ApiService.AddAsync(CurrentConfig);
            Configurations.Add(CurrentConfig);
        }

        CurrentConfig = new LogConfig();
    }
}
