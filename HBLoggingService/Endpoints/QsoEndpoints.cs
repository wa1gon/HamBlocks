using HBLoggingService.Data;

namespace HBLoggingService.Endpoints;

public class CreateQsoEndpoint : Endpoint<Qso, Qso>
{
    private readonly LoggingDbContext _db;
    public CreateQsoEndpoint(LoggingDbContext db) => _db = db;

    public override void Configure()
    {
        Post("/qso");
        AllowAnonymous();
        Description(b => b
            .Produces<Qso>(200, "application/json"));
    }
    public override async Task HandleAsync(Qso req, CancellationToken ct)
    {
        _db.Qsos.Add(req);
        await _db.SaveChangesAsync(ct);
        await SendAsync(req);
    }
}
