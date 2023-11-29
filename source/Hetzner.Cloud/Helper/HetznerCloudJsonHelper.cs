using System.Text.Json;
using Hetzner.Cloud.Pagination;
using lkcode.hetznercloudapi.Exceptions;

namespace Hetzner.Cloud.Helper;

public static class HetznerCloudJsonHelper
{
    public static Page CreatePagination(this string jsonContent)
    {
        JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);

        if (jsonDocument.RootElement.TryGetProperty("meta", out JsonElement metaElement))
        {
            JsonElement paginationElement = metaElement.GetProperty("pagination");
            int currentPage = paginationElement.GetProperty("page").GetInt32();
            int itemsPerPage = paginationElement.GetProperty("per_page").GetInt32();
            int totalEntries = paginationElement.GetProperty("total_entries").GetInt32();

            return new Page(jsonDocument, currentPage, itemsPerPage, totalEntries);
        }

        throw new InvalidArgumentException("Invalid JSON-Content. No 'meta'-Object found.");
    }

    public static Page<T> LoadContent<T>(this Page paginatedContent, string key, Func<JsonElement, T> converter)
    {
        if (converter is null)
        {
            throw new ArgumentNullException(nameof(converter));
        }

        if (!paginatedContent.JsonDocument.RootElement.TryGetProperty(key, out JsonElement parentElement))
        {
            throw new InvalidArgumentException($"Key '{nameof(key)}' not found in JSON-Response.");
        }

        List<T> items = parentElement.EnumerateArray()
            .Select(converter)
            .ToList();

        return new Page<T>(
            paginatedContent.JsonDocument,
            paginatedContent.CurrentPage,
            paginatedContent.ItemsPerPage,
            paginatedContent.TotalEntries,
            items
        );
    }
}