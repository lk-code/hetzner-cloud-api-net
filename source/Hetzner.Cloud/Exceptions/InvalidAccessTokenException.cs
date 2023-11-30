namespace Hetzner.Cloud.Exceptions;

public class InvalidAccessTokenException : Exception
{
    public InvalidAccessTokenException(string message)
        : base(message)
    {
    }

    public InvalidAccessTokenException(string message, Exception inner)
        : base(message, inner)
    {
    }
}