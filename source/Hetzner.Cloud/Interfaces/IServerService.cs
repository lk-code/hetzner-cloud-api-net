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
    /// <param name="page">Page to load.</param>
    /// <param name="itemsPerPage">Items to load per page.</param>
    /// <param name="filter">filter allows only <seealso cref="NameFilter"/>, <seealso cref="LabelFilter"/> and <seealso cref="StatusFilter"/>.</param>
    /// <param name="sorting">sorts the result</param>
    /// <returns></returns>
    Task<PagedResponse<Server>> GetAllAsync(long page = 1,
        long itemsPerPage = 25,
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
