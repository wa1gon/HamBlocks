namespace HBWebLogger.Services.ApiClients;
/// <summary>
/// The DxccEntity list will be static for many years if not decades.
/// </summary>
/// <param name="httpClient"></param>
/// <param name="logger"></param>
public class DxccInfoClientService(HttpClient httpClient, ILogger<DxccInfoClientService> logger)
{
    private readonly HttpClient _http;
    private List<DxccEntity>? _dxccList = [];
    private JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };
    //Todo: This may need a overload to a different server, but I don't see the need now.
    /// <summary>
    /// Fetch the entire DXCC entity list.
    /// Adjust the endpoint path if your API differs.
    /// </summary>
    public async Task<IReadOnlyList<DxccEntity>> GetAllAsync(CancellationToken ct = default)
    {
        if (_dxccList is not null && _dxccList.Any())
            return _dxccList;
        
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        _dxccList = await _http.GetFromJsonAsync<List<DxccEntity>>("dxcc", _jsonOptions, ct);
        return _dxccList ?? new List<DxccEntity>();
    }
   
}
