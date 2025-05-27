using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TasklySender_Domain.Common;

namespace TasklySender.Controllers;

[ApiController]
public abstract class ApiController(IMapper mapper) : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items["errors"] = errors.ToArray();

        var firstError = errors.FirstOrDefault();

        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError,
        };

        var problemDetails = new SenderError
        {
            Status = statusCode,
            Title = "Error",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Detail = "Multiple errors occurred",
        };

        problemDetails.Errors = mapper.Map<CustomError[]>(errors.ToArray());

        return new ObjectResult(problemDetails)
        {
            StatusCode = statusCode,
        };
    }
}
