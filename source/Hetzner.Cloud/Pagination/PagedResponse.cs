using System.Text.Json;

namespace Hetzner.Cloud.Pagination;

public class PagedResponse(JsonDocument jsonDocument, int currentPage, int itemsPerPage, int totalEntries)
{
    public readonly JsonDocument JsonDocument = jsonDocument;
    public readonly int CurrentPage = currentPage;
    public readonly int ItemsPerPage = itemsPerPage;
    public readonly int TotalEntries = totalEntries;
}

public class PagedResponse<T>(JsonDocument jsonDocument, int currentPage, int itemsPerPage, int totalEntries, IReadOnlyCollection<T> items)
    : PagedResponse(jsonDocument, currentPage, itemsPerPage, totalEntries)
{
    public IReadOnlyCollection<T> Items = items;
}
