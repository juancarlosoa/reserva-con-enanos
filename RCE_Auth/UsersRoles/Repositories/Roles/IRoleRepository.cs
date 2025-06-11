using RCE_Auth.UsersRoles.Entities;

namespace RCE_Auth.UsersRoles.Repositories;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetRoles();
}