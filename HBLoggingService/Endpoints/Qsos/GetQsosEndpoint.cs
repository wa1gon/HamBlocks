namespace HBLoggingService.Endpoints.Qsos;

public class GetQsosEndpoint(LoggingDbContext _db) : Endpoint<GetQsosRequest, List<Qso>>
{
    public override void Configure()
    {
        Get("/api/qsos");
        AllowAnonymous();
        Summary(s => { s.Summary = "Get all QSO records, optionally filtered by call sign"; });
    }

    public override async Task HandleAsync(GetQsosRequest req, CancellationToken ct)
    {
        var query = _db.Qsos.AsQueryable();

        if (!string.IsNullOrEmpty(req.Call))
            query = query.Where(q => q.Call == req.Call.ToUpper());

        query = query.OrderByDescending(q => q.QsoDate);

        if (req.Skip > 0)
            query = query.Skip(req.Skip);

        if (req.Take > 0)
            query = query.Take(req.Take);


        if (req.IncludeDetails)
            query = query.Include(q => q.Details);

        var result = await query
            .ToListAsync(ct);

        await SendAsync(result);
    }
}
