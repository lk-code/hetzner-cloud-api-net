using Hetzner.Cloud.Mapping;

namespace Hetzner.Cloud.Models;

public class ServerAction(
    long id,
    string command,
    int progress,
    ServerActionResource[] resources,
    DateTime started,
    ServerActionStatus status)
{
    /// <summary>
    /// ID of the Action. Limited to 52 bits to ensure compatability with JSON double precision floats.
    /// </summary>
    public long Id { get; } = id;

    /// <summary>
    /// Command executed in the Action
    /// </summary>
    public string Command { get; } = command;

    /// <summary>
    /// Error message for the Action if error occurred, otherwise null
    /// </summary>
    public Error? Error { get; internal init; }

    /// <summary>
    /// Point in time when the Action was finished (in ISO-8601 format). Only set if the Action is finished otherwise null.
    /// </summary>
    public DateTime? Finished { get; internal init; }

    /// <summary>
    /// Progress of Action in percent
    /// </summary>
    public int Progress { get; } = progress;

    /// <summary>
    /// Resources the Action relates to
    /// </summary>
    public ServerActionResource[] Resources { get; } = resources;

    /// <summary>
    /// Point in time when the Action was started (in ISO-8601 format)
    /// </summary>
    public DateTime Started { get; } = started;

    /// <summary>
    /// Possible enum values: success, running, error
    /// Status of the Action
    /// </summary>
    public ServerActionStatus Status { get; } = status;
}