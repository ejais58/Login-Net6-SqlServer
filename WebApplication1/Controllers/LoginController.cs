using AplicationCore.Entities;
using AplicationCore.interfaces.Dto;
using AplicationCore.interfaces.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsersService _userService;

        public LoginController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto login)
        {
            string token = await _userService.login(login);

            return Ok(token);
        }
    }
}
