#region

using TransactionService.Api.Endpoints.Transactions;
using TransactionService.Api.Middleware;
using TransactionService.Application;
using TransactionService.Infrastructure;
using TransactionService.Infrastructure.Persistence.Context;

#endregion

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.local.json", optional: true)
    .AddEnvironmentVariables();

// registrar application 
builder.Services.AddApplication();

// registrar infraestrutura 
builder.Services.AddInfrastructure(builder.Configuration, false);

// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Criar o banco, caso ainda não tenha
await PostgreSqlInitializer.InitializeAsync(app.Services);

// middleware de erro
app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

CreateTransactionEndpoint.Map(app);
GetTransactionByIdEndpoint.Map(app);

app.Run();