using Hetzner.Cloud.Exceptions.Http;
using Hetzner.Cloud.Helper;
using Hetzner.Cloud.Interfaces;
using Hetzner.Cloud.Mapping;
using Hetzner.Cloud.Models;
using Hetzner.Cloud.Pagination;
using Hetzner.Cloud.Sorting;
using Hetzner.Cloud.UriBuilder;

namespace Hetzner.Cloud.Services;

public class ServerActionsService(IHttpClientFactory httpClientFactory) : IServerActionsService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("HetznerCloudHttpClient");

    /// <inheritdoc/>
    public async Task<PagedResponse<ServerAction>> GetAllAsync(long page = 1,
        long itemsPerPage = 25,
        List<IFilter>? filter = null,
        Sorting<ServerActionSorting>? sorting = null,
        CancellationToken cancellationToken = default)
    {
        page.IsValidPageRequest();

        string requestUri = "/v1/servers/actions".AsUriBuilder()
            .AddPagination(page, itemsPerPage)
            .AddFilter(filter)
            .AddSorting(sorting)
            .ToUri();

        HttpResponseMessage response = await this._httpClient.GetAsync(requestUri, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            switch (response.StatusCode)
            {
                default:
                    throw new ApiException(response.StatusCode, $"Invalid Request");
            }
        }

        string content = await response.Content.ReadAsStringAsync(cancellationToken);

        PagedResponse<ServerAction> result = content
            .AsPagedResponse()
            .LoadContent("actions", (json) => json.ToServerAction());
        
        return result;
    }
}