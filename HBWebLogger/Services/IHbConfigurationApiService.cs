using HBAbstractions;

namespace HBWebLogger.Services;

public interface IHbConfigurationApiService
{
    Task<List<HBConfiguration>> GetAllAsync();
    Task AddAsync(HBConfiguration config);
    Task UpdateAsync(HBConfiguration config);
    Task DeleteAsync(string profileName);
}
