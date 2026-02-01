using TransactionService.Domain.Aggregates.Transaction;

namespace TransactionService.Application.Abstractions.Persistence;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction, CancellationToken cancellationToken);
}