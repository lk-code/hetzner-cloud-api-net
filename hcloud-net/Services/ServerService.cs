using lkcode.hetznercloudapi.Interfaces;

namespace lkcode.hetznercloudapi.Services;

public class ServerService : IServerService
{
    private readonly IHetznerCloudService _hetznerCloudService;

    public ServerService(IHetznerCloudService hetznerCloudService)
    {
        this._hetznerCloudService = hetznerCloudService ?? throw new ArgumentNullException(nameof(hetznerCloudService));
    }
}
