namespace HBLoggingService.Responses;

public class AdifUploadFileResponse
{
    public AdifUploadFileResponse(string message)
    {
        Message = message;
    }

    public string Message { get; set; }
    public int TotalRecords { get; set; } = 0;
    public int TotalRecordsWithError { get; set; } = 0;
}