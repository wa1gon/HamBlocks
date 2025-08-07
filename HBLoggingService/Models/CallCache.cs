namespace HBLoggingService.Models;

public class CallCache
{
   public required string Call { get; set; }  
   public string Grid { get; set; } = String.Empty;
   public DateTime AddedToCache { get; set; }
   public int Itu { get; set; } = 0;
   public string Continent { get; set; } = String.Empty;
   public string State { get; set; } = String.Empty;
   public string Country { get; set; } = String.Empty;
   public int Dxcc { get; set; } = 0;
}
