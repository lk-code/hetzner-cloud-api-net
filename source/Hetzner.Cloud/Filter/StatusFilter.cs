using Hetzner.Cloud.Interfaces;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Filter;

public class StatusFilter(ServerStatus status) : IFilter
{
    public ServerStatus Status { get; } = status;

    public string GetFilterField()
    {
        return "status";
    }

    public string GetValue()
    {
        return this.Status.ToString().ToLowerInvariant();
    }
}
