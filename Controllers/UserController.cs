using Microsoft.AspNetCore.Mvc;
using UserAccountManagement.Helpers;
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
            var result = _userService.GetUsers();
            return result.Success ? new OkObjectResult(result) : ErrorResponseHelper.CreateErrorResponse(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserRequest request)
        {
            var result = _userService.CreateUser(request);
            return result.Success ? new OkObjectResult(result) : ErrorResponseHelper.CreateErrorResponse(result);
        }
    }
}