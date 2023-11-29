using System.Net;

namespace lkcode.hetznercloudapi.Exceptions.Http;

public class ApiException : Exception
{
    public readonly HttpStatusCode StatusCode;

    public ApiException(HttpStatusCode statusCode, string message)
        : base(message)
    {
        StatusCode = statusCode;
    }

    public ApiException(HttpStatusCode statusCode, string message, Exception inner)
        : base(message, inner)
    {
        StatusCode = statusCode;
    }
}