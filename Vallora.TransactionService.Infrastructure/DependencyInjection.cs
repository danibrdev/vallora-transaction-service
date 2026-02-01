#region

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionService.Infrastructure.Common;
using TransactionService.Infrastructure.Outbox;
using TransactionService.Infrastructure.Persistence;

#endregion

namespace TransactionService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        bool enableWorker = true)
    {
        services
            .AddPersistence(configuration)
            //.AddMessaging(configuration)
            .AddOutbox(enableWorker)
            .AddInfrastructureCommon();

        return services;
    }
}