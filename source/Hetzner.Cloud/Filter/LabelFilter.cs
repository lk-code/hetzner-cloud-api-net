using Hetzner.Cloud.Interfaces;

namespace Hetzner.Cloud.Filter;

public class LabelFilter(string label) : IFilter
{
    public string Label { get; } = label;

    public string GetFilterField()
    {
        return "label_selector";
    }

    public string GetValue()
    {
        return this.Label;
    }
}
