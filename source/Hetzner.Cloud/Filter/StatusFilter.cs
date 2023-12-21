using Hetzner.Cloud.Interfaces;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Filter;

public class StatusFilter<T>(T status) : IFilter
{
    private T Status { get; } = status;

    public string GetFilterField()
    {
        return "status";
    }

    public string GetValue()
    {
        return this.Status!.ToString()!.ToLowerInvariant();
    }
}
