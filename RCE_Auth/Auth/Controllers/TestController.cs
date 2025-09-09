using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RCE_Auth.Auth.Controllers;

[ApiController]
[Route("auth/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("public")]
    public IActionResult Public()
    {
        return Ok(new { message = "Este es un endpoint público", timestamp = DateTime.UtcNow });
    }

    [HttpGet("protected")]
    [Authorize]
    public IActionResult Protected()
    {
        return Ok(new { 
            message = "Este es un endpoint protegido", 
            user = User.Identity?.Name,
            claims = User.Claims.Select(c => new { c.Type, c.Value }),
            timestamp = DateTime.UtcNow 
        });
    }

    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new { status = "healthy", service = "RCE_Auth", timestamp = DateTime.UtcNow });
    }
}