#region

using System.ComponentModel.DataAnnotations;
using TransactionService.Domain.Aggregates.Transaction;

#endregion

namespace TransactionService.Api.Contracts.Requests;

public abstract record CreateTransactionRequest(
    [Required] Guid PortfolioId,
    [Required] string Ticker,
    ETransactionType Type,
    decimal Quantity,
    decimal Price
);