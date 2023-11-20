using System.Net;
using System.Text.Json;

namespace CQRSExamp.Api.Middlewares;

public class ExceptionHandleMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandleMiddleware> logger;

    public ExceptionHandleMiddleware(RequestDelegate next, ILogger<ExceptionHandleMiddleware> logger)
    {
        this.next = next ?? throw new ArgumentNullException(nameof(next));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleException(context, e);
        }
    }
    private Task HandleException(HttpContext context, Exception e)
    {
        context.Response.ContentType = "application/json";
        switch (e)
        {
            case Application.Exceptions.ValidationException ve:

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var message = JsonSerializer.Serialize(ve.Errors);
                logger.LogError(message);
                return context.Response.WriteAsync(message);

            default:

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return context.Response.WriteAsync(e.ToString());

        }
    }
}
