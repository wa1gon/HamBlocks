namespace HBLoggingService.Requests;

using Microsoft.AspNetCore.Http;

public class AdifUploadFileRequest
{
    public IFormFile? File { get; set; }
}
