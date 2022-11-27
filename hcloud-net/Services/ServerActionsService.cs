using lkcode.hetznercloudapi.Exceptions;
using lkcode.hetznercloudapi.Instances.ServerActions;
using lkcode.hetznercloudapi.Interfaces;
using lkcode.hetznercloudapi.Mapping;
using lkcode.hetznercloudapi.Models.Api.ServerActions;

namespace lkcode.hetznercloudapi.Services;

public class ServerActionsService : IServerActionsService
{
    private readonly IHetznerCloudService _hetznerCloudService;

    public ServerActionsService(IHetznerCloudService hetznerCloudService)
    {
        this._hetznerCloudService = hetznerCloudService ?? throw new ArgumentNullException(nameof(hetznerCloudService));
    }

    /// <inheritdoc/>
    public async Task<ActionResult> Shutdown(long id)
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

            ActionResult result = response.ToActionResult();

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
