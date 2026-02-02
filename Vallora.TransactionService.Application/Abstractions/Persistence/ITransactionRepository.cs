#region

using TransactionService.Domain.Aggregates.Transaction;

#endregion

namespace TransactionService.Application.Abstractions.Persistence;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction, CancellationToken cancellationToken);
}