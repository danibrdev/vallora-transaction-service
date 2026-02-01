#region

using System.Net;
using System.Text.Json;
using TransactionService.Api.Contracts.Responses;
using TransactionService.Domain.Exceptions;

#endregion

namespace TransactionService.Api.Middleware;

public sealed class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (DomainException ex)
        {
            await WriteErrorAsync(
                context,
                HttpStatusCode.UnprocessableEntity,
                "DOMAIN_ERROR",
                ex.Message
            );
        }
        catch (ArgumentException ex)
        {
            await WriteErrorAsync(
                context,
                HttpStatusCode.BadRequest,
                "VALIDATION_ERROR",
                ex.Message
            );
        }
        catch (Exception)
        {
            await WriteErrorAsync(
                context,
                HttpStatusCode.InternalServerError,
                "INTERNAL_ERROR",
                "An unexpected error occurred."
            );
        }
    }

    private static async Task WriteErrorAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string code,
        string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var error = new ErrorResponse(code, message);

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(error)
        );
    }
}