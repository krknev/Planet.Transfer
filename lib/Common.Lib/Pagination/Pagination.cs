using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Common.Lib.Pagination;
public abstract record PagedRequest(int Page = 1, int PageSize = 50);

public record PagedResult<T> : PagedRequest
{
    public IEnumerable<T> Items { get; set; } = [];
    public int TotalItems { get; set; }

    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);

    // Adding a parameterless constructor to fix the deserialization issue
    [JsonConstructor]
    private PagedResult() { }

    private PagedResult(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }

    private PagedResult(IEnumerable<T> items, int totalItems)
    {
        Items = items;
        TotalItems = totalItems;
    }

    public static async Task<PagedResult<T>> CreatePagedResultAsync(IQueryable<T> items, int page, int pageSize)
    {
        return new PagedResult<T>(page, pageSize)
        {
            Items = await items.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
            TotalItems = await items.CountAsync()
        };
    }

    public PagedResult<To> Map<To>(Func<T, To> mapper)
    {
        return new PagedResult<To>([.. Items.Select(mapper)], TotalItems);
    }
}
