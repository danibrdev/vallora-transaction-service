#region

using TransactionService.Application.Abstractions.Persistence;
using TransactionService.Infrastructure.Persistence.Context;

#endregion

namespace TransactionService.Infrastructure.Persistence;

public class UnitOfWork(PostgreSqlDbContext context) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken = default)
        => await context.SaveChangesAsync(cancellationToken);
}