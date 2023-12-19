namespace Hetzner.Cloud.Models;

public class Error(string code, string message)
{
    /// <summary>
    /// Error code
    /// </summary>
    public string Code { get;  } = code;

    /// <summary>
    /// Error message
    /// </summary>
    public string Message { get; } = message;
}