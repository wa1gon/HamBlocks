namespace HBLoggingService.Endpoints.Qsos;

public class QsoUpdateEndpoint(LoggingDbContext _db) : Endpoint<Qso>
{
    // private readonly LoggingDbContext _db;
    //
    // public QsoUpdateEndpoint(LoggingDbContext db)
    // {
    //     _db = db;
    // }

    public override void Configure()
    {
        Put("/qsos/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Qso req, CancellationToken ct)
    {
        var existing = await _db.Qsos.FindAsync(new object[] { req.Id }, ct);
        if (existing == null)
        {
            await SendNotFoundAsync();
            return;
        }

        _db.Entry(existing).CurrentValues.SetValues(req);
        await _db.SaveChangesAsync(ct);
        await SendAsync(existing);
    }
}