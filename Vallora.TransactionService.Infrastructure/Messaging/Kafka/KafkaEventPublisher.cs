#region

using System.Text.Json;
using Confluent.Kafka;
using Polly;
using TransactionService.Application.Abstractions.Messaging;

#endregion

namespace TransactionService.Infrastructure.Messaging.Kafka;

public sealed class KafkaEventPublisher(IProducer<string, string> producer) : IEventPublisher
{
    public async Task PublishAsync<TEvent>(
        TEvent @event,
        CancellationToken cancellationToken = default)
        where TEvent : notnull
    {
        var topic = typeof(TEvent).Name;
        var message = JsonSerializer.Serialize(@event);

        var retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(2));

        await retryPolicy.ExecuteAsync(async () =>
        {
            await producer.ProduceAsync(
                topic,
                new Message<string, string>
                {
                    Key = Guid.NewGuid().ToString(),
                    Value = message
                },
                cancellationToken
            );
        });
    }
}