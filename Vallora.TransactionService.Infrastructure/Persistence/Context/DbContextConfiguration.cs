#region

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace TransactionService.Infrastructure.Persistence.Context;

internal static class DbContextConfiguration
{
    public static IServiceCollection AddTransactionDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgreSql");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException(
                "ConnectionStrings:PostgreSql não configurada.");

        services.AddDbContext<PostgreSqlDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}