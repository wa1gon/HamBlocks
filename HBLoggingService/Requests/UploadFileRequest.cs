namespace HBLoggingService.Requests;

using Microsoft.AspNetCore.Http;

public class UploadFileRequest
{
    public IFormFile File { get; set; }
}
