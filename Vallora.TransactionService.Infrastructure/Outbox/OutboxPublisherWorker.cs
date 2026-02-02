#region

using System.Text.Json;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TransactionService.Application.Abstractions.Messaging;
using TransactionService.Infrastructure.Persistence.Context;

#endregion

namespace TransactionService.Infrastructure.Outbox;

public sealed class OutboxPublisherWorker(IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<PostgreSqlDbContext>();
            var publisher = scope.ServiceProvider.GetRequiredService<IEventPublisher>();

            await using var transaction =
                await context.Database.BeginTransactionAsync(stoppingToken);

            var connection = context.Database.GetDbConnection();

            var messages = (await connection.QueryAsync<OutboxMessage>(
                """

                                    SELECT *
                                    FROM OutboxMessages
                                    WHERE PublishedAt IS NULL
                                    ORDER BY OccurredAt
                                    FOR UPDATE SKIP LOCKED
                                    LIMIT 10 
                                
                """,
                transaction: context.Database.CurrentTransaction!.GetDbTransaction()
            )).AsList();

            if (messages.Count == 0)
            {
                await transaction.CommitAsync(stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                continue;
            }

            foreach (var message in messages)
            {
                var eventType = Type.GetType(
                    $"Vallora.TransactionService.Application.Events.{message.Type}"
                );

                if (eventType is null)
                    continue;

                var payload = JsonSerializer.Deserialize(
                    message.Payload,
                    eventType
                );

                if (payload is null)
                    continue;

                await publisher.PublishAsync(payload, stoppingToken);

                message.PublishedAt = DateTime.UtcNow;
            }

            await context.SaveChangesAsync(stoppingToken);
            await transaction.CommitAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}