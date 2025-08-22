namespace HBLoggingService.Endpoints.Configuration;

public class UpdateHbConfigurationEndpoint : Endpoint<LogConfig>
{
    private readonly HbConfigurationService _service;
    public UpdateHbConfigurationEndpoint(HbConfigurationService service) => _service = service;

    public override void Configure()
    {
        Put("/hbconfigurations");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LogConfig req, CancellationToken ct)
    {
        await _service.UpdateAsync(req);
        await SendOkAsync(ct);
    }
}
