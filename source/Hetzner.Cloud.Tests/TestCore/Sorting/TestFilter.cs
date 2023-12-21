using Hetzner.Cloud.Interfaces;

namespace Hetzner.Cloud.Tests.TestCore.Sorting;

public class TestFilter(string field, string name) : IFilter
{
    private string Field { get; } = field;
    private string Name { get; } = name;

    public string GetFilterField()
    {
        return Field;
    }

    public string GetValue()
    {
        return Name;
    }
}
