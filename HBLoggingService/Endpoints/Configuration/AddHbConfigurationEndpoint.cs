namespace HBLoggingService.Endpoints.Configuration;

public class AddHbConfigurationEndpoint : ConfEndpointBase
{
    private readonly HbConfigurationService _service;

    public AddHbConfigurationEndpoint(HbConfigurationService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Post("/conf");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LogConfig? req, CancellationToken ct)
    {
        try
        {
            await _service.AddAsync(req);
            await SendOkAsync(ct);
        }
        catch (ArgumentException ae)
        {
            await SendAsync(new { Message = $"{ae.Message}" }, 409, ct);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
