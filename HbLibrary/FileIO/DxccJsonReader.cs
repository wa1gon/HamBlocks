

namespace HbLibrary.FileIO;

public class DxccJsonReader
{
    public static List<DxccEntity> LoadDxccFromJson(string filePath = null)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = Path.Combine("HbLibrary", "data", "dxcc.json");
        }
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file {filePath} was not found.");
        }

        var jsonString = File.ReadAllText(filePath);
        var options = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var dxRoot = System.Text.Json.JsonSerializer.Deserialize<DxccRoot>(jsonString, options);

        return dxRoot.Dxcc ?? new List<DxccEntity>();
    }
}
