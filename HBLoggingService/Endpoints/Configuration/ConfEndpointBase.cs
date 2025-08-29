namespace HBLoggingService.Endpoints;

public abstract class ConfEndpointBase : Endpoint<LogConfig>
{
    protected static string BasePath = "conf";
}