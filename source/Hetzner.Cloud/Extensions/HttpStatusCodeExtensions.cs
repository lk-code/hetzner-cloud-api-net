using System.Net;
using Hetzner.Cloud.Exceptions;

namespace Hetzner.Cloud.Extensions;

public static class HttpStatusCodeExtensions
{
    public static void ThrowIfNotSuccess(this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }
        
        HttpStatusCode statusCode = response.StatusCode switch
        {
            HttpStatusCode.Unauthorized => throw new UnauthorizedException("The API returned an Unauthorized error. Perhaps the API token has expired.", response),
            HttpStatusCode.NotFound => throw new ResourceNotFoundException("The requested resource was not found", response),
            _ => throw new ApiException(response, $"Invalid Request")
        };
    }
}