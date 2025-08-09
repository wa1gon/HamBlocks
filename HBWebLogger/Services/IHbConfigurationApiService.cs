using HBAbstractions;

namespace HBWebLogger.Services;

public interface IHbConfigurationApiService
{
    Task<List<HBConfiguration>> GetAllAsync();
    Task AddAsync(IHBConfiguration config);
    Task UpdateAsync(IHBConfiguration config);
    Task DeleteAsync(string profileName);
}
