using HamBlocks.Library.Models;
using HBAbstractions;

namespace HBWebLogger.Services;



public class HbConfigurationApiService : IHbConfigurationApiService
{
    private readonly HttpClient _http;

    public HbConfigurationApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<HBConfiguration>?> GetAllAsync()
        => await _http.GetFromJsonAsync<List<HBConfiguration>>("api/hbconfiguration");
    
    public async Task AddAsync(IHBConfiguration config)
        => await _http.PostAsJsonAsync("api/hbconfiguration", config);

    public async Task UpdateAsync(IHBConfiguration config)
        => await _http.PutAsJsonAsync($"api/hbconfiguration/{config.ProfileName}", config);

    Task<List<HBConfiguration>> IHbConfigurationApiService.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(string profileName)
        => await _http.DeleteAsync($"api/hbconfiguration/{profileName}");
}
