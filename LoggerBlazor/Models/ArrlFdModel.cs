namespace HBWebLogger.Models;

public class ArrlFdModel
{
    public string CallSign { get; set; } = string.Empty;
    public string Class { get; set; } = string.Empty;
    public string Section { get; set; } = string.Empty;


    public override string ToString()
    {
        return $"{CallSign} - {Class} - {Section}";
    }
}