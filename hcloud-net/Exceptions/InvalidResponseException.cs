namespace lkcode.hetznercloudapi.Exceptions;

public class InvalidResponseException : Exception
{
    public InvalidResponseException()
    {
    }

    public InvalidResponseException(string message)
        : base(message, null)
    {
    }
}
