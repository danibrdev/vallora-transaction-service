using TransactionService.Domain.Aggregates.Transaction;

namespace TransactionService.Application.DTOs;

public sealed record TransactionListItemDto(
    Guid TransactionId,
    string Ticker,
    ETransactionType Type,
    decimal Quantity,
    decimal Price,
    decimal TotalAmount,
    ETransactionStatus Status,
    DateTime CreatedAt
);