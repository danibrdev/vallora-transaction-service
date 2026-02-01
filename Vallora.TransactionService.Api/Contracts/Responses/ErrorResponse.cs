namespace TransactionService.Api.Contracts.Responses;

public sealed record ErrorResponse(
    string Code,
    string Message
);