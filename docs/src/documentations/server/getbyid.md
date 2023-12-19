The following call retrieves the server details via the server ID. If the server does not exist, a ResourceNotFoundException is thrown.

```
long serverId = 42;

try
{
    SingledResponse<Server> response = await _serverService.GetByIdAsync(serverId);

    JsonDocument originalJson = response.JsonDocument;
    Server server = response.Item;
}
catch (ResourceNotFoundException err)
{
    // server not found
}
```