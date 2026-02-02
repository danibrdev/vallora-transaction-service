#region

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionService.Application.Abstractions.Persistence;
using TransactionService.Infrastructure.Persistence.Context;
using TransactionService.Infrastructure.Persistence.Repositories;

#endregion

namespace TransactionService.Infrastructure.Persistence;

public static class PersistenceDependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransactionDbContext(configuration);

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ITransactionReadRepository, TransactionReadRepository>();

        return services;
    }
}