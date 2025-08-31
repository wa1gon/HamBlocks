using System.Text.Json.Nodes;

namespace HbLibrary.FileIO;


/// <summary>
/// Work in progress to read/write JSON files
/// </summary>
public class JsonIO
{
  public JsonNode? ReadJsonFile(string path)
  {
    var json = JsonNode.Parse(File.ReadAllText("appsettings.json"));
    json["Api"]["BaseUrl"] = "https://mynewapi.com/";

    File.WriteAllText("appsettings.json",
      (string?)json.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));

    return json;
  }
}
