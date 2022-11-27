using lkcode.hetznercloudapi.Enums;
using lkcode.hetznercloudapi.Interfaces;

namespace lkcode.hetznercloudapi.ParameterObjects.Filter;

public class StatusFilter : IFilter
{
    public StatusFilterField Status { get; private set; }

    public StatusFilter(StatusFilterField status)
    {
        this.Status = status;
    }

    public string GetFilterField()
    {
        return "status";
    }

    public string GetValue()
    {
        return this.Status.ToString();
    }
}
