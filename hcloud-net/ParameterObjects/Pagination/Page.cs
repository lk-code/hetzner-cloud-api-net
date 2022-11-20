namespace lkcode.hetznercloudapi.ParameterObjects.Pagination;

public class Page<T>
{
    /// <summary>
    /// 
    /// </summary>
    public int CurrentPage { get; private set; } = 0;
    /// <summary>
    /// 
    /// </summary>
    public int ItemsPerPage { get; private set; } = 0;
    /// <summary>
    /// 
    /// </summary>
    public int TotalEntries { get; private set; } = 0;
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<T> Items { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="currentPage"></param>
    /// <param name="itemsPerPage"></param>
    /// <param name="totalEntries"></param>
    /// <param name="items"></param>
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
