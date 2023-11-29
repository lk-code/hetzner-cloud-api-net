using System.Text.Json;
using Hetzner.Cloud.Pagination;
using lkcode.hetznercloudapi.Exceptions;

namespace Hetzner.Cloud.Helper;

public static class HetznerCloudJsonHelper
{
    public static PagedResponse AsPagedResponse(this string jsonContent)
    {
        JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);

        if (jsonDocument.RootElement.TryGetProperty("meta", out JsonElement metaElement))
        {
            JsonElement paginationElement = metaElement.GetProperty("pagination");
            int currentPage = paginationElement.GetProperty("page").GetInt32();
            int itemsPerPage = paginationElement.GetProperty("per_page").GetInt32();
            int totalEntries = paginationElement.GetProperty("total_entries").GetInt32();

            return new PagedResponse(jsonDocument, currentPage, itemsPerPage, totalEntries);
        }

        throw new InvalidArgumentException("Invalid JSON-Content. No 'meta'-Object found.");
    }
    
    public static SingledResponse AsSingledResponse(this string jsonContent)
    {
        JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);
        
        return new SingledResponse(jsonDocument);
    }

    public static PagedResponse<T> LoadContent<T>(this PagedResponse paginatedContent, string key, Func<JsonElement, T> converter)
    {
        if (converter is null)
        {
            throw new ArgumentNullException(nameof(converter));
        }

        if (!paginatedContent.JsonDocument.RootElement.TryGetProperty(key, out JsonElement parentElement))
        {
            throw new InvalidArgumentException($"Key '{key}' not found in JSON-Response.");
        }

        List<T> items = parentElement.EnumerateArray()
            .Select(converter)
            .ToList();

        return new PagedResponse<T>(
            paginatedContent.JsonDocument,
            paginatedContent.CurrentPage,
            paginatedContent.ItemsPerPage,
            paginatedContent.TotalEntries,
            items
        );
    }

    public static SingledResponse<T> LoadContent<T>(this SingledResponse singledResponse, string key, Func<JsonElement, T> converter)
    {
        if (converter is null)
        {
            throw new ArgumentNullException(nameof(converter));
        }

        if (!singledResponse.JsonDocument.RootElement.TryGetProperty(key, out JsonElement parentElement))
        {
            throw new InvalidArgumentException($"Key '{key}' not found in JSON-Response.");
        }

        T item = converter(parentElement);

        return new SingledResponse<T>(
            singledResponse.JsonDocument,
            item
        );
    }
}