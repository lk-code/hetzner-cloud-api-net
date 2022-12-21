namespace lkcode.hetznercloudapi.Exceptions;

public class ActionNotAllowedException : Exception
{
    public ActionNotAllowedException(string message)
        : base(message)
    {
    }

    public ActionNotAllowedException(string message, Exception inner)
        : base(message, inner)
    {
    }
}