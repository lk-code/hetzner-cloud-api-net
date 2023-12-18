using Hetzner.Cloud.Interfaces;

namespace Hetzner.Cloud.Filter;

public class LabelFilter : IFilter
{
    public string Label { get; private set; }

    public LabelFilter(string label)
    {
        this.Label = label;
    }

    public string GetFilterField()
    {
        return "label_selector";
    }

    public string GetValue()
    {
        return this.Label;
    }
}
