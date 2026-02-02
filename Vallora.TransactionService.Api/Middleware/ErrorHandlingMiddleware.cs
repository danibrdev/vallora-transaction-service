#region

using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using TransactionService.Api.Contracts.Responses;
using TransactionService.Domain.Exceptions;

#endregion

namespace TransactionService.Api.Middleware;

public sealed class ErrorHandlingMiddleware(
    RequestDelegate next,
    ILogger<ErrorHandlingMiddleware> logger,
    IWebHostEnvironment environment)
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
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);

            var message = environment.IsDevelopment()
                ? ex.ToString()
                : "An unexpected error occurred.";

            await WriteErrorAsync(
                context,
                HttpStatusCode.InternalServerError,
                "INTERNAL_ERROR",
                message
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