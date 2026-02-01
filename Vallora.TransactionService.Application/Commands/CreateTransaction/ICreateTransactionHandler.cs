namespace TransactionService.Application.Commands.CreateTransaction;

public interface ICreateTransactionHandler
{
    Task<CreateTransactionResult> HandleAsync(
        CreateTransactionCommand command,
        CancellationToken cancellationToken = default);
}