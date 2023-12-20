using System.Text.Json;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Mapping;

internal static class PriceMappings
{
    internal static Price[] ToPrices(this JsonElement json)
    {
        Price[] data = json.EnumerateArray()
            .Select(x => x.ToPrice())
            .ToArray();

        return data;
    }
    
    internal static Price ToPrice(this JsonElement json)
    {
        Price data = new(json.GetProperty("location").GetString()!,
            json.GetProperty("price_hourly").ToPriceValue(),
            json.GetProperty("price_monthly").ToPriceValue());

        return data;
    }
    
    internal static PriceValue ToPriceValue(this JsonElement json)
    {
        PriceValue data = new(json.GetProperty("gross").GetString()!,
            json.GetProperty("net").GetString()!);

        return data;
    }
}
