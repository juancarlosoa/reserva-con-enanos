using Microsoft.EntityFrameworkCore;
using RCE_Auth.Users.Entities;

namespace RCE_Auth.CoreData;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().Property(u => u.Role).HasConversion<string>().HasMaxLength(50);
    }
}
