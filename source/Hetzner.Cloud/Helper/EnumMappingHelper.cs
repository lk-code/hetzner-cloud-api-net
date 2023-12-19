using System.Text.Json;

namespace Hetzner.Cloud.Helper; 

internal static class EnumMappingHelper
{
    internal static TEnum? ToNullableEnum<TEnum>(this JsonElement json) where TEnum : struct, Enum
    {
        string? value = json.GetString();
        if (value is null)
        {
            return null;
        }
        else
        {
            TEnum enumValue;

            value = value.First().ToString().ToUpper() + value.Substring(1);
            Enum.TryParse(value, out enumValue);

            return enumValue;
        }
    }
    
    internal static TEnum ToEnum<TEnum>(this JsonElement json, TEnum defaultValue = default) where TEnum : struct, Enum
    {
        string? value = json.GetString();
        if (value is null)
        {
            return defaultValue;
        }
        else
        {
            TEnum enumValue;

            value = value.First().ToString().ToUpper() + value.Substring(1);
            Enum.TryParse(value, out enumValue);

            return enumValue;
        }
    }
}