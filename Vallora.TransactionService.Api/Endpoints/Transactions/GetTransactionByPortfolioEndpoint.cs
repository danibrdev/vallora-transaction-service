#region

using TransactionService.Api.Contracts.Requests;
using TransactionService.Api.Validation;
using TransactionService.Application.Queries.GetTransactionByPortfolio;

#endregion

namespace TransactionService.Api.Endpoints.Transactions;

public static class GetTransactionByPortfolioEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/transactions", async (
                [AsParameters] GetTransactionsByPortfolioRequest request,
                IGetTransactionByPortfolioHandler handler,
                CancellationToken ct) =>
            {
                GetTransactionByPortfolioRequestValidator.Validate(request);

                var query = new GetTransactionsByPortfolioQuery(
                    request.PortfolioId,
                    request.Page,
                    request.PageSize);

                var result = await handler.HandleAsync(query, ct);

                return Results.Ok(result);
            })
            .WithName("GetTransactions")
            .WithTags("Transactions");
    }
}