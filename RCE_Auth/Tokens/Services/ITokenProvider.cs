using Microsoft.IdentityModel.Tokens;
using RCE_Auth.Tokens.DTOs;
using RCE_Auth.UsersRoles.Entities;

namespace RCE_Auth.Tokens.Services;

public interface ITokenProvider
{
    TokenResponseDTO GenerateToken(User user);
    Task<TokenValidationResult?> ValidateToken(string token);
    SecurityToken? ReadToken(string token);
}
