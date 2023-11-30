using System.Text.Json;

namespace Hetzner.Cloud.Helper;

public static class JsonHelper
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
            return null;
        }

        List<long> list = new();

        foreach (JsonElement property in jsonElement.Value.EnumerateArray())
        {
            list.Add(property.GetInt64());
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
}