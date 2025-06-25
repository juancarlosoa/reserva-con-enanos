using Microsoft.AspNetCore.Identity;

namespace RCE_Auth.UsersRoles.Entities;

public class Role : IdentityRole<Guid>
{
    public ICollection<User> Users { get; set; } = [];
}