namespace RCE_Auth.Tokens.DTOs
{
    public class TokenResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
