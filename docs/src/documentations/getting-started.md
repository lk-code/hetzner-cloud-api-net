### 1. Installation

The current version is available as a pre-release for .NET 8. To load this version, you must activate the pre-release flag in the NuGet search.

Alternatively, you can download the current stable version (.NET 6).

```
dotnet add package hetznercloudapi
```

---


### 2. Service Registration

on your IServiceCollection-Instance add the following registration-method:

```
IServiceCollection services = builder.Services; // get it from your DI-Builder

services.AddHetznerCloud(); // optionally, you can define an API token here
```

This will register all Hetzner Cloud Services via dependency injection (Including the HTTP clients).


---


### 3. API Token

You can load the Hetzner Cloud API token in various ways.

#### 3.1. via AppSettings (static)

Add the following lines to your appsettings.json:

```
{
  "HetznerCloud": {
    "ApiToken": "{YOUR_API_TOKEN}"
  }
}
```


#### 3.2. via Dependency Injection (dynamic)

```
IHetznerCloudService hetznerCloudService = ...; // get it from your DI-Manager

hetznerCloudService.LoadApiToken("{YOUR_API_TOKEN}");
```


#### 3.3. via Service (dynamic)

```
IServiceCollection services = builder.Services; // get it from your DI-Builder

services.AddHetznerCloud("{YOUR_API_TOKEN}");
```


---


### 4. Basics


#### 4.1. PagedResponse

The PagedResponse class is returned by all methods that call a Hetzner Cloud API route that uses pagination. PagedResponse contains the results of the query and the pagination values.


```
public class PagedResponse<T> {
    /// <summary>
    /// Contains the response of the Hetzner Cloud as JSON.
    /// </summary>
    public readonly JsonDocument JsonDocument { get; }
    /// <summary>
    /// Specifies which page the current request refers to.
    /// </summary>
    public readonly long CurrentPage { get; }
    /// <summary>
    /// Specifies how many items are output per page.
    /// </summary>
    public readonly long ItemsPerPage { get; }
    /// <summary>
    /// Specifies how many items there are in total.
    /// </summary>
    public readonly long TotalItems { get; }
    /// <summary>
    /// Contains all items of the current request.
    /// </summary>
    public IReadOnlyCollection<T> Items { get; set; }
}
```


#### 4.2. SingledResponse

The SingledResponse class is returned by all methods that call a Hetzner Cloud API route that return a single object. SingledResponse contains the result of the query and the original JSON.


```
public class SingledResponse<T> {
    /// <summary>
    /// Contains the response of the Hetzner Cloud as JSON.
    /// </summary>
    public readonly JsonDocument JsonDocument { get; }
    /// <summary>
    /// Contains the item of the current request.
    /// </summary>
    public T Item { get; set; }
}
```

#### 4.3. Execute own HTTP-Requests

You can retrieve the HTTP client via dependency injection and use it for HTTP requests. The HTTP client is configured and uses the API token.

Call up the Http client with the key "HetznerCloudHttpClient".

```
private HttpClient _httpClient;

YouClassConstructor(IHttpClientFactory httpClientFactory) {
    _httpClient = httpClientFactory.CreateClient("HetznerCloudHttpClient");
}

public async Task YourMethod() {
    HttpResponseMessage response = await _httpClient.GetAsync("/v1/servers", cancellationToken);
}
```