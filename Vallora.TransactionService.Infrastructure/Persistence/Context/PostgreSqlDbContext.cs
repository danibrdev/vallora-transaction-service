#region

using Microsoft.EntityFrameworkCore;
using TransactionService.Domain.Aggregates.Transaction;

#endregion

namespace TransactionService.Infrastructure.Persistence.Context;

public sealed class PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options) : DbContext(options)
{
    public DbSet<Transaction> Transactions => Set<Transaction>();
    // public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgreSqlDbContext).Assembly);
    }
}