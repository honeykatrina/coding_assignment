using UserAccountManagement.Users.Models.Entities;

namespace UserAccountManagement.Users.Repositories;

public interface IUserRepository
{
    List<User> GetAll();

    void Update(User user);

    User GetByCustomerId(int customerId);
}