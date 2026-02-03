#region

using TransactionService.Application.Common.Pagination;
using TransactionService.Application.Queries.GetTransactionById;

#endregion

namespace TransactionService.Application.Abstractions.Persistence;

public interface ITransactionReadRepository
{
    Task<TransactionDetailsDto?> GetByIdAsync(Guid transactionId, CancellationToken ct);

    Task<PagedResult<TransactionDetailsDto>> GetByPortfolioAsync(
        Guid portfolioId,
        int page,
        int pageSize,
        CancellationToken ct);
}