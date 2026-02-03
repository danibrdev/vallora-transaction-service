#region

using TransactionService.Api.Endpoints.Transactions;

#endregion

namespace TransactionService.Api.Endpoints;

public static class EndpointsMappings
{
    public static void MapTransactionsEndpoints(this IEndpointRouteBuilder app)
    {
        CreateTransactionEndpoint.Map(app);
        GetTransactionByIdEndpoint.Map(app);
        GetTransactionByPortfolioEndpoint.Map(app);
    }
}