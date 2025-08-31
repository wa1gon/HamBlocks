namespace HBLoggingService.Services;

public class HbConfigurationService(LoggingDbContext context)
{
    internal List<LogConfig> Configuration { get; private set; } = [];

    internal async Task<List<LogConfig>> GetAllAsync()
    {
        Configuration = await context.LogConfig
            .Include(c => c.RigControls)
            .Include(c => c.Logbooks)
            .Include(c => c.DxClusters)
            .ToListAsync();
        return Configuration;
    }

    internal async Task<LogConfig?> GetByProfileNameAsync(string profileName)
    {
        return await context.LogConfig
            .Include(c => c.RigControls)
            .Include(c => c.Logbooks)
            .Include(c => c.DxClusters)
            .FirstOrDefaultAsync(c => c.ProfileName == profileName);
    }

    internal async Task AddAsync(LogConfig? config)
    {
        if (config is not null && config.Id == Guid.Empty)
        {
            if (Configuration.Any(c => c.ProfileName == config.ProfileName))
                throw new ArgumentException(
                    $"A configuration with the same ProfileName {config.ProfileName} already exists.");
            config.Id = Guid.NewGuid();
            context.LogConfig.Add(config);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException("Config is null or already has an Id");
        }
    }

    internal async Task UpdateAsync(LogConfig config)
    {
        context.LogConfig.Update(config);
        await context.SaveChangesAsync();
    }

    internal async Task DeleteAsync(Guid Id)
    {
        var entity = await context.Set<LogConfig>().FindAsync(Id);

        if (entity != null)
        {
            // Mark the entity for deletion
            context.Set<LogConfig>().Remove(entity);

            // Save changes to the database
            await context.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException("Entity not found.");
        }

        // var config = await GetByProfileNameAsync(Id);
        // if (config != null)
        // {
        //     context.HBConfigurations.Remove(config);
        //     await context.SaveChangesAsync();
        // }
    }
}