namespace TransactionService.Api.Contracts.Requests;

public sealed record GetTransactionsByPortfolioRequest(
    Guid PortfolioId,
    int Page = 1,
    int PageSize = 20
);