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
        private readonly IAccountService _accountService;
        public UserController(
            IUserService userService,
            IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userService.GetUsers();
        }

        [HttpPost]
        public IActionResult CreateAccount([FromBody] CreateAccountRequest request)
        {
            _accountService.CreateAccount(request.CustomerId, request.InitialCredit);
            return Ok();
        }
    }

    public class CreateAccountRequest 
    {
        public Guid CustomerId { get; set; }
        public double InitialCredit { get; set; }
    }
}