#region

using TransactionService.Application.Abstractions.Persistence;

#endregion

namespace TransactionService.Application.Queries.GetTransactionById;

public sealed class GetTransactionByIdHandler(ITransactionReadRepository repository) : IGetTransactionByIdHandler
{
    public async Task<TransactionDetailsDto?> HandleAsync(
        GetTransactionByIdQuery query,
        CancellationToken ct)
    {
        return await repository.GetByIdAsync(query.TransactionId, ct);
    }
}