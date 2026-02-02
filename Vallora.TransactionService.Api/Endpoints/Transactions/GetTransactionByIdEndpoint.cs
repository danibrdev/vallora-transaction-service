#region

using Microsoft.AspNetCore.Mvc;
using TransactionService.Application.Queries.GetTransactionById;

#endregion

namespace TransactionService.Api.Endpoints.Transactions;

public static class GetTransactionByIdEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/transactions/{id:guid}", async (
                [FromRoute] Guid id,
                IGetTransactionByIdHandler handler,
                CancellationToken ct) =>
            {
                var result = await handler.HandleAsync(new GetTransactionByIdQuery(id), ct);

                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            })
            .Produces<TransactionDetailsDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetTransactionById")
            .WithTags("Transactions");
    }
}