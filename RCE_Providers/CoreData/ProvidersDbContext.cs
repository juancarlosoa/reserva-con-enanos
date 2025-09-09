using Microsoft.EntityFrameworkCore;
using RCE_Providers.EscapeRoomProviders.Entities;
using RCE_Providers.Rooms.Entities;

namespace RCE_Providers.CoreData;

public class ProvidersDbContext : DbContext
{
    public ProvidersDbContext(DbContextOptions<ProvidersDbContext> options)
        : base(options) { }

    public DbSet<EscapeRoomProvider> EscapeRoomProviders => Set<EscapeRoomProvider>();
    public DbSet<Room> Rooms => Set<Room>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // EscapeRoomProvider configuration
        modelBuilder.Entity<EscapeRoomProvider>(entity =>
        {
            entity.HasKey(p => p.Id);
            
            entity.HasIndex(p => p.Slug).IsUnique();
            entity.HasIndex(p => p.Name);
            entity.HasIndex(p => p.Email);
            entity.HasIndex(p => p.IsDeleted);
            
            entity.HasQueryFilter(p => !p.IsDeleted);
            
            entity.Property(p => p.Name).HasMaxLength(100).IsRequired();
            entity.Property(p => p.Email).HasMaxLength(255).IsRequired();
            entity.Property(p => p.PhoneNumber).HasMaxLength(20).IsRequired();
            entity.Property(p => p.Slug).HasMaxLength(120).IsRequired();
        });

        // Room configuration
        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(r => r.Id);
            
            entity.HasIndex(r => new { r.ProviderId, r.Slug }).IsUnique();
            entity.HasIndex(r => r.Theme);
            entity.HasIndex(r => new { r.MinPlayers, r.MaxPlayers });
            entity.HasIndex(r => r.IsDeleted);
            
            entity.HasQueryFilter(r => !r.IsDeleted);
            
            entity.Property(r => r.Name).HasMaxLength(100).IsRequired();
            entity.Property(r => r.Description).HasMaxLength(1000);
            entity.Property(r => r.Theme).HasMaxLength(50).IsRequired();
            entity.Property(r => r.Slug).HasMaxLength(120).IsRequired();
        });

        // Relationships
        modelBuilder.Entity<EscapeRoomProvider>()
            .HasMany(p => p.Rooms)
            .WithOne(r => r.Provider)
            .HasForeignKey(r => r.ProviderId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Modified)
            {
                if (entry.Entity is EscapeRoomProvider provider)
                    provider.UpdatedAt = DateTime.UtcNow;
                else if (entry.Entity is Room room)
                    room.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
