namespace HBLoggingService.Endpoints.Configuration;

public class UpdateHbConfigurationEndpoint : ConfEndpointBase
{
    private readonly HbConfigurationService _service;
    public UpdateHbConfigurationEndpoint(HbConfigurationService service) => _service = service;

    public override void Configure()
    {
        Put("/conf");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LogConfig req, CancellationToken ct)
    {
        await _service.UpdateAsync(req);
        await SendOkAsync(ct);
    }
}
