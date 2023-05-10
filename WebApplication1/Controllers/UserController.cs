using AplicationCore.Entities;
using AplicationCore.interfaces.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _userService;

        public UserController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<List<Users>> GetAllUsers()
        {
            return await _userService.GetAll();
        }

        [HttpPost]
        public async Task CreateUser([FromBody] Users user)
        {
            await _userService.Register(user);
        }
    }
}
