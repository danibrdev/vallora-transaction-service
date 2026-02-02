#region

using Microsoft.Extensions.DependencyInjection;
using TransactionService.Application.Commands.CreateTransaction;
using TransactionService.Application.Queries.GetTransactionById;

#endregion

namespace TransactionService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IGetTransactionByIdHandler, GetTransactionByIdHandler>();
        services.AddScoped<ICreateTransactionHandler, CreateTransactionHandler>();

        return services;
    }
}