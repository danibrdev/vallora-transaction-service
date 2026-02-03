#region

using TransactionService.Application.Abstractions.Persistence;
using TransactionService.Application.Common.Pagination;
using TransactionService.Application.Queries.GetTransactionById;

#endregion

namespace TransactionService.Application.Queries.GetTransactionByPortfolio;

public sealed class GetTransactionByPortfolioHandler(ITransactionReadRepository repository)
    : IGetTransactionByPortfolioHandler
{
    public async Task<PagedResult<TransactionDetailsDto>> HandleAsync(
        GetTransactionsByPortfolioQuery query,
        CancellationToken ct)
        => await repository.GetByPortfolioAsync(query.PortfolioId, query.Page, query.PageSize, ct);
}