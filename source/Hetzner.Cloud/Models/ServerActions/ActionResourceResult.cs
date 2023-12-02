namespace Hetzner.Cloud.Instances.ServerActions;

/// <summary>
/// Resource the Action relates to
/// </summary>
public class ActionResourceResult
{
    /// <summary>
    /// ID of the Resource
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// Type of resource referenced
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Resource the Action relates to
    /// </summary>
    /// <param name="id">ID of the Resource</param>
    /// <param name="type">Type of resource referenced</param>
    public ActionResourceResult(long id,
        string type)
    {
        Id = id;
        Type = type;
    }
}
