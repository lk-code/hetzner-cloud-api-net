using lkcode.hetznercloudapi.Interfaces;

namespace lkcode.hetznercloudapi.ParameterObjects.Filter;

public class LabelFilter : IFilter
{
    public string Label { get; private set; }

    public LabelFilter(string label)
    {
        this.Label = label;
    }

    public string GetFilterField()
    {
        return "label";
    }

    public string GetValue()
    {
        return this.Label;
    }
}
