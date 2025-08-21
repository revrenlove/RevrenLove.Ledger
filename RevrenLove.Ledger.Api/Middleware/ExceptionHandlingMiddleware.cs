using System.Net;
using System.Text.Json;

namespace RevrenLove.Ledger.Api.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (KeyNotFoundException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.Response.ContentType = "application/json";

            var result = JsonSerializer.Serialize(new { error = ex.Message });

            await context.Response.WriteAsync(result);
        }
    }
}
