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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        var user = new User { UserName = request.Email, Email = request.Email };
        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            _logger.LogInformation("User registered: {Email}", request.Email);
            return Ok(new { message = "User registered successfully" });
        }

        return BadRequest(result.Errors);
    }
}