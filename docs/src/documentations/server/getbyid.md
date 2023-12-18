```
long serverId = 42;

SingledResponse<Server> response = await _serverService.GetByIdAsync(serverId);

JsonDocument originalJson = response.JsonDocument;
Server server = response.Item;
```