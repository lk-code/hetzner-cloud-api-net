namespace lkcode.hetznercloudapi.Exceptions;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(string message)
        : base(message)
    {
    }

    public ResourceNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}