using RCE_Auth.CoreData;
using RCE_Auth.Users.Entities;

namespace RCE_Auth.Users.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthDbContext _context;

    public UserRepository(AuthDbContext context)
    {
        _context = context;
    }

    public async Task<User> AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await SaveChangesAsync();

        return user;
    }

    public async Task<bool> DeleteUserAsync(Guid userId)
    {
        var user = await GetUserByIdAsync(userId);

        if (user is null)
            return false;

        _context.Users.Remove(user);
        await SaveChangesAsync();

        return true;
    }

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public User? GetUserByEmail(string email)
    {
        return _context.Users.Where(u => u.Email == email).FirstOrDefault();
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await SaveChangesAsync();

        return user;
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
