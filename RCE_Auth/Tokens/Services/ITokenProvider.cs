using System;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using RCE_Auth.Tokens.DTOs;
using RCE_Auth.Users.Entities;

namespace RCE_Auth.Tokens.Services;

public interface ITokenProvider
{
    TokenResponseDTO GenerateToken(User user);
    Task<TokenValidationResult?> ValidateToken(string token);
    SecurityToken? ReadToken(string token);
}
