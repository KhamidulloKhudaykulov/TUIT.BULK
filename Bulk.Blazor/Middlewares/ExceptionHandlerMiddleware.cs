using Bulk.Service.Exceptions;
using Bulk.Service.Extensions;

namespace Bulk.Blazor.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (AlreadyExistException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(new Response()
            {
                Message = ex.Message,
                StatusCode = ex.StatusCode
            });

        }

        catch (ArgumentIsNotValidException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(new Response()
            {
                Message = ex.Message,
                StatusCode = ex.StatusCode
            });
        }

        catch (NotFoundException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(new Response()
            {
                Message = ex.Message,
                StatusCode = ex.StatusCode
            });
        }

        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new Response()
            {
                Message = ex.Message,
                StatusCode = 500
            });
        }
    }
}
