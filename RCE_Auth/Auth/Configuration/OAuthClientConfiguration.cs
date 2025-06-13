namespace RCE_Auth.Auth.Configuration;

public class OAuthClientConfiguration
{
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string RedirectUri { get; set; } = string.Empty;
    public List<string> Permissions { get; set; } = new();
} 