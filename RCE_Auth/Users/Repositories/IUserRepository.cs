using RCE_Auth.Users.Entities;

namespace RCE_Auth.Users.Repositories;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    Task<User> AddUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(Guid userId);
    Task<User?> GetUserByIdAsync(Guid id);
}
