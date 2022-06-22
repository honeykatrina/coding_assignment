using UserAccountManagement.Models;
using UserAccountManagement.Repositories;

namespace UserAccountManagement.Services;

public interface IAccountService
{
    void CreateAccount(Guid customerId, double initialCredit);
}

public class AccountService:IAccountService
{
    private readonly IUserRepository _userRepository;

    public AccountService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void CreateAccount(Guid customerId, double initialCredit)
    {
        var users = _userRepository.GetUsers();
        var currentCustomer = users.SingleOrDefault(x => x.Id == customerId);
        if (currentCustomer == null)
            throw new Exception();
        Transaction? transaction = null;
        if (initialCredit != 0)
        {
            transaction = new()
            {
                Id = Guid.NewGuid(),
                Credit = initialCredit
            };
        }
        currentCustomer.Balance += initialCredit;
        currentCustomer.Accounts.Add(new()
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            Transaction = transaction
        });

        _userRepository.UpdateUser(currentCustomer);
    }
}
