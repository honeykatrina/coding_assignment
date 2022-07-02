using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserAccountManagement.Shared.Helpers;
using UserAccountManagement.Shared.Models;
using UserAccountManagement.Users.Services;

namespace UserAccountManagement.Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _userService.GetUsers();

            return result.Success ? new OkObjectResult(result) : ErrorResponseHelper.CreateErrorResponse(result);
        }

        [HttpGet("{customerId}/accounts")]
        public IActionResult GetUserAccounts([FromRoute] int customerId)
        {
            var result = _userService.GetUserAccountsByCustomerId(customerId);

            return result.Success ? new OkObjectResult(result) : ErrorResponseHelper.CreateErrorResponse(result);
        }

        [HttpPost("{customerId}/accounts")]
        public async Task<IActionResult> CreateAccountAsync([FromRoute] int customerId, [FromBody] double initialCredit)
        {
            var result = await _userService.CreateUserAccountAsync(
                _mapper.Map<CreateAccountRequest>((customerId, initialCredit)));

            return result.Success ? new OkObjectResult(result) : ErrorResponseHelper.CreateErrorResponse(result);
        }
    }
}