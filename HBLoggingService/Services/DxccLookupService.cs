using HbLibrary;
using HbLibrary.FileIO;
using Microsoft.IdentityModel.Tokens;

namespace HBLoggingService.Services;
/// <summary>
/// Lookup service for DXCC information.  That can be used to look up DXCC information based on callsigns.
/// The DXCC information is loaded from a JSON file located in the Data directory of the
/// </summary>
/// <param name="logger"></param>
public class DxccLookupService
{
    public List<DxccEntity> DxccList { get; private set; } = [];
    private ILogger<DxccLookupService> _logger;

    public DxccLookupService(ILogger<DxccLookupService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
        ReadDxccInfo();
    }
    public void ReadDxccInfo()
    {
        if (DxccList.IsNullOrEmpty() == false)
        {
            return;
        }
        
        string filePath = Path.Combine(AppContext.BaseDirectory, "Data", "dxcc.json");
        if (File.Exists(filePath) == false)
        {
            _logger.LogError($"DXCC data file not found at {filePath}. Please ensure the file exists.");
            throw new FileNotFoundException($"DXCC data file not found at {filePath}");
        }
        DxccList = DxccJsonReader.LoadDxccFromJson(filePath);
        if (DxccList.IsNullOrEmpty() || DxccList.Count == 0)
        {
            _logger.LogError("DXCC data is empty or not loaded correctly.");
            throw new InvalidOperationException("DXCC data is empty or not loaded correctly.");
        }

    }
}
