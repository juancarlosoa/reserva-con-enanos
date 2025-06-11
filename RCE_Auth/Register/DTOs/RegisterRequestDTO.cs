using RCE_Auth.UsersRoles.Entities;

namespace RCE_Auth.Register.DTOs;

public class RegisterRequestDTO
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public ICollection<Role> Roles { get; set; } = [];
}
