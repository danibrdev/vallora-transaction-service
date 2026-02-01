#region

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

#endregion

namespace TransactionService.Infrastructure.Persistence.Context;

public static class PostgreSqlInitializer
{
    public static async Task InitializeAsync(
        IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider
            .GetRequiredService<TransactionDbContext>();

        var environment = scope.ServiceProvider
            .GetRequiredService<IHostEnvironment>();

        if (!environment.IsDevelopment())
            return;

        await context.Database.EnsureCreatedAsync();
    }
}