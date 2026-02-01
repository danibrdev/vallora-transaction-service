#region

using TransactionService.Application.Abstractions.Clock;

#endregion

namespace TransactionService.Infrastructure.Common;

public sealed class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}