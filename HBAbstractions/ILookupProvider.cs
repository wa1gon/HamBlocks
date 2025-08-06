namespace HBAbstractions;

public interface ILookupProvider
{
    Task<ICallSignInfo?> LookupAsync(string callSign);    
}
