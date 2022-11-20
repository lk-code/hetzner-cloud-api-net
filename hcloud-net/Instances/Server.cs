namespace lkcode.hetznercloudapi.Instances;

public class Server
{
    public long Id { get; private set; } = 0;
    public string Name { get; internal set; } = string.Empty;

    public Server(long id)
    {
        Id = id;
    }
}
