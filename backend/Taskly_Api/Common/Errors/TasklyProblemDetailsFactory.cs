using System.Diagnostics;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace Taskly_Api.Common.Errors;

public class TasklyProblemDetailsFactory(
    IOptions<ApiBehaviorOptions> options) : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    
    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null, 
        string? detail = null, 
        string? instance = null)
    {
        statusCode ??= 500;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance
        };
        
        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        statusCode ??= 400; 

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Title = title ?? "One or more validation errors occurred.",
            Type = type ?? "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Detail = detail,
            Instance = instance
        };

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    private void ApplyProblemDetailsDefaults(
        HttpContext httpContext,
        ProblemDetails problemDetails,
        int statusCode)
    {
        problemDetails.Status ??= statusCode;

        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;

        if (traceId != null)
            problemDetails.Extensions["traceId"] = traceId;
        
        var errors = httpContext?.Items["errors"] as List<Error>;
        
        if (errors != null)
            problemDetails.Extensions.Add("errorCodes", "customValue");
    }
}