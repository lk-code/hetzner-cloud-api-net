using lkcode.hetznercloudapi.Exceptions;
using lkcode.hetznercloudapi.Helper;
using lkcode.hetznercloudapi.Instances.Server;
using lkcode.hetznercloudapi.Interfaces;
using lkcode.hetznercloudapi.Mapping;
using lkcode.hetznercloudapi.Models.Api.Server;
using lkcode.hetznercloudapi.ParameterObjects.Pagination;
using lkcode.hetznercloudapi.ParameterObjects.Sort;

namespace lkcode.hetznercloudapi.Services;

public class ServerService : IServerService
{
    private readonly IHetznerCloudService _hetznerCloudService;

    public ServerService(IHetznerCloudService hetznerCloudService)
    {
        this._hetznerCloudService = hetznerCloudService ?? throw new ArgumentNullException(nameof(hetznerCloudService));
    }

    /// <inheritdoc/>
    public async Task<Page<Server>> GetAllAsync(int page = 1,
        int itemsPerPage = 25,
        List<IFilter>? filter = null,
        Sorting<ServerSortField>? sorting = null)
    {
        if (page <= 0)
        {
            throw new InvalidArgumentException($"invalid page number ({page}). must be greather than 0.");
        }

        string requestUri = $"/servers";

        Dictionary<string, string> arguments = new();
        arguments.Add("page", page.ToString());

        // render filter and sorting
        if (filter != null && filter.Count > 0)
        {
            filter.ForEach(x => arguments.Add(x.GetFilterField(), x.GetValue()));
        }
        if (sorting != null)
        {
            arguments.Add(sorting.Field.ToString(), sorting.Direction.ToString());
        }

        GetAllResponse? response = await this._hetznerCloudService.GetRequest<GetAllResponse>(requestUri, arguments);

        // verify response
        if (response == null)
        {
            throw new InvalidResponseException("the api response is empty or invalid.");
        }
        if (response.Meta == null
            || response.Meta.Pagination == null)
        {
            throw new InvalidResponseException("the meta property in the api response is empty or invalid.");
        }
        if (response.Servers == null)
        {
            throw new InvalidResponseException("the server property in the api response is empty or invalid.");
        }

        IEnumerable<Server> servers = response.Servers
            .Select(x => x.ToServer())
            .ToList();

        Page<Server> result = new Page<Server>(response.Meta.Pagination.Page.Ensure("the page property in the api response is empty or invalid."),
            response.Meta.Pagination.ItemsPerPage.Ensure("the items-per-page property in the api response is empty or invalid."),
            response.Meta.Pagination.TotalEntries.Ensure("the total-entries property in the api response is empty or invalid."),
            servers);

        return result;
    }

    /// <inheritdoc/>
    public async Task<Server> GetByIdAsync(long id)
    {
        string requestUri = $"/servers/{id}";

        try
        {
            GetByIdResponse? response = await this._hetznerCloudService.GetRequest<GetByIdResponse>(requestUri);
            // verify response
            if (response == null)
            {
                throw new InvalidResponseException("the api response is empty or invalid.");
            }

            if (response.Server == null)
            {
                throw new ResourceNotFoundException($"the server with the id {id} was not found");
            }

            Server server = response.Server.ToServer();

            return server;
        }
        catch (HttpRequestException err)
        {
            if (err.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ResourceNotFoundException($"the server with the id {id} was not found", err);
            }
            else
            {
                throw;
            }
        }
    }

    /// <inheritdoc/>
    public Task Create(string name,
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
        string? userData)
    {
        throw new NotImplementedException();
    }
}