using lkcode.hetznercloudapi.Exceptions;
using lkcode.hetznercloudapi.Helper;
using lkcode.hetznercloudapi.Instances.ServerActions;
using lkcode.hetznercloudapi.Interfaces;
using lkcode.hetznercloudapi.Mapping;
using lkcode.hetznercloudapi.Models.Api.ServerActions;
using lkcode.hetznercloudapi.ParameterObjects.Pagination;
using lkcode.hetznercloudapi.ParameterObjects.Sort;
using System;
using Hetzner.Cloud.Interfaces;

namespace lkcode.hetznercloudapi.Services;

public class ServerActionsService : IServerActionsService
{
    private readonly IHetznerCloudService _hetznerCloudService;

    public ServerActionsService(IHetznerCloudService hetznerCloudService)
    {
        this._hetznerCloudService = hetznerCloudService ?? throw new ArgumentNullException(nameof(hetznerCloudService));
    }

    /// <inheritdoc/>
    public async Task<Page<ServerAction>> GetAllActions(long id,
        List<IFilter>? filter = null,
        Sorting<ServerActionSortField>? sorting = null)
    {
        string requestUri = $"/servers/{id}/actions";

        Dictionary<string, string> arguments = new();

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
        if (response.Actions == null)
        {
            throw new InvalidResponseException("the actions property in the api response is empty or invalid.");
        }

        IEnumerable<ServerAction> actions = response.Actions
            .Select(x => x.ToActionResult())
            .ToList();

        Page<ServerAction> result = new Page<ServerAction>(response.Meta.Pagination.Page.Ensure("the page property in the api response is empty or invalid."),
            response.Meta.Pagination.ItemsPerPage.Ensure("the items-per-page property in the api response is empty or invalid."),
            response.Meta.Pagination.TotalEntries.Ensure("the total-entries property in the api response is empty or invalid."),
            actions);

        return result;
    }

    /// <inheritdoc/>
    public async Task<Instances.ServerActions.ServerAction> Shutdown(long id)
    {
        string requestUri = $"/servers/{id}/actions/shutdown";

        try
        {
            ShutdownResponse? response = await this._hetznerCloudService.GetRequest<ShutdownResponse>(requestUri);
            // verify response
            if (response == null)
            {
                throw new InvalidResponseException("the api response is empty or invalid.");
            }
            if (response.Action == null)
            {
                throw new InvalidArgumentException("the action property can't be null (invalid api response)");
            }

            Instances.ServerActions.ServerAction result = response.Action.ToActionResult();

            return result;
        }
        catch (HttpRequestException err)
        {
            if (err.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ResourceNotFoundException($"the resource with the id {id} was not found", err);
            }
            if (err.StatusCode == System.Net.HttpStatusCode.MethodNotAllowed)
            {
                throw new ActionNotAllowedException($"the requested action is not allowed", err);
            }
            else
            {
                throw;
            }
        }
    }
}
