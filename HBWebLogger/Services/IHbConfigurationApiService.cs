using HBAbstractions;

namespace HBWebLogger.Services;

public interface IHbConfigurationApiService
{
    Task<List<LogConfig>> GetAllAsync();
    Task AddAsync(LogConfig config);
    Task UpdateAsync(LogConfig config);
    Task DeleteAsync(string profileName);
}
