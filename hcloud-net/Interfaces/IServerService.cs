using lkcode.hetznercloudapi.Instances.Server;
using lkcode.hetznercloudapi.ParameterObjects.Filter;
using lkcode.hetznercloudapi.ParameterObjects.Pagination;
using lkcode.hetznercloudapi.ParameterObjects.Sort;
using System;

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
        Sorting<ServerSortField>? sorting = null);

    /// <summary>
    /// Returns a specific Server object. The Server must exist inside the Project
    /// </summary>
    /// <param name="id">ID of the Server</param>
    /// <returns></returns>
    Task<Server> GetByIdAsync(long id);

    /// <summary>
    /// Creates a new Server. Returns preliminary information about the Server as well as an Action that covers progress of creation.
    /// </summary>
    /// <param name="name">Name of the Server to create (must be unique per Project and a valid hostname as per RFC 1123)</param>
    /// <param name="image">ID or name of the Image the Server is created from</param>
    /// <param name="serverType">ID or name of the Server type this Server should be created with</param>
    /// <param name="datacenter">ID or name of Datacenter to create Server in (must not be used together with location)</param>
    /// <param name="location">ID or name of Location to create Server in (must not be used together with datacenter)</param>
    /// <param name="startAfterCreate">Start Server right after creation. Defaults to true.</param>
    /// <param name="labels">User-defined labels (key-value pairs)</param>
    /// <param name="automount">Auto-mount Volumes after attach</param>
    /// <param name="volumes">Volume IDs which should be attached to the Server at the creation time. Volumes must be in the same Location.</param>
    /// <param name="sshKeys">SSH key IDs (integer) or names (string) which should be injected into the Server at creation time</param>
    /// <param name="networks">Network IDs which should be attached to the Server private network interface at the creation time</param>
    /// <param name="publicNet">Public Network options</param>
    /// <param name="firewalls">Firewalls which should be applied on the Server's public network interface at creation time. Contains: ID of the Firewall</param>
    /// <param name="placementGroup">ID of the Placement Group the server should be in</param>
    /// <param name="userData">Cloud-Init user data to use during Server creation. This field is limited to 32KiB.</param>
    /// <returns></returns>
    Task Create(string name,
        string image,
        string serverType,
        string? datacenter,
        string? location,
        bool? startAfterCreate,
        object? labels,
        bool? automount,
        IEnumerable<long>? volumes,
        IEnumerable<string>? sshKeys,
        IEnumerable<long>? networks,
        object? publicNet,
        IEnumerable<long>? firewalls,
        long? placementGroup,
        string? userData);
}
