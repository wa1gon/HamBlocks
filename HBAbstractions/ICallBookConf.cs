namespace HBAbstractions;

public interface ICallBookConf
{
    string Name { get; set; }
    string Host { get; set; }
    int Port { get; set; }
    string? UserName { get; set; }
    string? Password { get; set; }
    string? ApiKey { get; set; } // Optional API key for services that
}
