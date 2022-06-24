using System.Net;

namespace UserAccountManagement.Shared.Models;

public class Error
{
    public string Message { get; set; }

    public HttpStatusCode ErrorCode { get; set; }
}