using UserAccountManagement.Shared.Models;
using UserAccountManagement.Users.Models.Responses;

namespace UserAccountManagement.Users.Services;

public interface IUserService
{
    BaseResponse<List<UserResponseModel>> GetUsers();

    BaseResponse<List<AccountResponseModel>> GetUserAccountsByCustomerId(int customerId);

    Task<BaseResponse<AccountResponseModel>> CreateUserAccountAsync(CreateAccountRequest request);
}