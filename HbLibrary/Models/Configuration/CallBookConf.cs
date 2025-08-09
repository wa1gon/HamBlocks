namespace HamBlocks.Library.Models;


public class CallBookConf : ICallBookConf
{
    [Key]
    public string Name { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string? UserName { get; set; } 
    public string? Password { get; set; }
    public string? ApiKey { get; set; } // Optional API key for services that
}
