using RCE_Auth.UsersRoles.Entities;

namespace RCE_Auth.UsersRoles.Repositories;

public class RoleRepository : IRoleRepository
{
    public Task<IEnumerable<Role>> GetRoles()
    {
        throw new NotImplementedException();
    }
}