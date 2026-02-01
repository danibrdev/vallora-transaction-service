using TransactionService.Domain.Aggregates.Transaction;

namespace TransactionService.Application.Commands.CreateTransaction;

public sealed record CreateTransactionResult(
    Guid TransactionId,
    ETransactionStatus Status,
    DateTime CreatedAt
);