using lkcode.hetznercloudapi.Instances;
using lkcode.hetznercloudapi.ParameterObjects.Sort;
using lkcode.hetznercloudapi.ParameterObjects.Filter;
using lkcode.hetznercloudapi.ParameterObjects.Pagination;

namespace lkcode.hetznercloudapi.Interfaces;

public interface IServerService
{
    /// <summary>
    /// Returns all existing Server objects
    /// </summary>
    /// <param name="page">specifies the page to fetch. The number of the first page is 1.</param>
    /// <param name="itemsPerPage">specifies the number of items returned per page. The default value is 25, the maximum value is 50 except otherwise specified in the documentation.</param>
    /// <param name="filter">filter allows only <seealso cref="NameFilter"/>, <seealso cref="LabelFilter"/> and <seealso cref="StatusFilter"/>.</param>
    /// <param name="sorting">sorts the result</param>
    /// <returns></returns>
    Task<Page<Server>> GetAllAsync(int page = 1,
        int itemsPerPage = 25,
        List<IFilter>? filter = null,
        ServerSort? sorting = null);
}
