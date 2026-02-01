namespace TransactionService.Api.Contracts.Responses;

public sealed record CreateTransactionResponse(
    Guid TransactionId
);