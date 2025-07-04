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
            await SendAsync(new UploadFileResponse { Message = "No file uploaded." }, StatusCodes.Status400BadRequest, ct);
            return;
        }

        using var stream = req.File.OpenReadStream();
        using var reader = new StreamReader(stream);
        var adifContent = await reader.ReadToEndAsync();
        
        var records = AdifReader.ReadFromString(adifContent); // Adjust method as per hbLibrary API
        _db.Qsos.AddRange(records);
        await _db.SaveChangesAsync(ct);
        await SendAsync(new UploadFileResponse
        {
            Message = $"File '{req.File.FileName}' uploaded. Parsed {records.Count} ADIF records."
        }, cancellation: ct);
    }
}
