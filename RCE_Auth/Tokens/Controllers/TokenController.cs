using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RCE_Auth.Tokens.DTOs;
using RCE_Auth.Tokens.Services;
using RCE_Auth.Users.Entities;

namespace RCE_Auth.Tokens.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenProvider _tokenProvider;

        public TokenController(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        [HttpPost]
        public Task<TokenResponseDTO> CreateUserToken([FromBody] User user)
        {
            return Task.FromResult(_tokenProvider.GenerateToken(user));
        }

        [HttpGet("/validate")]
        public IActionResult ValidateToken(string token)
        {
            try
            {
                var validation = _tokenProvider.ValidateToken(token).Result;
                if (validation is not null && !validation.IsValid)
                {
                    return BadRequest(validation.Exception.Message);
                }
                return Ok("Token Valido");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/read")]
        public Task<SecurityToken?> ReadToken(string token)
        {
            return Task.FromResult(_tokenProvider.ReadToken(token));
        }

    }
}
