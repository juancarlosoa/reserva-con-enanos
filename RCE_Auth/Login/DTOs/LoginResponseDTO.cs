namespace RCE_Auth.Login.DTOs;

public class LoginResponseDTO
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}
