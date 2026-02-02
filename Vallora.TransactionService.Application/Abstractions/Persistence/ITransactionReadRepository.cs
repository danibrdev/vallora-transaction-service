#region

using TransactionService.Application.Queries.GetTransactionById;

#endregion

namespace TransactionService.Application.Abstractions.Persistence;

public interface ITransactionReadRepository
{
    Task<TransactionDetailsDto?> GetByIdAsync(Guid transactionId, CancellationToken ct);
}