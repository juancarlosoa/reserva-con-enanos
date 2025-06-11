using RCE_Auth.UsersRoles.Entities;

namespace RCE_Auth.Login.DTOs;

public class LoginResponseDTO
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public Guid? UserId { get; set; }
    public IEnumerable<Role> UserRoles { get; set; } = [];
    public bool EmailVerified { get; set; }
}