

using HbLibrary.Extensions;

namespace HBLoggingService.Endpoints.Qsos;

using FastEndpoints;

public class FilterQsoEndpoint(LoggingDbContext _db,ILogger<FilterQsoEndpoint> _logger) : 
    Endpoint<QsoFilterRequest, List<Qso>>
{

    // private readonly LoggingDbContext _db;
    // public FilterQsoEndpoint(LoggingDbContext db)
    // {
    //     _db = db;
    // }

    public override void Configure()
    {
        Get("/qso/filter");
        AllowAnonymous();
        Summary(s => {
            s.Summary = "Filter QSO records by callsign and date with pagination";
        });
    }

    public override async Task HandleAsync(QsoFilterRequest req, CancellationToken ct)
    {
        
        var query = _db.Qsos.AsQueryable();

        if (!string.IsNullOrEmpty(req.Call))
            query = query.Where(q => q.Call.StartsWith(req.Call));
        
        if (req.Date.HasValue)
            query = query.Where(q => q.QsoDate.Date == req.Date.Value.Date);
        
        if (req.Country.IsNotEmptyOrNull())
            query = query.Where(q => q.Country == req.Country);
        
        if (req.State.IsNotEmptyOrNull())
            query = query.Where(q => q.State == req.State);
        // if (req.County.IsNotEmptyOrNull())
        //     query = query.Where(q => q.County == req.County);
        if (req.Band.IsNotEmptyOrNull())
            query = query.Where(q => q.Band == req.Band);

        var skip = (req.PageNumber - 1) * req.PageSize;
        var result = await query
            .OrderBy(q => q.QsoDate)
            .Skip(skip)
            .Take(req.PageSize)
            .ToListAsync(ct);
        await SendAsync( result);
        
        // var query = _db.Qsos.AsQueryable();
        //
        // if (!string.IsNullOrEmpty(req.Call))
        //     query = query.Where(q => q.Call.StartsWith(req.Call));

        // if (req.Date.HasValue)
        //     query = query.Where(q => q.QsoDate == req.Date.Value.Date);

        // Apply pagination
        // var skip = (req.PageNumber - 1) * req.PageSize;
        
        
        // var result = await query
        //     .OrderBy(q => q.QsoDate) // Ensure consistent ordering
        //     .Skip(skip)
        //     .Take(req.PageSize)
        //     // .Select(q => new QsoFilterResponse
        //     // {
        //     //     Id = q.Id,
        //     //     Call = q.Call,
        //     //     QsoDate = q.QsoDate,
        //     //     Mode = q.Mode,
        //     //     Freq = q.Freq,
        //     //     Band = q.Band
        //     //     // Map other properties as needed
        //     // }
        //     // )
        //     .ToListAsync(ct);

        // await SendAsync(result);
    }
}
