using Bbpro.Domain.Models.Response;
using Bbpro.Service.Exceptions;

namespace Bbpro.Api.Middlewares;


public class BbproMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<BbproMiddleware> _logger;

    public BbproMiddleware(RequestDelegate next, ILogger<BbproMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (BbproException ex)
        {
            await HandleException(context, ex.Code, ex.Message, ex.Global);
        }
        catch (Exception ex)
        {
            //Log
            _logger.LogError(ex.ToString());

            await HandleException(context, 500, "", true);
        }
    }

    public async Task HandleException(HttpContext context, int code, string message, bool? Global)
    {
        context.Response.StatusCode = code;
        await context.Response.WriteAsJsonAsync(
            new ResponseModel<string>
            {
                Status = false,
                Error = message,
                Data = null,
                GlobalError = Global
            }
        );
    }
}