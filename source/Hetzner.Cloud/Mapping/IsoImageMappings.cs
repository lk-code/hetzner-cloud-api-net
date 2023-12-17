using System.Text.Json;
using Hetzner.Cloud.Helper;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Mapping;

internal static class IsoImageMappings
{
    internal static IsoImage ToIsoImage(this JsonElement json)
    {
        IsoImage data = new(json.GetProperty("id").GetInt64())
        {
            Architecture = json.GetProperty("architecture").ToNullableEnum<IsoImageArchitecture>(),
            Deprecated = json.GetProperty("deprecated").GetDateTime(),
            Deprecation = json.GetProperty("deprecation").ToIsoImageDeprecation()!,
            Description = json.GetProperty("description").GetString()!,
            Name = json.GetNullableProperty("name").GetString(),
            Type = json.GetProperty("type").ToNullableEnum<IsoImageType>(),
        };

        return data;
    }
    
    internal static IsoImageDeprecation ToIsoImageDeprecation(this JsonElement json)
    {
        IsoImageDeprecation data = new()
        {
            Announced = json.GetProperty("announced").GetDateTime(),
            UnavailableAfter = json.GetProperty("unavailable_after").GetDateTime(),
        };

        return data;
    }
}