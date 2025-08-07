namespace HBAbstractions;

public interface ILookupProvider
{
    Task<ICallSignInfo?> LookupCallSignAsync(string callSign);    
}
