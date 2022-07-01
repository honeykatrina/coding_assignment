using AutoMapper;
using System.Net;
using System.Text.Json;
using UserAccountManagement.Shared.Helpers;
using UserAccountManagement.Shared.Models;
using UserAccountManagement.Shared.ServiceBusServices;
using UserAccountManagement.Users.Models.Entities;
using UserAccountManagement.Users.Models.Responses;
using UserAccountManagement.Users.Repositories;

namespace UserAccountManagement.Users.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IMessageSender _messageSender;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper,
        IMessageSender messageSender)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _messageSender = messageSender;
    }

    public BaseResponse<List<UserResponseModel>> GetUsers()
    {
        var users = _userRepository.GetAll();
        return new BaseResponseBuilder<List<UserResponseModel>>()
            .BuildSuccessResponse(_mapper.Map<List<UserResponseModel>>(users));
    }

    public BaseResponse<List<AccountResponseModel>> GetUserAccountsByCustomerId(int customerId)
    {
        var user = _userRepository.GetByCustomerId(customerId);
        if (user == null)
        {
            return new BaseResponseBuilder<List<AccountResponseModel>>()
                .SetError(new()
                { ErrorCode = HttpStatusCode.NotFound, Message = "Customer doesn't exist." })
                .Build();
        }

        return new BaseResponseBuilder<List<AccountResponseModel>>()
            .BuildSuccessResponse(_mapper.Map<List<AccountResponseModel>>(user.Accounts));
    }

    public async Task<BaseResponse<AccountResponseModel>> CreateUserAccountAsync(CreateAccountRequest request)
    {
        var user = _userRepository.GetByCustomerId(request.CustomerId);
        if (user == null)
        {
            return new BaseResponseBuilder<AccountResponseModel>()
                .SetError(new()
                    { ErrorCode = HttpStatusCode.NotFound, Message = "Customer doesn't exist." })
                .Build();
        }

        var newAccount = _mapper.Map<Account>(request);
        user.Accounts.Add(newAccount);
        _userRepository.Update(user);
        
        if (request.InitialCredit != 0)
        {
            var transaction = JsonSerializer.Serialize(
                _mapper.Map<CreateTransaction>((request, newAccount.Id)));
            await _messageSender
                       .SendMessageAsync(transaction);
        }

        return new BaseResponseBuilder<AccountResponseModel>()
            .BuildSuccessResponse(_mapper.Map<AccountResponseModel>(newAccount));
    }
}