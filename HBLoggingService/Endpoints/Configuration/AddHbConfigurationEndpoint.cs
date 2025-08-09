namespace HBLoggingService.Endpoints.Configuration;

public class AddHbConfigurationEndpoint : Endpoint<HBConfiguration>
{
    private readonly HbConfigurationService _service;
    public AddHbConfigurationEndpoint(HbConfigurationService service) => _service = service;

    public override void Configure()
    {
        Post("/hbconfigurations");
        AllowAnonymous();
    }

    public override async Task HandleAsync(HBConfiguration req, CancellationToken ct)
    {
        await _service.AddAsync(req);
        await SendOkAsync(ct);
    }
}
