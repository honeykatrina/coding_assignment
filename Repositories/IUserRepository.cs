using UserAccountManagement.Models.Entities;

namespace UserAccountManagement.Repositories;

public interface IUserRepository
{
    List<User> GetAll();

    void Create(User user);

    User GetByCustomerId(int customerId);

    User GetById(Guid id);
}