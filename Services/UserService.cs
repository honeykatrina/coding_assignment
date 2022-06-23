using AutoMapper;
using System.Net;
using UserAccountManagement.Helpers;
using UserAccountManagement.Models.Entities;
using UserAccountManagement.Models.Requests;
using UserAccountManagement.Models.Responses;
using UserAccountManagement.Repositories;

namespace UserAccountManagement.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository userRepository,
        ITransactionRepository transactionRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public BaseResponse<List<UserResponseModel>> GetUsers()
    {
        var users = _userRepository.GetAll();
        return new BaseResponseBuilder<List<UserResponseModel>>()
            .BuildSuccessResponse(_mapper.Map<List<UserResponseModel>>(users));
    }

    public BaseResponse<UserResponseModel> CreateUser(CreateUserRequest request)
    {
        var currentUser = _userRepository.GetByCustomerId(request.CustomerId);
        if (currentUser != null)
        {
            return new BaseResponseBuilder<UserResponseModel>()
                .SetError(new()
                    { ErrorCode = HttpStatusCode.BadRequest, Message = "User account already exists." })
                .Build();
        }

        var newUser = _mapper.Map<User>(request);
        _userRepository.Create(newUser);
        var createdUser = _userRepository.GetById(newUser.Id);
        
        if (request.InitialCredit != 0)
        {
            var transaction = _mapper.Map<Transaction>((request, newUser.Account.Id));
            _transactionRepository.Create(transaction);
        }

        return new BaseResponseBuilder<UserResponseModel>()
            .BuildSuccessResponse(_mapper.Map<UserResponseModel>(createdUser));
    }
}