using UserAccountManagement.Shared.Models;
using UserAccountManagement.UserModule.Models.Responses;

namespace UserAccountManagement.UserModule.Services;

public interface IUserService
{
    BaseResponse<List<UserResponseModel>> GetUsers();

    Task<BaseResponse<UserResponseModel>> CreateUserAsync(CreateUserRequest request);
}