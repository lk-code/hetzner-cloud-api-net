using System.Text.Json;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Mapping;

internal static class ErrorMappings
{
    internal static Error ToError(this JsonElement json)
    {
        Error data = new(json.GetProperty("code").GetString()!,
            json.GetProperty("message").GetString()!);

        return data;
    }
}