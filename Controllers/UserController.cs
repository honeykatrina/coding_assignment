using Microsoft.AspNetCore.Mvc;
using UserAccountManagement.Models.Requests;
using UserAccountManagement.Services;

namespace UserAccountManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.GetUsers());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserRequest request)
        {
            _userService.CreateUser(request);
            return Ok();
        }
    }
}