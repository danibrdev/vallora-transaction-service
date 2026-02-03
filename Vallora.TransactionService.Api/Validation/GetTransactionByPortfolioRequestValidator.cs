#region

using TransactionService.Api.Contracts.Requests;

#endregion

namespace TransactionService.Api.Validation;

public static class GetTransactionByPortfolioRequestValidator
{
    public static void Validate(GetTransactionsByPortfolioRequest request)
    {
        if (request.PortfolioId.Equals(Guid.Empty))
            throw new ArgumentException("Invalid portfolioId");

        if (request.Page <= 0)
            throw new ArgumentException("Invalid page");

        if (request.PageSize <= 0)
            throw new ArgumentException("Invalid pageSize");
    }
}