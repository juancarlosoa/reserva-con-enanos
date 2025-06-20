using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RCE_Auth.UsersRoles.Entities;


namespace RCE_Auth.Auth.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<AuthController> _logger;

    public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<AuthController> logger)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Unauthorized("Invalid credentials");

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
            return Unauthorized("Invalid credentials");

        // Aquí puedes devolver un token, claims, etc.
        return Ok("Login successful");
    }
}