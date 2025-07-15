namespace HBLoggingService.Responses;

public class UploadFileResponse
{
    public UploadFileResponse(string message)
    {
        Message = message;
    }

    public string Message { get; set; }
}
