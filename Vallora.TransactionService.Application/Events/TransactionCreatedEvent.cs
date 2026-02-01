using TransactionService.Domain.Aggregates.Transaction;

namespace TransactionService.Application.Events;

public sealed record TransactionCreatedEvent(
    Guid TransactionId,
    Guid PortfolioId,
    string Ticker,
    ETransactionType Type,
    decimal Quantity,
    decimal Price,
    DateTime CreatedAt
);