using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using OpenIddict.EntityFrameworkCore.Models;
using RCE_Auth.UsersRoles.Entities;

namespace RCE_Auth.CoreData;

public class AuthDbContext : IdentityDbContext<User, Role, Guid>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
        try
        {
            var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (databaseCreator != null)
            {
                // Create the database if it doesn't exist
                if (!databaseCreator.CanConnect())
                {
                    databaseCreator.Create();
                }

                // Create tables if they don't exist
                if (!databaseCreator.HasTables())
                {
                    databaseCreator.CreateTables();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database creation failed: {ex.Message}");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>();

        modelBuilder.Entity<Role>();
    }
}
