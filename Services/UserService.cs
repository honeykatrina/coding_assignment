using AutoMapper;
using UserAccountManagement.Models.DomainModels;
using UserAccountManagement.Models.Requests;
using UserAccountManagement.Models.ResponseModels;
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

    public List<UserResponseModel> GetUsers()
    {
        var users = _userRepository.GetAll();
        return _mapper.Map<List<UserResponseModel>>(users);
    }

    public void CreateUser(CreateUserRequest request)
    {
        var currentUser = _userRepository.GetByCustomerId(request.CustomerId);
        if (currentUser != null)
        {
            // already exists error
        }
        var newUser = _mapper.Map<User>(request);
        _userRepository.Create(newUser);

        
        if (request.InitialCredit != 0)
        {
            var transaction = _mapper.Map<Transaction>((request, newUser.Account.Id));
            _transactionRepository.Create(transaction);

        }
    }
}

