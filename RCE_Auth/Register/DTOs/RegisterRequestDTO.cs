using RCE_Auth.Users.Entities;

namespace RCE_Auth.Register.DTOs;

public class RegisterRequestDTO
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Room;
}
