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
}