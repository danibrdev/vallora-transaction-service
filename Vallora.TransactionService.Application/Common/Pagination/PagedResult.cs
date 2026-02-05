namespace TransactionService.Application.Common.Pagination;

public sealed class PagedResult<T>(
    IReadOnlyList<T> items,
    int page,
    int pageSize,
    int totalItems)
{
    public IReadOnlyList<T> Items { get; } = items;
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
    public int TotalItems { get; } = totalItems;
    public int TotalPages { get; } = (int)Math.Ceiling(totalItems / (double)pageSize);
}