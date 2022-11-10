using Slacker.Api.Contracts;
using System.Net;

namespace Slacker.Api.Middlewares;

public class GlobalExceptionHandler
{
    public RequestDelegate _next { get; set; }
    public ILogger<GlobalExceptionHandler> _logger { get; set; }

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);

            var error = new ErrorResponse();
            error.Errors.Add(e.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
