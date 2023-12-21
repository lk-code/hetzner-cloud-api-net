namespace Hetzner.Cloud.Exceptions;

public class UnauthorizedException : ApiException
{
    public UnauthorizedException(string message,
        HttpResponseMessage response)
        : base(response, message)
    {
    }

    public UnauthorizedException(string message,
        HttpResponseMessage response,
        Exception inner)
        : base(response, message, inner)
    {
    }
}