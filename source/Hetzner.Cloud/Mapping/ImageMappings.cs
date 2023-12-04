using System.Text.Json;
using Hetzner.Cloud.Helper;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Mapping;

internal static class ImageMappings
{
    internal static Image? ToImage(this JsonElement json)
    {
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        Image data = new(json.GetProperty("id").GetInt64())
        {
            Architecture = json.GetProperty("architecture").GetString()!,
            BoundTo = json.GetNullableProperty("bound_to").GetInt64(),
            Created = json.GetProperty("created").GetDateTime(),
            CreatedFrom = json.GetProperty("created_from").ToImageCreatedFrom(),
            Deleted = json.GetNullableProperty("deleted").GetDateTime(),
            Deprecated = json.GetNullableProperty("deprecated").GetDateTime(),
            Description = json.GetProperty("description").GetString()!,
            DiskSize = json.GetProperty("disk_size").GetDouble(),
            ImageSize = json.GetNullableProperty("disk_size").GetDouble(),
            Labels = json.GetProperty("labels").ToDictionary(),
            Name = json.GetNullableProperty("name").GetString()!,
            OsFlavor = json.GetProperty("os_flavor").ToOsFlavor(),
            OsVersion = json.GetNullableProperty("os_version").GetString()!,
            Protection = json.GetProperty("protection").ToImageProtection(),
            RapidDeploy = json.GetProperty("rapid_deploy").GetBoolean()!,
            Status = json.GetProperty("status").ToImageStatus(),
            Type = json.GetProperty("type").ToImageType(),
        };

        return data;
    }
    
    internal static ImageType ToImageType(this JsonElement json)
    {
        string? value = json.GetString();
        ImageType enumValue = ImageType.System;
        if (!string.IsNullOrEmpty(value))
        {
            string parsedEnumValue = value;
            parsedEnumValue = parsedEnumValue.First().ToString().ToUpper() + parsedEnumValue.Substring(1);
            Enum.TryParse(parsedEnumValue, out enumValue);
        }

        return enumValue;
    }
    
    internal static ImageStatus ToImageStatus(this JsonElement json)
    {
        string? value = json.GetString();
        ImageStatus enumValue = ImageStatus.Unavailable;
        if (!string.IsNullOrEmpty(value))
        {
            string parsedEnumValue = value;
            parsedEnumValue = parsedEnumValue.First().ToString().ToUpper() + parsedEnumValue.Substring(1);
            Enum.TryParse(parsedEnumValue, out enumValue);
        }

        return enumValue;
    }
    
    internal static OsFlavor ToOsFlavor(this JsonElement json)
    {
        string? value = json.GetString();
        OsFlavor enumValue = OsFlavor.Unknown;
        if (!string.IsNullOrEmpty(value))
        {
            string parsedEnumValue = value;
            parsedEnumValue = parsedEnumValue.First().ToString().ToUpper() + parsedEnumValue.Substring(1);
            Enum.TryParse(parsedEnumValue, out enumValue);
        }

        return enumValue;
    }

    internal static ImageCreatedFrom? ToImageCreatedFrom(this JsonElement json)
    {
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        ImageCreatedFrom data = new(json.GetProperty("id").GetInt64())
        {
            Name = json.GetProperty("name").GetString()!,
        };

        return data;
    }
    
    internal static ImageProtection? ToImageProtection(this JsonElement json)
    {
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        ImageProtection data = new()
        {
            Delete = json.GetProperty("delete").GetBoolean()!,
        };

        return data;
    }
}