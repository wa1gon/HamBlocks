namespace HBAbstractions;

public interface IDxClusterConf
{
    string Host { get; set; }
    int Port { get; set; } // Default port for DX Cluster
    string? UserName { get; set; } // Optional username for authentication
    string? Password { get; set; } // Optional password for authentication
}
