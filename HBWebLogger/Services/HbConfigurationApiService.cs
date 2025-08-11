
namespace HBWebLogger.Services;


public class HbConfigurationApiService
{
    private readonly HttpClient _http;

    public HbConfigurationApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<HBConfiguration>?> GetAllAsync()
        => await _http.GetFromJsonAsync<List<HBConfiguration>>("api/hbconfigurations");

    public async Task AddAsync(HBConfiguration config)
        => await _http.PostAsJsonAsync("api/hbconfiguration", config);

    public async Task UpdateAsync(HBConfiguration config)
        => await _http.PutAsJsonAsync($"api/hbconfiguration/{config.ProfileName}", config);

    public async Task DeleteAsync(string profileName)
        => await _http.DeleteAsync($"api/hbconfiguration/{profileName}");
}
