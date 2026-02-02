#region

using TransactionService.Application.Abstractions.Persistence;
using TransactionService.Domain.Aggregates.Transaction;
using TransactionService.Infrastructure.Persistence.Context;

#endregion

namespace TransactionService.Infrastructure.Persistence.Repositories;

public sealed class TransactionRepository(PostgreSqlDbContext context) : ITransactionRepository
{
    public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken)
        => await context.Transactions.AddAsync(transaction, cancellationToken);
}