namespace HBLoggingService.Services;

using HamBlocks.Library.Models;
using Microsoft.EntityFrameworkCore;


public class HbConfigurationService(LoggingDbContext _context)
{

    public List<LogConfig> Configuration { get; private set; } = [];

    public async Task<List<LogConfig>> GetAllAsync()
    {
        Configuration = await _context.HBConfigurations
            .Include(c => c.RigControls)
            .Include(c => c.Logbooks)
            .Include(c => c.DxClusters)
            .ToListAsync();
        return Configuration;
    }

    public async Task<LogConfig?> GetByProfileNameAsync(string profileName)
    {
        return await _context.HBConfigurations
            .Include(c => c.RigControls)
            .Include(c => c.Logbooks)
            .Include(c => c.DxClusters)
            .FirstOrDefaultAsync(c => c.ProfileName == profileName);
    }

    public async Task AddAsync(LogConfig config)
    {
        _context.HBConfigurations.Add(config);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(LogConfig config)
    {
        _context.HBConfigurations.Update(config);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string profileName)
    {
        var config = await GetByProfileNameAsync(profileName);
        if (config != null)
        {
            _context.HBConfigurations.Remove(config);
            await _context.SaveChangesAsync();
        }
    }
}
