namespace HBLoggingService.Endpoints.Configuration;

public class GetAllHbConfigurationsEndpoint : EndpointWithoutRequest<List<LogConfig>>
{
    private readonly HbConfigurationService _service;

    public GetAllHbConfigurationsEndpoint(HbConfigurationService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        try
        {
            Get("/conf");
            AllowAnonymous();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        Get("/conf");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        try
        {
            var configs = await _service.GetAllAsync();
            await SendAsync(configs, cancellation: ct);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}