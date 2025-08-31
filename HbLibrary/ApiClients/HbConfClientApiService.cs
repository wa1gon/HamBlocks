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
    {
        try
        {
            // await _http.PostAsJsonAsync("conf", config);
            Console.WriteLine("Sending POST request to 'conf' with payload:");
            Console.WriteLine(JsonSerializer.Serialize(config));

            var response = await _http.PostAsJsonAsync("conf", config);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("POST request succeeded.");
            }
            else
            {
                Console.WriteLine($"POST request failed with status code: {response.StatusCode}");
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error response content: {errorContent}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred during the POST request:");
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task UpdateAsync(LogConfig config)
    {
        await _http.PutAsJsonAsync($"conf/{config.ProfileName}", config);
    }

    public async Task DeleteAsync(string profileId)
    {
        await _http.DeleteAsync($"conf/{profileId}");
    }
}
