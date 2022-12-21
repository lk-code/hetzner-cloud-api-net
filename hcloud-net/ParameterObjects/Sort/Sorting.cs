namespace lkcode.hetznercloudapi.ParameterObjects.Sort;

public class Sorting<T>
{
    public T Field { get; }
    public SortingDirection Direction { get; }

    public Sorting(T field,
        SortingDirection sortDirection)
    {
        this.Field = field;
        this.Direction = sortDirection;
    }
}
