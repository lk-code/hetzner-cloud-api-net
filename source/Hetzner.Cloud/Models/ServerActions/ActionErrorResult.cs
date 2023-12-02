namespace Hetzner.Cloud.Instances.ServerActions;

/// <summary>
/// Error message for the Action
/// </summary>
public class ActionErrorResult
{
    /// <summary>
    /// Fixed machine readable code
    /// </summary>
    public string Code { get; set; }
    /// <summary>
    /// Humanized error message
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Error message for the Action
    /// </summary>
    /// <param name="code">Fixed machine readable code</param>
    /// <param name="message">Humanized error message</param>
    public ActionErrorResult(string code,
        string message)
    {
        Code = code;
        Message = message;
    }
}
