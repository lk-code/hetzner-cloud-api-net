using lkcode.hetznercloudapi.Interfaces;
using lkcode.hetznercloudapi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace lkcode.hetznercloudapi;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// add hetzner cloud services
    /// </summary>
    /// <param name="service">the <see cref="IServiceCollection"/> instance</param>
    /// <returns>the <see cref="IServiceCollection"/> instance</returns>
    public static IServiceCollection AddHetznerCloud(this IServiceCollection service)
    {
        service.AddSingleton<IHetznerCloudService, HetznerCloudService>();
        service.AddSingleton<IServerService, ServerService>();
        service.AddSingleton<IServerActionsService, ServerActionsService>();

        return service;
    }
}
