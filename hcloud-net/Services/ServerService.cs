using lkcode.hetznercloudapi.Exceptions;
using lkcode.hetznercloudapi.Helper;
using lkcode.hetznercloudapi.Instances;
using lkcode.hetznercloudapi.Interfaces;
using lkcode.hetznercloudapi.Mapping;
using lkcode.hetznercloudapi.Models.Api.Server;
using lkcode.hetznercloudapi.ParameterObjects.Pagination;
using lkcode.hetznercloudapi.ParameterObjects.Sort;
using System.Security.Cryptography.X509Certificates;

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
        ServerSort? sorting = null)
    {
        if (page <= 0)
        {
            throw new InvalidPageException($"invalid page number ({page}). must be greather than 0.");
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

        ServerAllResponse? response = await this._hetznerCloudService.GetRequest<ServerAllResponse>(requestUri, arguments);

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
            .Select(x => x.ToServerInstance())
            .ToList();

        Page<Server> result = new Page<Server>(response.Meta.Pagination.Page.EnsureWithException("the page property in the api response is empty or invalid."),
            response.Meta.Pagination.ItemsPerPage.EnsureWithException("the items-per-page property in the api response is empty or invalid."),
            response.Meta.Pagination.TotalEntries.EnsureWithException("the total-entries property in the api response is empty or invalid."),
            servers);

        return result;
    }

    ///// <inheritdoc/>
    //public async Task<Objects.Server.PostShutdown.Response> Shutdown(long serverId)
    //{
    //    string requestUri = $"/servers/{serverId}/actions/shutdown";
    //    Objects.Server.PostShutdown.Response? response = await this._hetznerCloudService.PostRequest<Objects.Server.PostShutdown.Response>(requestUri);

    //    if (response == null)
    //    {
    //        throw new InvalidResponseException("The api response is null");
    //    }

    //    return response;
    //}
}