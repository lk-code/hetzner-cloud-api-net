namespace Hetzner.Cloud.Sorting;

public class Sorting<T>(T field, SortingDirection direction)
{
    public T Field { get; } = field;
    public SortingDirection Direction { get; } = direction;
    
    public string AsUriParameter()
    {
        return $"{Field!.ToString()!.ToLowerInvariant()}:{Direction.ToString().ToLowerInvariant()}";
    }
}
