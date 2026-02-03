#region

using Microsoft.Extensions.DependencyInjection;
using TransactionService.Application.Commands.CreateTransaction;
using TransactionService.Application.Queries.GetTransactionById;
using TransactionService.Application.Queries.GetTransactionByPortfolio;

#endregion

namespace TransactionService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateTransactionHandler, CreateTransactionHandler>();
        services.AddScoped<IGetTransactionByIdHandler, GetTransactionByIdHandler>();
        services.AddScoped<IGetTransactionByPortfolioHandler, GetTransactionByPortfolioHandler>();

        return services;
    }
}