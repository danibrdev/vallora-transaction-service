#region

using TransactionService.Application.Abstractions.Outbox;
using TransactionService.Infrastructure.Persistence.Context;

#endregion

namespace TransactionService.Infrastructure.Outbox;

public sealed class OutboxRepository(PostgreSqlDbContext context) : IOutbox
{
    public async Task AddAsync(
        string type,
        string payload,
        CancellationToken cancellationToken = default)
    {
        var message = new OutboxMessage
        {
            Id = Guid.NewGuid(),
            Type = type,
            Payload = payload,
            OccurredAt = DateTime.UtcNow
        };

        // await context.OutboxMessages.AddAsync(message, cancellationToken);        
        await context.SaveChangesAsync(cancellationToken);
    }
}