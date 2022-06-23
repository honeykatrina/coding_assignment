namespace UserAccountManagement.UserModule.Models.Requests;

public class CreateUserRequest
{
    public int CustomerId { get; set; }
    public double InitialCredit { get; set; }
}
