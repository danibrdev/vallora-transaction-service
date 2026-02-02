#region

using Microsoft.EntityFrameworkCore;
using TransactionService.Application.Abstractions.Persistence;
using TransactionService.Application.Queries.GetTransactionById;
using TransactionService.Domain.ValueObjects;
using TransactionService.Infrastructure.Persistence.Context;

#endregion

namespace TransactionService.Infrastructure.Persistence.Repositories;

public sealed class TransactionReadRepository(PostgreSqlDbContext context) : ITransactionReadRepository
{
    public async Task<TransactionDetailsDto?> GetByIdAsync(Guid transactionId, CancellationToken ct)
        => await context.Transactions
            .AsNoTracking()
            .Where(t => t.Id.Equals(TransactionId.From(transactionId)))
            .Select(t => new TransactionDetailsDto(
                t.Id.Value,
                t.PortfolioId.Value,
                t.Ticker,
                t.Type,
                t.Quantity,
                t.Price,
                t.Quantity * t.Price,
                t.Status,
                t.CreatedAt,
                t.CompletedAt,
                t.CancelledAt
            ))
            .FirstOrDefaultAsync(ct);
}