namespace HBAbstractions;

public interface IHBConfiguration
{
    string ProfileName { get; set; }
    string Callsign { get; set; }
    string StationName { get; set; }
    string GridSquare { get; set; }
    string City { get; set; }
    string County { get; set; }
    string CountyCode { get; set; }
    string State { get; set; }
    int Dxcc { get; set; } // DXCC Entity Code
    int ProKey { get; set; }
    List<IRigCtlConf> RigControls { get; set; }
    List<ICallBookConf> Logbooks { get; set; }
    List<IDxClusterConf> DxClusters { get; set; }
}
