namespace TransactionService.Domain.Aggregates.Transaction;

public enum ETransactionStatus
{
    Created = 1,
    Pending = 2,
    Completed = 3,
    Cancelled = 4
}