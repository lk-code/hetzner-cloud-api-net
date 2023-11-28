# Hetzner Cloud API for .NET

![Hetzner Cloud API for .NET](https://raw.githubusercontent.com/lk-code/hetzner-cloud-api-net/main/icon_128.png)

[![.NET Version](https://img.shields.io/badge/dotnet%20version-net6.0-blue?style=flat-square)](http://www.nuget.org/packages/hetznercloudapi/)
[![License](https://img.shields.io/github/license/lk-code/hetzner-cloud-api-net.svg?style=flat-square)](https://github.com/lk-code/hetzner-cloud-api-net/blob/master/LICENSE)
[![Build](https://github.com/lk-code/hetzner-cloud-api-net/actions/workflows/dotnet.yml/badge.svg)](https://github.com/lk-code/hetzner-cloud-api-net/actions/workflows/dotnet.yml)
[![Downloads](https://img.shields.io/nuget/dt/hetznercloudapi.svg?style=flat-square)](http://www.nuget.org/packages/hetznercloudapi/)
[![NuGet](https://img.shields.io/nuget/v/hetznercloudapi.svg?style=flat-square)](http://nuget.org/packages/hetznercloudapi)

[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=lk-code_hetzner-cloud-api-net&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=lk-code_hetzner-cloud-api-net)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=lk-code_hetzner-cloud-api-net&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=lk-code_hetzner-cloud-api-net)

Here you can find a .NET library for the Hetzner Cloud API, with which all functions of the endpoints can be used.

The current version is provided as .NET Standard 2.0, currently I am working on a new version for .NET 6 (with features like dependency injection, etc.)

<a href="https://www.buymeacoffee.com/lk.code" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/v2/default-yellow.png" alt="Buy Me A Coffee" style="height: 60px !important;width: 217px !important;" ></a>

# Hetzner Cloud API Client for .NET

## installation

see the getting started page here https://github.com/lk-code/hetzner-cloud-api-net/wiki/getting-started

## demo

see the demo projects here https://github.com/lk-code/hetzner-cloud-api-net-demo

## documentation

see the documentation on https://github.com/lk-code/hetzner-cloud-api-net/wiki

## informations :)

see the demo projects here https://github.com/lk-code/hetzner-cloud-api-net-demo

# New Documentation for Hetzner Cloud API Client (based on v2-Client)

## installation

```
dotnet add package hetznercloudapi
```

## usage

### initialization

#### API Token via AppSettings

add the following block to your AppSettings:

```
{
    "HetznerCloud": {
        "ApiToken": "YOUR_API_TOKEN"
    }
}
```

#### Load API Token dynamically

Alternatively, the API token can also be set dynamically:

```
IHetznerCloudService _hetznerCloudService = {get instance via DI};

...

_hetznerCloudService.LoadApiToken("{YOUR_API_TOKEN}");
```

[![Contributors](https://contrib.rocks/image?repo=lk-code/hetzner-cloud-api-net)](https://github.com/lk-code/hetzner-cloud-api-net/graphs/contributors)
