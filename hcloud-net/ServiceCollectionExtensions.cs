using Microsoft.Extensions.DependencyInjection;

namespace lkcode.hetznercloudapi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHetznerCloud(this IServiceCollection service)
    {
        service.AddSingleton<IHetznerCloudService, HetznerCloudService>();

        return service;
    }
}
