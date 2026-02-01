namespace TransactionService.Application.Abstractions.Outbox;

public interface IOutbox
{
    Task AddAsync(
        string type,
        string payload,
        CancellationToken cancellationToken = default);
}