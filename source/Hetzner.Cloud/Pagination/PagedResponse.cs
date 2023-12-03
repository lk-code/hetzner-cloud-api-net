using System.Text.Json;

namespace Hetzner.Cloud.Pagination;

public class PagedResponse(JsonDocument jsonDocument, long currentPage, long itemsPerPage, long totalItems)
{
    public readonly JsonDocument JsonDocument = jsonDocument;
    public readonly long CurrentPage = currentPage;
    public readonly long ItemsPerPage = itemsPerPage;
    public readonly long TotalItems = totalItems;
}

/// <summary>
/// 
/// </summary>
/// <param name="jsonDocument"></param>
/// <param name="currentPage"></param>
/// <param name="itemsPerPage"></param>
/// <param name="totalEntries"></param>
/// <param name="items"></param>
/// <typeparam name="T"></typeparam>
public class PagedResponse<T>(JsonDocument jsonDocument, long currentPage, long itemsPerPage, long totalEntries, IReadOnlyCollection<T> items)
    : PagedResponse(jsonDocument, currentPage, itemsPerPage, totalEntries)
{
    public IReadOnlyCollection<T> Items = items;
}
