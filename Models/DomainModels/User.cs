namespace UserAccountManagement.Models.DomainModels;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public Account Account { get; set; }
}