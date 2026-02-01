using TransactionService.Domain.Aggregates.Transaction;

namespace TransactionService.Application.Commands.CreateTransaction;

public sealed record CreateTransactionCommand(
    Guid PortfolioId,
    string Ticker,
    ETransactionType Type,
    decimal Quantity,
    decimal Price
);
