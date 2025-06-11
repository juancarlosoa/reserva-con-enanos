using Microsoft.AspNetCore.Mvc;
using RCE_Auth.Services.UsersRoles;


namespace RCE_Auth.UsersRoles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }
    }
}
