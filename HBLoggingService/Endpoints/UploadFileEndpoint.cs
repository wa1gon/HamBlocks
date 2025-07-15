using HBLoggingService.Data;

public class UploadFileEndpoint : Endpoint<UploadFileRequest, UploadFileResponse>
{
    private readonly LoggingDbContext _db; 
    public UploadFileEndpoint(LoggingDbContext db)
    {
        _db = db;
    }
    public override void Configure()
    {
        Post("/upload");
        AllowAnonymous();
        Description(x => x
            .Accepts<UploadFileRequest>("multipart/form-data")
            .Produces<UploadFileResponse>(StatusCodes.Status200OK)
        );
    }

    public override async Task HandleAsync(UploadFileRequest req, CancellationToken ct)
    {
        if (req.File == null || req.File.Length == 0)
        {
            await SendAsync(new UploadFileResponse(  "No file uploaded.") , 
                StatusCodes.Status400BadRequest, ct);
            return;
        }

        List<Qso> validQsos = [];
        List<Qso> invalidQsos = [];
        var adifContent = await GetFileString(req);
        
        var records = AdifReader.ReadFromString(adifContent);
        
        foreach (var qso in records)
        {
            var minTime = qso.QsoDate.AddMinutes(-15);
            var maxTime = qso.QsoDate.AddMinutes(15);

            bool exists = await _db.Qsos.AnyAsync(x =>
                x.Id == qso.Id ||
                (x.Call == qso.Call &&
                x.Band == qso.Band &&
                x.Mode == qso.Mode &&
                x.QsoDate >= minTime &&
                x.QsoDate <= maxTime), ct);
            if (exists)
                invalidQsos.Add(qso);
            else
                validQsos.Add(qso);
        }
        
        _db.Qsos.AddRange(validQsos);
        await _db.SaveChangesAsync(ct);
        await SendAsync(new UploadFileResponse($"File '{req.File.FileName}' uploaded. Parsed {records.Count} ADIF records."), cancellation: ct);
    }

    private async Task<string> GetFileString(UploadFileRequest req)
    {
        using var stream = req.File.OpenReadStream();
        using var reader = new StreamReader(stream);
        var adifContent = await reader.ReadToEndAsync();
        return adifContent;
    }
}
