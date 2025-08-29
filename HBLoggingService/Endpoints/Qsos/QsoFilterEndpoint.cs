namespace HBLoggingService.Endpoints.Qsos;

public class FilterQsoEndpoint(LoggingDbContext _db, ILogger<FilterQsoEndpoint> _logger) :
    Endpoint<QsoFilterRequest, List<Qso>>
{
    public override void Configure()
    {
        Get("/qso/filter");
        AllowAnonymous();
        Summary(s => { s.Summary = "Filter QSO records by callsign and date with pagination"; });
    }

    public override async Task HandleAsync(QsoFilterRequest req, CancellationToken ct)
    {
        var query = _db.Qsos.AsQueryable();

        if (!string.IsNullOrEmpty(req.Call))
            query = query.Where(q => q.Call.StartsWith(req.Call));

        var skip = (req.PageNumber - 1) * req.PageSize;
        var result = await query
            .OrderBy(q => q.QsoDate)
            .Skip(skip)
            .Take(req.PageSize)
            .ToListAsync(ct);
        await SendAsync(result);
        _logger.LogInformation("Filtered {Count} QSOs for call {Call} on page {PageNumber}",
            result.Count, req.Call, req.PageNumber);
    }
}