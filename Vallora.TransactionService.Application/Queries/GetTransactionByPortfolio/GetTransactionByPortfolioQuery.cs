namespace TransactionService.Application.Queries.GetTransactionByPortfolio;

public sealed record GetTransactionsByPortfolioQuery(
    Guid PortfolioId,
    int Page,
    int PageSize
);