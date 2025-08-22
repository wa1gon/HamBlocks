
namespace HBWebLogger.Services.ApiClients;


public class HbConfigurationApiService
{
    private readonly HttpClient _http;

    public HbConfigurationApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<LogConfig>?> GetAllAsync()
        => await _http.GetFromJsonAsync<List<LogConfig>>("api/conf");

    public async Task AddAsync(LogConfig config)
        => await _http.PostAsJsonAsync("api/hbconfiguration", config);

    public async Task UpdateAsync(LogConfig config)
        => await _http.PutAsJsonAsync($"api/hbconfiguration/{config.ProfileName}", config);

    public async Task DeleteAsync(string profileName)
        => await _http.DeleteAsync($"api/hbconfiguration/{profileName}");
}
