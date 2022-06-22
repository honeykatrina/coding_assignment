using Microsoft.AspNetCore.Mvc;
using UserAccountManagement.Models;
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
        public IEnumerable<User> Get()
        {
            return _userService.GetUsers();
        }

        [HttpPost]
        public IActionResult CreateAccount([FromBody]Guid customerId, double initialCredit)
        {
            return Ok();
        }
    }
}