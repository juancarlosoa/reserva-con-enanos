namespace RCE_Auth.Users.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool EmailVerified { get; set; } = false;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}

public enum UserRole
{
    Provider,
    Room,
}
