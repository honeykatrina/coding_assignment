namespace UserAccountManagement.Shared.Models;

public class GeneralResponse
{
    public bool Success { get; set; }

    public Error Error { get; set; }

    public GeneralResponse(
            bool success,
            Error error)
    {
        Success = success;
        Error = error;
    }
}