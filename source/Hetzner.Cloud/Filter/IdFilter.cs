using Hetzner.Cloud.Interfaces;

namespace Hetzner.Cloud.Filter;

public class IdFilter(long id) : IFilter
{
    public long Id { get; } = id;

    public string GetFilterField()
    {
        return "id";
    }

    public string GetValue()
    {
        return this.Id.ToString();
    }
}
