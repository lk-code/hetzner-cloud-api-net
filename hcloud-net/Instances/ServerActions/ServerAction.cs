using lkcode.hetznercloudapi.Enums;

namespace lkcode.hetznercloudapi.Instances.ServerActions;

/// <summary>
/// 
/// </summary>
public class ServerAction
{
    /// <summary>
    /// Command executed in the Action
    /// </summary>
    public string Command { get; set; }
    /// <summary>
    /// Error message for the Action if error occurred, otherwise null
    /// </summary>
    public ActionErrorResult? Error { get; set; }
    /// <summary>
    /// Point in time when the Action was finished (in ISO-8601 format). Only set if the Action is finished otherwise null.
    /// </summary>
    public DateTime? Finished { get; set; }
    /// <summary>
    /// ID of the Resource
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// Progress of Action in percent
    /// </summary>
    public double Progress { get; set; }
    /// <summary>
    /// Resources the Action relates to
    /// </summary>
    public IEnumerable<ActionResourceResult> Resources { get; set; }
    /// <summary>
    /// Point in time when the Action was started (in ISO-8601 format)
    /// </summary>
    public DateTime Started { get; set; }
    /// <summary>
    /// Status of the Action
    /// </summary>
    public ServerActionsResult Status { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="command">Command executed in the Action</param>
    /// <param name="id">ID of the Resource</param>
    /// <param name="progress">Progress of Action in percent</param>
    /// <param name="resources">Resources the Action relates to</param>
    /// <param name="started">Point in time when the Action was started (in ISO-8601 format)</param>
    /// <param name="status">Status of the Action</param>
    public ServerAction(string command,
        long id,
        double progress,
        IEnumerable<ActionResourceResult> resources,
        DateTime started,
        ServerActionsResult status)
    {
        Command = command;
        Id = id;
        Progress = progress;
        Resources = resources;
        Started = started;
        Status = status;
    }
}
