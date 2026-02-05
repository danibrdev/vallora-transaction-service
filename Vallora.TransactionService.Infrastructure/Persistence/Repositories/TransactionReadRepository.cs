#region

using Microsoft.EntityFrameworkCore;
using TransactionService.Application.Abstractions.Persistence;
using TransactionService.Application.Common.Pagination;
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

    public async Task<PagedResult<TransactionDetailsDto>> GetByPortfolioAsync(
        Guid portfolioId,
        int page,
        int pageSize,
        CancellationToken ct)
    {
        var portfolioVo = new PortfolioId(portfolioId);

        var baseQuery = context.Transactions
            .AsNoTracking()
            .Where(x => x.PortfolioId == portfolioVo);

        var totalItems = await baseQuery.CountAsync(ct);

        var items = await baseQuery
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new TransactionDetailsDto(
                x.Id.Value,
                x.PortfolioId.Value,
                x.Ticker,
                x.Type,
                x.Quantity,
                x.Price,
                x.Quantity * x.Price,
                x.Status,
                x.CreatedAt,
                x.CompletedAt,
                x.CancelledAt
            ))
            .ToListAsync(ct);

        return new PagedResult<TransactionDetailsDto>(
            items, page, pageSize, totalItems);
    }
}