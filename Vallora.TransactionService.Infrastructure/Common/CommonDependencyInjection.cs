#region

using Microsoft.Extensions.DependencyInjection;
using TransactionService.Application.Abstractions.Clock;

#endregion

namespace TransactionService.Infrastructure.Common;

public static class CommonDependencyInjection
{
    public static IServiceCollection AddInfrastructureCommon(
        this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
        return services;
    }
}