using HbLibrary;

namespace HBLoggingService.Data;

public class LoggingDbContext : DbContext
{
    public LoggingDbContext(DbContextOptions<LoggingDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Qso indexes
        modelBuilder.Entity<Qso>()
            .HasIndex(q => q.Call);
        modelBuilder.Entity<Qso>()
            .HasIndex(q => q.Band);
        modelBuilder.Entity<Qso>()
            .HasIndex(q => q.Dxcc);
        modelBuilder.Entity<QsoQslInfo>()
            .HasIndex(q => q.QslService);

        // Configure LogConfig
        modelBuilder.Entity<LogConfig>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ProfileName)
                .HasMaxLength(50)
                .IsRequired();
            entity.Property(e => e.Callsign)
                .HasMaxLength(20)
                .IsRequired();
            entity.Property(e => e.StationName)
                .HasMaxLength(20);
            entity.Property(e => e.GridSquare)
                .HasMaxLength(20);
            entity.Property(e => e.City)
                .HasMaxLength(50);
            entity.Property(e => e.County)
                .HasMaxLength(50);
            entity.Property(e => e.CountyCode)
                .HasMaxLength(10);
            entity.Property(e => e.State)
                .HasMaxLength(2);
            entity.Ignore(e => e.IsDirty); // Ignore IsDirty
        });

        // Configure CallBookConf
        modelBuilder.Entity<CallBookConf>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                .HasMaxLength(100) // Adjust as needed
                .IsRequired();
            entity.Property(e => e.Host)
                .HasMaxLength(255)
                .IsRequired();
            entity.Ignore(e => e.isDirty);
            entity.HasOne(e => e.LogConfig)
                .WithMany(l => l.Logbooks)
                .HasForeignKey(e => e.LogConfigId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure DxClusterConf
        modelBuilder.Entity<DxClusterConf>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Host)
                .HasMaxLength(255)
                .IsRequired();
            entity.Property(e => e.Port)
                .HasDefaultValue(7300);
            entity.Ignore(e => e.isDirty);
            entity.HasOne(e => e.LogConfig)
                .WithMany(l => l.DxClusters)
                .HasForeignKey(e => e.LogConfigId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure RigCtlConf
        modelBuilder.Entity<RigCtlConf>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();
            entity.Property(e => e.Host)
                .HasMaxLength(255)
                .IsRequired();
            entity.Property(e => e.TunerName)
                .HasMaxLength(100);
            entity.Ignore(e => e.isDirty);
            entity.HasOne(e => e.LogConfig)
                .WithMany(l => l.RigControls)
                .HasForeignKey(e => e.LogConfigId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    public DbSet<Qso> Qsos => Set<Qso>();
    public DbSet<QsoDetail> QsoDetails { get; set; }
    public DbSet<QsoQslInfo> QsoQslInfos { get; set; }
    public DbSet<OperatorProfile> OperatorProfiles { get; set; }
    public DbSet<CallSign> CallSigns { get; set; }
    public DbSet<ServerLog> ServerLogs { get; set; }
    public DbSet<LogConfig> LogConfig { get; set; }
    public DbSet<CallBookConf> CallBookConfs { get; set; }
    public DbSet<RigCtlConf> RigCtlConfs { get; set; }
    public DbSet<DxClusterConf> DxClusterConfs { get; set; }
}
