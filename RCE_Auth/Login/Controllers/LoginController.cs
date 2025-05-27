using Microsoft.AspNetCore.Mvc;
using RCE_Auth.Login.DTOs;
using RCE_Auth.Login.Services;
using RCE_Auth.Users.Entities;

namespace RCE_Auth.Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public Task<LoginResponseDTO> AuthenticateUser([FromBody] LoginRequestDTO dto)
        {
            return _loginService.AuthenticateAsync(dto);
        }
    }
}