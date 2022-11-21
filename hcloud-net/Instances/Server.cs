using lkcode.hetznercloudapi.Enums;

namespace lkcode.hetznercloudapi.Instances;

public class Server
{
    public long Id { get; private set; } = 0;
    public string Name { get; internal set; } = string.Empty;
    public ServerStatus Status { get; internal set; } = ServerStatus.Unknown;
    public DateTime Created { get; internal set; } = DateTime.MinValue;

    public Server(long id)
    {
        Id = id;
    }
}
