using Hetzner.Cloud.Filter;
using Hetzner.Cloud.Models;
using Hetzner.Cloud.Pagination;
using Hetzner.Cloud.Sorting;

namespace Hetzner.Cloud.Interfaces;

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
    Task<PagedResponse<Server>> GetAllAsync(int page = 1,
        int itemsPerPage = 25,
        List<IFilter>? filter = null,
        Sorting<ServerSorting>? sorting = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a specific Server object. The Server must exist inside the Project
    /// </summary>
    /// <param name="id">ID of the Server</param>
    /// <returns></returns>
    Task<SingledResponse<Server>> GetByIdAsync(long id,
        CancellationToken cancellationToken = default);
}
