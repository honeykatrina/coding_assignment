namespace UserAccountManagement.Shared.Models;

public class CreateAccountRequest
{
    public int CustomerId { get; set; }

    public double InitialCredit { get; set; }
}
