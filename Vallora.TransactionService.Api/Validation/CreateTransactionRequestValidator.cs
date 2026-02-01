#region

using TransactionService.Api.Contracts.Requests;

#endregion

namespace TransactionService.Api.Validation;

public static class CreateTransactionRequestValidator
{
    public static void Validate(CreateTransactionRequest request)
    {
        if (request.PortfolioId == Guid.Empty)
            throw new ArgumentException("PortfolioId is required");

        if (string.IsNullOrWhiteSpace(request.Ticker))
            throw new ArgumentException("Ticker is required");

        if (request.Quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");

        if (request.Price <= 0)
            throw new ArgumentException("Price must be greater than zero");
    }
}