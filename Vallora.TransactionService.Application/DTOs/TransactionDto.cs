using TransactionService.Domain.Aggregates.Transaction;

namespace TransactionService.Application.DTOs;

//TODO: Verificar se realmente preciso desse DTO
public sealed record TransactionDto(
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