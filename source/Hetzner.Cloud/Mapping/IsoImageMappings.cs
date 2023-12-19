using System.Text.Json;
using Hetzner.Cloud.Helper;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Mapping;

internal static class IsoImageMappings
{
    internal static IsoImage? ToIsoImage(this JsonElement jsonElement)
    {
        if (jsonElement.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        IsoImage data = new(jsonElement.GetProperty("id").GetInt64())
        {
            Architecture = jsonElement.GetProperty("architecture").ToNullableEnum<IsoImageArchitecture>(),
            Deprecated = jsonElement.GetProperty("deprecated").GetDateTime(),
            Deprecation = jsonElement.GetProperty("deprecation").ToIsoImageDeprecation()!,
            Description = jsonElement.GetProperty("description").GetString()!,
            Name = jsonElement.GetNullableProperty("name").GetString(),
            Type = jsonElement.GetProperty("type").ToNullableEnum<IsoImageType>(),
        };

        return data;
    }
    
    internal static IsoImageDeprecation ToIsoImageDeprecation(this JsonElement jsonElement)
    {
        IsoImageDeprecation data = new()
        {
            Announced = jsonElement.GetProperty("announced").GetDateTime(),
            UnavailableAfter = jsonElement.GetProperty("unavailable_after").GetDateTime(),
        };

        return data;
    }
}