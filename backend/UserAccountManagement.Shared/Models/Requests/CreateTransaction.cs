namespace UserAccountManagement.Shared.Models;

public class CreateTransaction: CustomMessage
{
    public Guid AccountId { get; set; }

    public double Amount { get; set; }
}
