#region

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace TransactionService.Infrastructure.Messaging;

public static class MessagingDependencyInjection
{
    public static IServiceCollection AddMessaging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var kafkaBootstrapServers =
            configuration["Kafka:BootstrapServers"]
            ?? throw new InvalidOperationException("Kafka bootstrap servers not found.");

        if (string.IsNullOrWhiteSpace(kafkaBootstrapServers))
            return services;

        // services.AddSingleton<IProducer<string, string>>(_ =>
        //     new ProducerBuilder<string, string>(
        //         new ProducerConfig
        //         {
        //             BootstrapServers = kafkaBootstrapServers
        //         }).Build());

        //services.AddSingleton<IEventPublisher, KafkaEventPublisher>();

        return services;
    }
}