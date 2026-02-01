#region

using Microsoft.Extensions.DependencyInjection;
using TransactionService.Application.Commands.CreateTransaction;

#endregion

namespace TransactionService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateTransactionHandler, CreateTransactionHandler>();

        return services;
    }
}