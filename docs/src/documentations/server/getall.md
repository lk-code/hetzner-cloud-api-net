##### simple request

The following call consists of the default arguments of the method, which returns the first page of the server listing.

```
PagedResponse<Server> response = await _serverService.GetAllAsync();

JsonDocument originalJson = response.JsonDocument;
IReadOnlyCollection<Server> server = response.Items;
```

---

##### request server listing with specific page

The following call requests page 4 from the server listing.

```
PagedResponse<Server> response = await _serverService.GetAllAsync(4);

JsonDocument originalJson = response.JsonDocument;
IReadOnlyCollection<Server> server = response.Items;
```

---

##### request server listing with specific page and number of items per page

The following call requests page 4 of the server listing, with 10 entries per page.

```
PagedResponse<Server> response = await _serverService.GetAllAsync(4, 10);

JsonDocument originalJson = response.JsonDocument;
IReadOnlyCollection<Server> server = response.Items;
```

---

##### filter by name

The following call requests entries that are filtered by server name.

```
List<IFilter> filter = new();
filter.Add(new NameFilter("test"));

PagedResponse<Server> response = await _serverService.GetAllAsync(1, 25, filter);

JsonDocument originalJson = response.JsonDocument;
IReadOnlyCollection<Server> server = response.Items;
```

---

##### filter by status

The following call requests entries that are filtered by server status.

```
List<IFilter> filter = new();
filter.Add(new StatusFilter(ServerStatus.Running));

PagedResponse<Server> response = await _serverService.GetAllAsync(1, 25, filter);

JsonDocument originalJson = response.JsonDocument;
IReadOnlyCollection<Server> server = response.Items;
```

---

##### filter by label

The following call requests entries that are filtered by server label.

```
List<IFilter> filter = new();
filter.Add(new LabelFilter("test"));

PagedResponse<Server> response = await _serverService.GetAllAsync(1, 25, filter);

JsonDocument originalJson = response.JsonDocument;
IReadOnlyCollection<Server> server = response.Items;
```

---

##### sorting

The following call requests entries sorted by server name (descending)

```
Sorting<ServerSorting>? _sorting = new(ServerSorting.Name, SortingDirection.DESC);

PagedResponse<Server> response = await _serverService.GetAllAsync(1, 25, null, sorting);

JsonDocument originalJson = response.JsonDocument;
IReadOnlyCollection<Server> server = response.Items;
```