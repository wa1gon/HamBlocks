namespace HBWebLogger.Services.ApiClients;

public interface IHbConfClientApiService
{
    Task<List<LogConfig>?> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(LogConfig config);
    Task UpdateAsync(LogConfig config);
    Task DeleteAsync(string profileId);
}