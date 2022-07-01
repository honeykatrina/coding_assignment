using UserAccountManagement.UserModule.Models.Entities;

namespace UserAccountManagement.UserModule.Repositories;

public interface IUserRepository
{
    List<User> GetAll();

    void Create(User user);

    User GetByCustomerId(int customerId);

    User GetById(Guid id);
}