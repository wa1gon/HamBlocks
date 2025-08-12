using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HBWebLogger.Pages.Configuration;

public partial class HbConfiguration : ComponentBase
{
    private readonly DialogOptions _maxWidth = new() { MaxWidth = MaxWidth.Medium, FullWidth = true };
    private readonly DialogOptions _closeButton = new() { CloseButton = true };
    private readonly DialogOptions _noHeader = new() { NoHeader = true };
    private readonly DialogOptions _backdropClick = new() { BackdropClick = false };
    private readonly DialogOptions _fullScreen = new() { FullScreen = true, CloseButton = true };
    private readonly DialogOptions _topCenter = new() { Position = DialogPosition.TopCenter };
    private List<HBConfiguration>? _configs = [];
    [Inject] public HbConfigurationApiService? ConfServ { get; set; }

    protected override async Task OnInitializedAsync()
    {   
        _configs = await ConfServ!.GetAllAsync();
    }
    private Task OpenDialogAsync(DialogOptions options)
    {
        return Dialog.ShowAsync<ConfigurationDialog>("Custom Options Dialog", options);
    }
}
