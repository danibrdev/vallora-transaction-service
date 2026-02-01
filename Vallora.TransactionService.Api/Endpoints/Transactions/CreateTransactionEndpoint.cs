#region

using TransactionService.Api.Contracts.Requests;
using TransactionService.Api.Contracts.Responses;
using TransactionService.Api.Validation;
using TransactionService.Application.Commands.CreateTransaction;

#endregion

namespace TransactionService.Api.Endpoints.Transactions;

public static class CreateTransactionEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/transactions", async (
                CreateTransactionRequest request,
                ICreateTransactionHandler handler,
                CancellationToken ct
            ) =>
            {
                // 👇 validação de entrada
                CreateTransactionRequestValidator.Validate(request);

                var command = new CreateTransactionCommand(
                    request.PortfolioId,
                    request.Ticker,
                    request.Type,
                    request.Quantity,
                    request.Price
                );

                var result = await handler.HandleAsync(command, ct);

                return Results.Created(
                    $"/transactions/{result.TransactionId}",
                    new CreateTransactionResponse(result.TransactionId)
                );
            })
            // 👇 METADADOS PARA O SWAGGER
            .Produces<CreateTransactionResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status422UnprocessableEntity)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("CreateTransaction")
            .WithTags("Transactions");
    }
}