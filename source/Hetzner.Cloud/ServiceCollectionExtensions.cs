using System.Net.Http.Headers;
using Hetzner.Cloud.Interfaces;
using Hetzner.Cloud.Services;
using lkcode.hetznercloudapi.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Hetzner.Cloud;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// add hetzner cloud services
    /// </summary>
    /// <param name="services">the <see cref="IServiceCollection"/> instance</param>
    /// <returns>the <see cref="IServiceCollection"/> instance</returns>
    public static IServiceCollection AddHetznerCloud(this IServiceCollection services)
    {
        services
            .AddHttpClient("HetznerCloudHttpClient",
                client =>
                {
                    // some configuration
                })
            .ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                IHetznerCloudService hetznerCloudService = serviceProvider.GetRequiredService<IHetznerCloudService>();

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                hetznerCloudService.ConfigureClient(serviceProvider, httpClient);
            });

        services.AddSingleton<IHetznerCloudService, HetznerCloudService>();
        services.AddSingleton<IServerService, ServerService>();

        return services;
    }
}