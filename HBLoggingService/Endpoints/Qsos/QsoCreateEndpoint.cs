namespace HBLoggingService.Endpoints.Qsos;

public class QsoCreateEndpoint: Endpoint<Qso>
{
    private readonly LoggingDbContext _db;

    public QsoCreateEndpoint(LoggingDbContext db)
    {

        _db = db;
    }        
    
    public override void Configure()
    {
        Post("/qso");
        AllowAnonymous();
        // Other options like Description(), Summary(), etc.
    }

    public override async Task HandleAsync(Qso req, CancellationToken ct)
    {
        await _db.Qsos.AddAsync(req, ct);
        await _db.SaveChangesAsync(ct);
        await SendAsync(req);
    }   
}
