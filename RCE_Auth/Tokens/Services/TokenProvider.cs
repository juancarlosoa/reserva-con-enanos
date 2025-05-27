using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using RCE_Auth.Tokens.DTOs;
using RCE_Auth.Tokens.Settings;
using RCE_Auth.Users.Entities;

namespace RCE_Auth.Tokens.Services;

public sealed class TokenProvider : ITokenProvider
{
    private readonly JwtSettings _settings;
    private readonly SymmetricSecurityKey _securityKey;

    public TokenProvider(IOptions<JwtSettings> settings)
    {
        _settings = settings.Value;
        _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
    }

    public TokenResponseDTO GenerateToken(User user)
    {
        var credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
        var expiresAt = DateTime.UtcNow.AddMinutes(_settings.ExpirationInMinutes);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.EmailVerified, user.EmailVerified.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                ]
            ),
            Expires = expiresAt,
            SigningCredentials = credentials,
            Issuer = _settings.Issuer,
            Audience = _settings.Audience,
        };

        var tokenHandler = new JsonWebTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new TokenResponseDTO
        {
            Token = token,
            ExpiresAt = expiresAt
        };
    }

    public async Task<TokenValidationResult?> ValidateToken(string token)
    {
        var handler = new JsonWebTokenHandler();
        try
        {
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _settings.Issuer,
                ValidAudience = _settings.Audience,
                IssuerSigningKey = _securityKey,
                ClockSkew = TimeSpan.Zero
            };

            return await handler.ValidateTokenAsync(token, parameters);
        }
        catch
        {
            return null;
        }
    }

    public SecurityToken? ReadToken(string token)
    {
        var handler = new JsonWebTokenHandler();
        return handler.ReadToken(token);
    }
}
