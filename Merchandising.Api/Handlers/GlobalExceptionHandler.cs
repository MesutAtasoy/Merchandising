using Framework.Domain.Exceptions;
using Framework.Exceptions.Abstraction;
using Microsoft.AspNetCore.Diagnostics;

namespace Merchandising.Api.Handlers;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Title = "Error",
            Detail = exception.Message,
            Status = 400
        };

        switch (exception)
        {
            case ExceptionBase exceptionBase:
                problemDetails.Status = exceptionBase.StatusCode;
                problemDetails.Detail = exceptionBase.Message;
                problemDetails.Title = "An error occured";
                break;
            case BusinessRuleValidationException businessRuleValidationException:
                problemDetails.Detail = businessRuleValidationException.Message;
                problemDetails.Title = "An error occured";
                break;
        }
        
        _logger.LogError($"{problemDetails.Title}:{problemDetails.Detail}");

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}