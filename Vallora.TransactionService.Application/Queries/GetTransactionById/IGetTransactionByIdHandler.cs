namespace TransactionService.Application.Queries.GetTransactionById;

public interface IGetTransactionByIdHandler
{
    Task<TransactionDetailsDto?> HandleAsync(
        GetTransactionByIdQuery query,
        CancellationToken ct
    );
}