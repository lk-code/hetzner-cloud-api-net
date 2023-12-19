To use the service, you can use dependency injection:

```
public class YourService {
    private readonly IServerService _serverService;

    public YourService(IServerService serverService) {
        _serverService = serverService;
    }
}
```