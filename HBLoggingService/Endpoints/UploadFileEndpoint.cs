using HBLoggingService.Requests;
using HBLoggingService.Responses;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using HbLibrary.Adif; // Adjust namespace as needed

public class UploadFileEndpoint : Endpoint<UploadFileRequest, UploadFileResponse>
{
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

        await SendAsync(new UploadFileResponse
        {
            Message = $"File '{req.File.FileName}' uploaded. Parsed {records.Count} ADIF records."
        }, cancellation: ct);
    }
}
