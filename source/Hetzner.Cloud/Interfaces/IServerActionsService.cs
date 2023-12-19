using Hetzner.Cloud.Filter;
using Hetzner.Cloud.Models;
using Hetzner.Cloud.Pagination;
using Hetzner.Cloud.Sorting;

namespace Hetzner.Cloud.Interfaces;

public interface IServerActionsService
{
    /// <summary>
    /// Returns all Action objects. You can sort the results by using the sort URI parameter, and filter them with the status and id parameter.
    /// </summary>
    /// <param name="page">Page to load.</param>
    /// <param name="itemsPerPage">Items to load per page.</param>
    /// <param name="filter">filter allows only <seealso cref="IdFilter"/> and <seealso cref="StatusFilter"/>.</param>
    /// <param name="sorting">sorts the result</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PagedResponse<ServerAction>> GetAllAsync(long page = 1,
        long itemsPerPage = 25,
        List<IFilter>? filter = null,
        Sorting<ServerActionSorting>? sorting = null,
        CancellationToken cancellationToken = default);
}