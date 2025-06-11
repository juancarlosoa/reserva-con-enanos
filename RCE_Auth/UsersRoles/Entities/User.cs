namespace RCE_Auth.UsersRoles.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool EmailVerified { get; set; } = false;
    public string PasswordHash { get; set; } = string.Empty;
    public ICollection<Role> Roles { get; set; } = [];
}