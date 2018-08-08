## hetzner-cloud-api for .net

the project is a .NET Standard 2.0 Library and can be used in the most projects like:

- Universal Windows Platform (UWP) (minimum target version is the fall creators update - https://blogs.msdn.microsoft.com/dotnet/2017/10/10/announcing-uwp-support-for-net-standard-2-0/)
- Xamarin and Xamarin.Forms (only .NET Standard)
- Windows Presentation Foundation (WPF)
- Console Application
- ASP.NET

### requirements

- JSON.NET

# examples

## initialize
```
// this code as first in the init-method
CloudApiNet.Core.ApiCore.ApiToken = "YOUR_ACCESS_TOKEN_HERE";
```

## load server

### get all server

Server.GetAsync() is a static method and returns all server.

```
List<CloudApiNet.Api.Server> serverList = await CloudApiNet.Api.Server.GetAsync();
```

### get all server with filter

Server.GetAsync() is a static method and returns all server.

```
string serverFilter = "servername";
List<CloudApiNet.Api.Server> serverList = await CloudApiNet.Api.Server.GetAsync(serverFilter);
```

### get one server

Server.GetAsync(long id) is a static method and returns a specific server.

```
long id = 5864;
CloudApiNet.Api.Server server = await CloudApiNet.Api.Server.GetAsync(id);
```

## the server object

with the returned server object you can execute every server action.

### Server.PowerOn()

```
long id = 5864;
CloudApiNet.Api.Server server = await CloudApiNet.Api.Server.GetAsync(id);

ServerActionResponse actionResponse = await server.PowerOn();
```

### Server.Reboot()

```
long id = 5864;
CloudApiNet.Api.Server server = await CloudApiNet.Api.Server.GetAsync(id);

ServerActionResponse actionResponse = await server.Reboot();
```

### Server.Reset()

```
long id = 5864;
CloudApiNet.Api.Server server = await CloudApiNet.Api.Server.GetAsync(id);

ServerActionResponse actionResponse = await server.Reset();
```

### Server.Shutdown()

```
long id = 5864;
CloudApiNet.Api.Server server = await CloudApiNet.Api.Server.GetAsync(id);

ServerActionResponse actionResponse = await server.Shutdown();
```

### Server.PowerOff()

```
long id = 5864;
CloudApiNet.Api.Server server = await CloudApiNet.Api.Server.GetAsync(id);

ServerActionResponse actionResponse = await server.PowerOff();
```

### Server.ResetPassword()

```
long id = 5864;
CloudApiNet.Api.Server server = await CloudApiNet.Api.Server.GetAsync(id);

ServerActionResponse actionResponse = await server.Shutdown();
string newPassword = (string)actionResponse.AdditionalActionContent;
```