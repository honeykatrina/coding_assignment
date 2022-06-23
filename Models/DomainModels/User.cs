namespace UserAccountManagement.Models.DomainModels;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public Account Account { get; set; }
}

// создается юзер
// с аккаунтом
// если не 0 , то еще и первая транзакция