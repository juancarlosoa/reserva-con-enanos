using Microsoft.AspNetCore.Identity;

namespace RCE_Auth.UsersRoles.Entities;

public class Role : IdentityRole
{
    public ICollection<User> Users { get; set; } = [];
}