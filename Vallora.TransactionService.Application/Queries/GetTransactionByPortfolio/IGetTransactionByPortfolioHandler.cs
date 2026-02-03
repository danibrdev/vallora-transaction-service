#region

using TransactionService.Application.Common.Pagination;
using TransactionService.Application.Queries.GetTransactionById;

#endregion

namespace TransactionService.Application.Queries.GetTransactionByPortfolio;

public interface IGetTransactionByPortfolioHandler
{
    Task<PagedResult<TransactionDetailsDto>> HandleAsync(
        GetTransactionsByPortfolioQuery query,
        CancellationToken ct);
}