using Microsoft.AspNetCore.Mvc;
using RCE_Auth.Services.UsersRoles;


namespace RCE_Auth.UsersRoles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
    }
}
