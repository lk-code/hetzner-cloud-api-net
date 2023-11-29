using lkcode.hetznercloudapi.Interfaces;

namespace lkcode.hetznercloudapi.ParameterObjects.Filter;

public class NameFilter : IFilter
{
    public string Name { get; private set; }

    public NameFilter(string name)
    {
        this.Name = name;
    }

    public string GetFilterField()
    {
        return "name";
    }

    public string GetValue()
    {
        return this.Name;
    }
}
