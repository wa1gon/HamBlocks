
namespace HBWebLogger.Services.ApiClients;


public class HbConfClientApiService
{
    private readonly HttpClient _http;

    public HbConfClientApiService(IHttpClientFactory http)
    {
        _http = http.CreateClient("SharedClient");
    }

    public async Task<List<LogConfig>?> GetAllAsync(CancellationToken ct = default)
    {

        Console.WriteLine("GetAllAsync called");
         return await _http.GetFromJsonAsync<List<LogConfig>>("conf");
    }

    public async Task AddAsync(LogConfig config)
        => await _http.PostAsJsonAsync("hbconfiguration", config);

    public async Task UpdateAsync(LogConfig config)
        => await _http.PutAsJsonAsync($"hbconfiguration/{config.ProfileName}", config);

    public async Task DeleteAsync(string profileId)
        => await _http.DeleteAsync($"hbconfiguration/{profileId}");
}
