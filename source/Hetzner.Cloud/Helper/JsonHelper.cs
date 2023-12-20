using System.Text.Json;

namespace Hetzner.Cloud.Helper;

internal static class JsonHelper
{
    public static Dictionary<string, string> ToDictionary(this JsonElement jsonElement)
    {
        Dictionary<string, string> dictionary = new();

        foreach (var property in jsonElement.EnumerateObject())
        {
            dictionary[property.Name] = property.Value.ToString();
        }

        return dictionary;
    }
    
    public static long[] ToLongArray(this JsonElement? jsonElement)
    {
        if (jsonElement is null)
        {
            return Array.Empty<long>();
        }

        List<long> list = new();

        foreach (JsonElement property in jsonElement.Value.EnumerateArray())
        {
            list.Add(property.GetInt64());
        }

        return list.ToArray();
    }
    
    public static string[] ToStringArray(this JsonElement? jsonElement)
    {
        if (jsonElement is null)
        {
            return Array.Empty<string>();
        }

        List<string> list = new();

        foreach (JsonElement property in jsonElement.Value.EnumerateArray())
        {
            list.Add(property.GetString()!);
        }

        return list.ToArray();
    }
    
    public static JsonElement? GetNullableProperty(this JsonElement jsonElement, string propertyName)
    {
        if (jsonElement.TryGetProperty(propertyName, out var property))
        {
            return property;
        }

        return null;
    }
    
    public static long? GetInt64(this JsonElement? jsonElement)
    {
        if (jsonElement is not null
            && jsonElement.Value.ValueKind != JsonValueKind.Null)
        {
            return jsonElement.Value.GetInt64();
        }

        return null;
    }
    
    public static DateTime? GetDateTime(this JsonElement? jsonElement)
    {
        if (jsonElement is not null
            && jsonElement.Value.ValueKind != JsonValueKind.Null)
        {
            return jsonElement.Value.GetDateTime();
        }

        return null;
    }
    
    public static double? GetDouble(this JsonElement? jsonElement)
    {
        if (jsonElement is not null
            && jsonElement.Value.ValueKind != JsonValueKind.Null)
        {
            return jsonElement.Value.GetDouble();
        }

        return null;
    }
    
    public static string? GetString(this JsonElement? jsonElement)
    {
        if (jsonElement is not null
            && jsonElement.Value.ValueKind != JsonValueKind.Null)
        {
            return jsonElement.Value.GetString();
        }

        return null;
    }
}