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

        modelBuilder.Entity<EscapeRoomProvider>().HasKey(p => p.Id);
        modelBuilder.Entity<Room>().HasKey(r => r.Id);

        modelBuilder
            .Entity<EscapeRoomProvider>()
            .HasMany(p => p.Rooms)
            .WithOne(r => r.Provider)
            .HasForeignKey(r => r.ProviderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
