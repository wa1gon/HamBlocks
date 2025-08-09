

namespace HBLoggingService.Endpoints.Configuration;

public class GetAllHbConfigurationsEndpoint : EndpointWithoutRequest<List<HBConfiguration>>
{
    private readonly HbConfigurationService _service;
    public GetAllHbConfigurationsEndpoint(HbConfigurationService service) => _service = service;

    public override void Configure()
    {
        Get("/hbconfigurations");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var configs = await _service.GetAllAsync();
        await SendAsync(configs, cancellation: ct);
    }
}
