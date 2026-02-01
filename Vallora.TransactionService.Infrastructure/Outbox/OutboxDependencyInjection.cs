#region

using Microsoft.Extensions.DependencyInjection;
using TransactionService.Application.Abstractions.Outbox;

#endregion

namespace TransactionService.Infrastructure.Outbox;

public static class OutboxDependencyInjection
{
    public static IServiceCollection AddOutbox(
        this IServiceCollection services,
        bool enableWorker = true)
    {
        services.AddScoped<IOutbox, OutboxRepository>();

        if (enableWorker)
            services.AddHostedService<OutboxPublisherWorker>();

        return services;
    }
}