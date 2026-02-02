#region

using TransactionService.Application.Abstractions.Clock;
using TransactionService.Application.Abstractions.Persistence;
using TransactionService.Domain.Aggregates.Transaction;
using TransactionService.Domain.ValueObjects;

#endregion

namespace TransactionService.Application.Commands.CreateTransaction;

public sealed class CreateTransactionHandler(
    // IOutbox outbox,
    IUnitOfWork uow,
    ITransactionRepository repository,
    IDateTimeProvider dateTimeProvider)
    : ICreateTransactionHandler
{
    public async Task<CreateTransactionResult> HandleAsync(
        CreateTransactionCommand command,
        CancellationToken cancellationToken = default)
    {
        var transaction = Transaction.Create(
            new PortfolioId(command.PortfolioId),
            command.Ticker,
            command.Type,
            command.Quantity,
            command.Price,
            dateTimeProvider.UtcNow
        );

        await repository.AddAsync(transaction, cancellationToken);
        await uow.CommitAsync(cancellationToken);

        // var @event = new TransactionCreatedEvent(
        //     transaction.Id.Value,
        //     transaction.PortfolioId.Value,
        //     transaction.Ticker,
        //     transaction.Type,
        //     transaction.Quantity,
        //     transaction.Price,
        //     transaction.CreatedAt
        // );
        //
        // //Utilizando padrão outbox para adicionar os dados numa tabela que vai ser lida por um worker a fim de 
        // //Não perder nenhuma publicação
        // await outbox.AddAsync(
        //     nameof(TransactionCreatedEvent),
        //     JsonSerializer.Serialize(@event),
        //     cancellationToken);cancellationToken

        return new CreateTransactionResult(
            transaction.Id.Value,
            transaction.Status,
            transaction.CreatedAt
        );
    }
}