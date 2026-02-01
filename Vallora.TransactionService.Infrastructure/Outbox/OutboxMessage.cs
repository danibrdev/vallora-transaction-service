namespace TransactionService.Infrastructure.Outbox;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = default!;
    public string Payload { get; set; } = default!;
    public DateTime OccurredAt { get; set; }
    public DateTime? PublishedAt { get; set; }
}