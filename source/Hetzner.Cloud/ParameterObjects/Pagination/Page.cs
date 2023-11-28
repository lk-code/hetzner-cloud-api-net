namespace lkcode.hetznercloudapi.ParameterObjects.Pagination;

public class Page<T>
{
    public int CurrentPage { get; private set; } = 0;
    public int ItemsPerPage { get; private set; } = 0;
    public int TotalEntries { get; private set; } = 0;
    public IEnumerable<T> Items { get; private set; }

    public Page(int currentPage,
        int itemsPerPage,
        int totalEntries,
        IEnumerable<T> items)
    {
        CurrentPage = currentPage;
        ItemsPerPage = itemsPerPage;
        TotalEntries = totalEntries;
        Items = items;
    }
}
