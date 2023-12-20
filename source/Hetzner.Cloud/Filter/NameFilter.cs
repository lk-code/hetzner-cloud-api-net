using Hetzner.Cloud.Interfaces;

namespace Hetzner.Cloud.Filter;

public class NameFilter(string name) : IFilter
{
    public string Name { get; } = name;

    public string GetFilterField()
    {
        return "name";
    }

    public string GetValue()
    {
        return this.Name;
    }
}
