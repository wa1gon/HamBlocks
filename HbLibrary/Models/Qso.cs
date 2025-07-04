namespace HamBlocks.Library.Models;

public class Qso
{
    [Required] [Key] public Guid Id { get; set; }
    [Required] public string Call { get; set; } = string.Empty;
    [Required] public string MyCall { get; set; } = string.Empty;
    [Required] public DateTime QsoDate { get; set; }
    [Required] public string Mode { get; set; } = string.Empty;
    public string ContestId { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public decimal Freq { get; set; } = decimal.Zero;
    public string Band { get; set; } = string.Empty;
    public string RstSent { get; set; } = string.Empty;
    public string RstRcvd { get; set; } = string.Empty;
    
    public bool BackedUp { get; set; } = false;
    public DateTime BackupDate { get;set; }
    public DateTime LastUpdate { get;set; }

    public ICollection<QsoDetail> Details { get; set; } = new List<QsoDetail>();   
    // public Dictionary<string,QsoDetail> QsoDetails { get; set; } = new Dictionary<string,QsoDetail>();
    
    public override string ToString()
    {
        return $"{Call}: {QsoDate} - {Freq}  {Mode}";
    }
}

// usage example:
// var qso = new Qso
// {
//     Call = "WX5ZZZ",
//     QsoDateTimeUtc = DateTime.UtcNow,
//     Band = "40M",
//     Mode = "FT8",
//     RstSent = "-01",
//     RstRcvd = "-02"
// };
//
// // This will succeed
// bool added1 = qso.TryAddDetail("IOTA", "K-1234");
//
// // This will be skipped (already in Qso core fields)
// bool added2 = qso.TryAddDetail("Call", "WRONG");
//
// // This will be skipped (duplicate within details)
// bool added3 = qso.TryAddDetail("POTA", "AnotherRef");
