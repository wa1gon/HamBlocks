namespace HBLoggingService.Endpoints.DxccEntries;

public class GetDxccEndpoint(DxccLookupService dxccService) : EndpointWithoutRequest<List<DxccEntity>>
{
    // public GetDxccEndpoint(DxccLookupService dxccLookupService)
    // {
    //     _dxccLookupService = dxccLookupService;
    // }

    public override void Configure()
    {
        Get("/dxcc");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        dxccService.ReadDxccInfo();
        await SendAsync(dxccService.DxccList, cancellation: ct);
    }
}