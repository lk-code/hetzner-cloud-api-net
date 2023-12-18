using Hetzner.Cloud.Enums;
using Hetzner.Cloud.Interfaces;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Filter;

public class StatusFilter : IFilter
{
    public ServerStatus Status { get; private set; }

    public StatusFilter(ServerStatus status)
    {
        this.Status = status;
    }

    public string GetFilterField()
    {
        return "status";
    }

    public string GetValue()
    {
        return this.Status.ToString().ToLowerInvariant();
    }
}
