using System.Net;
using Hetzner.Cloud.Exceptions;
using Hetzner.Cloud.Exceptions.Http;
using Hetzner.Cloud.Helper;
using Hetzner.Cloud.Interfaces;
using Hetzner.Cloud.Mapping;
using Hetzner.Cloud.Models;
using Hetzner.Cloud.Pagination;
using Hetzner.Cloud.Sorting;
using Hetzner.Cloud.UriBuilder;

namespace Hetzner.Cloud.Services;

public class ServerService(IHttpClientFactory httpClientFactory) : IServerService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("HetznerCloudHttpClient");

    /// <inheritdoc/>
    public async Task<PagedResponse<Server>> GetAllAsync(long page = 1,
        long itemsPerPage = 25,
        List<IFilter>? filter = null,
        Sorting<ServerSorting>? sorting = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            page.MustBeGreatherThanZero();
        }
        catch (InvalidArgumentException err)
        {
            throw new InvalidArgumentException($"invalid page number ({page}).", err);
        }

        try
        {
            itemsPerPage.MustBeGreatherThanZero();
        }
        catch (InvalidArgumentException err)
        {
            throw new InvalidArgumentException($"invalid items per page ({itemsPerPage}).", err);
        }

        string requestUri = "/v1/servers".AsUriBuilder()
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

        PagedResponse<Server> result = content
            .AsPagedResponse()
            .LoadContent("servers", (json) => json.ToServer());
        
        return result;
    }

    /// <inheritdoc/>
    public async Task<SingledResponse<Server>> GetByIdAsync(long id,
        CancellationToken cancellationToken = default)
    {
        string requestUri = $"/v1/servers/{id}".AsUriBuilder()
            .ToUri();

        HttpResponseMessage response = await this._httpClient.GetAsync(requestUri, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    throw new ResourceNotFoundException($"the server with the id {id} was not found");
                default:
                    throw new ApiException(response.StatusCode, $"Invalid Request");
            }
        }

        string content = await response.Content.ReadAsStringAsync(cancellationToken);

        SingledResponse<Server> result = content
            .AsSingledResponse()
            .LoadContent("server", (json) => json.ToServer());

        return result;
    }
}