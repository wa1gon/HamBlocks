using HBWebLogger.Models;
using Microsoft.AspNetCore.Components;

namespace HBWebLogger.Pages.Logs;

public partial class ArrlFieldDay : ComponentBase
{
    public ArrlFdModel ArrlFd { get; set; } = new ArrlFdModel();
    // this might need to go someplace else
    private List<string> ArrlSections = new()
    {
        "AB", "AK", "AL", "AR", "AZ", "BC", "CO", "CT", "DC", "DE",
        "DX", "EB", "EMA", "ENY", "EPA", "EWA", "GA", "GTA", "IA", "ID",
        "IL", "IN", "KS", "KY", "LA", "LAX", "MAR", "MB", "MDC", "ME",
        "MI", "MN", "MO", "MS", "MT", "NC", "NE", "NFL", "NH", "NL",
        "NM", "NNJ", "NNY", "NTX", "NV", "NY", "OH", "OK", "ONE", "ONN",
        "OR", "ORG", "PAC", "PA", "PR", "QC", "RI", "SB", "SC", "SD",
        "SDG", "SF", "SFL", "SJV", "SK", "SNJ", "STX", "SV", "TN", "TX",
        "UT", "VA", "VI", "VT", "WCF", "WI", "WMA", "WNY", "WN", "WTX",
        "WV", "WWA", "WY"
    };
}
