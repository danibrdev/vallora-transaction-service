#region

using TransactionService.Domain.Exceptions;
using TransactionService.Domain.ValueObjects;

#endregion

namespace TransactionService.Domain.Aggregates.Transaction;

public class Transaction
{
    public TransactionId Id { get; }
    public PortfolioId PortfolioId { get; }
    public string Ticker { get; }
    public ETransactionType Type { get; }
    public decimal Quantity { get; }
    public decimal Price { get; }
    public ETransactionStatus Status { get; private set; }

    public DateTime CreatedAt { get; }
    public DateTime? CompletedAt { get; private set; }
    public DateTime? CancelledAt { get; private set; }

    public decimal TotalAmount => Quantity * Price;

    //fase2
    //public string CorrelationId { get; set; }
    //public string? CancellationReason { get; set; }

    private Transaction(
        TransactionId id,
        PortfolioId portfolioId,
        string ticker,
        ETransactionType type,
        decimal quantity,
        decimal price,
        DateTime createdAt)
    {
        Id = id;
        PortfolioId = portfolioId;
        Ticker = ticker;
        Type = type;
        Quantity = quantity;
        Price = price;
        CreatedAt = createdAt;
        Status = ETransactionStatus.Created;
    }

    public static Transaction Create(
        PortfolioId portfolioId,
        string ticker,
        ETransactionType type,
        decimal quantity,
        decimal price,
        DateTime createdAt)
    {
        if (string.IsNullOrWhiteSpace(ticker))
            throw new DomainException("Ticker is required.");

        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");

        if (price <= 0)
            throw new DomainException("Price must be greater than zero.");

        return new Transaction(
            TransactionId.New(),
            portfolioId,
            ticker,
            type,
            quantity,
            price,
            createdAt);
    }

    public void MarkCompleted(DateTime completedAt)
    {
        if (Status == ETransactionStatus.Cancelled)
            throw new DomainException("Cancelled transaction cannot be completed.");

        Status = ETransactionStatus.Completed;
        CompletedAt = completedAt;
    }

    public void Cancel(DateTime cancelledAt)
    {
        if (Status == ETransactionStatus.Completed)
            throw new DomainException("Completed transaction cannot be cancelled.");

        Status = ETransactionStatus.Cancelled;
        CancelledAt = cancelledAt;
    }
}