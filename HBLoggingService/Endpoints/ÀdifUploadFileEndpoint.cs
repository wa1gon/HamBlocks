using HBLoggingService.Data;

public class ÀdifUploadFileEndpoint : Endpoint<AdifUploadFileRequest, AdifUploadFileResponse>
{
    private readonly ILogger _logger;
    private IServiceProvider _serviceProvider;
    public ÀdifUploadFileEndpoint(ILogger<Endpoint<AdifUploadFileRequest, AdifUploadFileResponse>> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public override void Configure()
    {
        Post("/uploadadif");
        AllowAnonymous();
        Description(x => x
            .Accepts<AdifUploadFileRequest>("multipart/form-data")
            .Produces<AdifUploadFileResponse>(StatusCodes.Status200OK)
        );
    }

    public override async Task HandleAsync(AdifUploadFileRequest req, CancellationToken ct)
    {

        // Start background processing

        _logger.LogInformation("Start of ADIF file upload...");
        if (req.File == null || req.File.Length == 0)
        {
            await SendAsync(new AdifUploadFileResponse("No file uploaded."),
                StatusCodes.Status400BadRequest, CancellationToken.None);
            return;
        }

        int totalRecords = 0;
        int totalErrors = 0;
        // ... process file, update totalRecords and totalErrors

        // Log to database
        var log = new ServerLog()
        {
            TotalErrors = totalErrors,
            Message = "Start of ADIF file upload",
            Dtg = DateTime.UtcNow
        };
        if (req.File == null || req.File.Length == 0)
        {
            _logger.LogInformation("No file uploaded.");
            await SendAsync(new AdifUploadFileResponse("No file uploaded."),
                StatusCodes.Status400BadRequest, CancellationToken.None);
            return;
        }

        List<Qso> validQsos = [];
        List<Qso> invalidQsos = [];
        var adifContent = GetFileString(req);
        var records = AdifReader.ReadFromString(adifContent);
        await SendAsync(
            new AdifUploadFileResponse(
                $"File '{req.File.FileName}' uploaded. Processing in background."),
            cancellation: ct);
        _ = Task.Run(async () =>
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                using var db = scope.ServiceProvider.GetRequiredService<LoggingDbContext>();
                
                _logger.LogInformation("ADIF parse complete. Parsed {Count} records.", records.Count);
                foreach (var qso in records)
                {
                    var minTime = qso.QsoDate.AddMinutes(-15);
                    var maxTime = qso.QsoDate.AddMinutes(15);

                    bool exists = await db.Qsos.AnyAsync(x =>
                        x.Id == qso.Id ||
                        (x.Call == qso.Call &&
                         x.Band == qso.Band &&
                         x.Mode == qso.Mode &&
                         x.QsoDate >= minTime &&
                         x.QsoDate <= maxTime), CancellationToken.None);
                    if (exists)
                    {
                        invalidQsos.Add(qso);
                    }
                    else
                    {
                        if (qso.Id == Guid.Empty)
                            qso.Id = Guid.NewGuid(); // Ensure every QSO has a unique ID
                        validQsos.Add(qso);
                    }
                }

                db.Qsos.AddRange(validQsos);
                log.Message =
                    $" ADIFUpload: File '{req.File.FileName}' uploaded. Parsed {records.Count} ADIF records. " +
                    $"{validQsos.Count} valid QSOs added, {invalidQsos.Count} duplicates found.";
                db.ServerLogs.Add(log);
                var foo = await db.SaveChangesAsync(CancellationToken.None);
                _logger.LogInformation("SaveChangesAsync returned {Foo}", foo);
                
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error processing ADIF file upload: {Message} {stack}", e.Message, e.StackTrace);
                throw;
            }
        });
    }

    private string GetFileString(AdifUploadFileRequest req)
    {
        try
        {
            using var stream = req.File.OpenReadStream();
            using var reader = new StreamReader(stream);
            var adifContent = reader.ReadToEnd();

            return adifContent;
        }
        catch (Exception e)
        {
            _logger.LogInformation("GetFileString exception {0} {1}", e.Message, e.StackTrace);
            throw;
        }
    }
}
