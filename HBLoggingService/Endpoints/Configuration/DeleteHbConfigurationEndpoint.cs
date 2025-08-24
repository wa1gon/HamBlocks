namespace HBLoggingService.Endpoints.Configuration;

public class DeleteHbConfigurationEndpoint : ConfEndpointBase
{
    private readonly HbConfigurationService _service;
    public DeleteHbConfigurationEndpoint(HbConfigurationService service) => _service = service;

    public override void Configure()
    {
        Delete($"/{BasePath}/{{Id}}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LogConfig? req, CancellationToken ct)
    {
        var id = Route<string>("id");
        await _service.DeleteAsync(req.Id);
        await SendOkAsync(ct);
    }
}
