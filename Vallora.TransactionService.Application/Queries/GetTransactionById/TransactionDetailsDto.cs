#region

using TransactionService.Domain.Aggregates.Transaction;

#endregion

namespace TransactionService.Application.Queries.GetTransactionById;

public sealed record TransactionDetailsDto(
    Guid TransactionId,
    Guid PortfolioId,
    string Ticker,
    ETransactionType Type,
    decimal Quantity,
    decimal Price,
    decimal TotalAmount,
    ETransactionStatus Status,
    DateTime CreatedAt,
    DateTime? CompletedAt,
    DateTime? CancelledAt
);