using Microsoft.AspNetCore.Identity;

namespace RCE_Auth.UsersRoles.Entities;

public class User : IdentityUser
{
    public bool EmailVerified { get; set; } = false;
}