using System.Net;

namespace Hetzner.Cloud.Exceptions;

public class ApiException : Exception
{
    public HttpStatusCode StatusCode => Response.StatusCode;
    public readonly HttpResponseMessage Response;

    public ApiException(HttpResponseMessage response, string message)
        : base(message)
    {
        Response = response;
    }

    public ApiException(HttpResponseMessage response, string message, Exception inner)
        : base(message, inner)
    {
        Response = response;
    }
}