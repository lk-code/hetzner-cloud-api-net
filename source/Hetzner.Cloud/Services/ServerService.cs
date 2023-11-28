using Hetzner.Cloud.Interfaces;
using lkcode.hetznercloudapi.Exceptions;
using lkcode.hetznercloudapi.Instances.Server;
using lkcode.hetznercloudapi.Interfaces;
using lkcode.hetznercloudapi.ParameterObjects.Pagination;
using lkcode.hetznercloudapi.ParameterObjects.Sort;

namespace Hetzner.Cloud.Services;

public class ServerService(IHttpClientFactory httpClientFactory) : IServerService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("HetznerCloudHttpClient");

    /// <inheritdoc/>
    public async Task<Page<Server>> GetAllAsync(int page = 1,
        int itemsPerPage = 25,
        List<IFilter>? filter = null,
        Sorting<ServerSortField>? sorting = null,
        CancellationToken cancellationToken = default)
    {
        if (page <= 0)
        {
            throw new InvalidArgumentException($"invalid page number ({page}). must be greather than 0.");
        }

        string requestUri = $"servers";

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
        
        var response = await this._httpClient.GetAsync(requestUri, cancellationToken);

        int i = 0;

        return null;

        // GetAllResponse? response = await this._hetznerCloudService.GetRequest<GetAllResponse>(requestUri, arguments);
        //
        // // verify response
        // if (response == null)
        // {
        //     throw new InvalidResponseException("the api response is empty or invalid.");
        // }
        //
        // if (response.Meta == null
        //     || response.Meta.Pagination == null)
        // {
        //     throw new InvalidResponseException("the meta property in the api response is empty or invalid.");
        // }
        //
        // if (response.Servers == null)
        // {
        //     throw new InvalidResponseException("the server property in the api response is empty or invalid.");
        // }
        //
        // IEnumerable<Server> servers = response.Servers
        //     .Select(x => x.ToServer())
        //     .ToList();
        //
        // Page<Server> result = new Page<Server>(
        //     response.Meta.Pagination.Page.Ensure("the page property in the api response is empty or invalid."),
        //     response.Meta.Pagination.ItemsPerPage.Ensure(
        //         "the items-per-page property in the api response is empty or invalid."),
        //     response.Meta.Pagination.TotalEntries.Ensure(
        //         "the total-entries property in the api response is empty or invalid."),
        //     servers);
        //
        // return result;
    }

    /// <inheritdoc/>
    public async Task<Server> GetByIdAsync(long id,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
        // string requestUri = $"/servers/{id}";
        //
        // try
        // {
        //     GetByIdResponse? response = await this._hetznerCloudService.GetRequest<GetByIdResponse>(requestUri);
        //     // verify response
        //     if (response == null)
        //     {
        //         throw new InvalidResponseException("the api response is empty or invalid.");
        //     }
        //
        //     if (response.Server == null)
        //     {
        //         throw new ResourceNotFoundException($"the server with the id {id} was not found");
        //     }
        //
        //     Server server = response.Server.ToServer();
        //
        //     return server;
        // }
        // catch (HttpRequestException err)
        // {
        //     if (err.StatusCode == System.Net.HttpStatusCode.NotFound)
        //     {
        //         throw new ResourceNotFoundException($"the server with the id {id} was not found", err);
        //     }
        //     else
        //     {
        //         throw;
        //     }
        // }
    }
}