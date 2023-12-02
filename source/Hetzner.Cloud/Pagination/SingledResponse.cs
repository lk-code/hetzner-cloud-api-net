using System.Text.Json;

namespace Hetzner.Cloud.Pagination;

public class SingledResponse(JsonDocument jsonDocument)
{
    public readonly JsonDocument JsonDocument = jsonDocument;
}

public class SingledResponse<T>(JsonDocument jsonDocument, T item)
    : SingledResponse(jsonDocument)
{
    public T Item = item;
}
