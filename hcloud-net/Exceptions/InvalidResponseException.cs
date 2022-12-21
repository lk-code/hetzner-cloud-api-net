namespace lkcode.hetznercloudapi.Exceptions;

public class InvalidResponseException : Exception
{
    public InvalidResponseException(string message)
        : base(message)
    {
    }

    public InvalidResponseException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
