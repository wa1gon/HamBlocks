namespace HBLoggingService.Endpoints.Configuration;

public class DeleteHbConfigurationEndpoint : Endpoint<DeleteByProfileNameRequest>
{
    private readonly HbConfigurationService _service;
    public DeleteHbConfigurationEndpoint(HbConfigurationService service) => _service = service;

    public override void Configure()
    {
        Delete("/hbconfigurations/{ProfileName}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteByProfileNameRequest req, CancellationToken ct)
    {
        await _service.DeleteAsync(req.ProfileName);
        await SendOkAsync(ct);
    }
}
