using System.Net;

namespace UserAccountManagement.Models.Responses;

public class Error
{
    public string Message { get; set; }

    public HttpStatusCode ErrorCode { get; set; }
}