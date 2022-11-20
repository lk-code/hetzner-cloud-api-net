namespace lkcode.hetznercloudapi.ParameterObjects.Sort;

public class ServerSort
{
    public ServerSortField Field { get; private set; }
    public SortDirection Direction { get; private set; }

    public ServerSort(ServerSortField field, SortDirection sortDirection)
    {
        this.Field = field;
        this.Direction = sortDirection;
    }
}
