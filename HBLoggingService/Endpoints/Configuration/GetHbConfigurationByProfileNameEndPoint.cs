namespace HBLoggingService.Endpoints.Configuration;

public class GetHbConfigurationByProfileNameEndpoint : Endpoint<GetByProfileNameRequest, LogConfig?>
{
    private readonly HbConfigurationService _service;
    public GetHbConfigurationByProfileNameEndpoint(HbConfigurationService service) => _service = service;

    public override void Configure()
    {
        Get("/hbconfigurations/{ProfileName}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetByProfileNameRequest req, CancellationToken ct)
    {
        var config = await _service.GetByProfileNameAsync(req.ProfileName);
        if (config is null)
            await SendNotFoundAsync(ct);
        else
            await SendAsync(config, cancellation: ct);
    }
}
