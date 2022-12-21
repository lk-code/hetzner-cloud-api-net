using lkcode.hetznercloudapi.Instances.ServerActions;
using lkcode.hetznercloudapi.ParameterObjects.Pagination;
using lkcode.hetznercloudapi.ParameterObjects.Sort;

namespace lkcode.hetznercloudapi.Interfaces;

public interface IServerActionsService
{
    /// <summary>
    /// Returns all Action objects for a Server. You can sort the results by using the sort URI parameter, and filter them with the status parameter.
    /// </summary>
    /// <param name="id">ID of the Resource</param>
    /// <param name="filter">Can be used multiple times, the response will contain only Actions with specified statuses</param>
    /// <param name="sorting">Can be used multiple times.</param>
    /// <returns></returns>
    Task<Page<ServerAction>> GetAllActions(long id,
        List<IFilter>? filter = null,
        Sorting<ServerActionSortField>? sorting = null);

    /// <summary>
    /// Shuts down a Server gracefully by sending an ACPI shutdown request. The Server operating system must support ACPI and react to the request, otherwise the Server will not shut down.
    /// </summary>
    /// <param name="id">ID of the Server</param>
    /// <returns></returns>
    Task<ServerAction> Shutdown(long id);
}
