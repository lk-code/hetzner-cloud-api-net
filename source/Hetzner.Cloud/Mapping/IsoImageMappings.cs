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
            Description = json.GetProperty("description").GetString()!,
            Name = json.GetNullableProperty("name").GetString(),
            Type = json.GetProperty("type").ToNullableEnum<IsoImageType>(),
        };

        return data;
    }
}