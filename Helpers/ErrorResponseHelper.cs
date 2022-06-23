using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserAccountManagement.Models.Responses;

namespace UserAccountManagement.Helpers;

public class ErrorResponseHelper
{
    public static IActionResult CreateErrorResponse(GeneralResponse response)
    {
        return response.Error.ErrorCode switch
        {
            HttpStatusCode.BadRequest => new BadRequestObjectResult(response),
            _ => new NotFoundObjectResult(response),
        };
    }
}
