namespace UserAccountManagement.UserModule.Models.Responses;

public class UserResponseModel
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public double Balance { get; set; }

    public Guid AccountId { get; set; }

    public int CustomerId { get; set; }
}
