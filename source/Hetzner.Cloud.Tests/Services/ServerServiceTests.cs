using FluentAssertions;
using lkcode.hetznercloudapi.Enums;
using lkcode.hetznercloudapi.Exceptions;
using lkcode.hetznercloudapi.Interfaces;
using lkcode.hetznercloudapi.Models.Api.Server;
using lkcode.hetznercloudapi.Services;
using Moq;
using Newtonsoft.Json;

namespace hcloud_net.Test.Services;

[TestClass]
public class ServerServiceTests
{
    private Mock<IHetznerCloudService> _hetznerCloudService = null!;

    private IServerService _instance = null!;

    public ServerServiceTests()
    {
        this._hetznerCloudService = new Mock<IHetznerCloudService>();

        this._instance = new ServerService(this._hetznerCloudService.Object);
    }

    [TestMethod]
    public async Task GetByIdAsync_WithDemoServer_ReturnsCorrectData()
    {
        long serverId = 42;

        string serverResponse = "{\n  \"server\": {\n    \"backup_window\": \"22-02\",\n    \"created\": \"2016-01-30T23:55:00+00:00\",\n    \"datacenter\": {\n      \"description\": \"Falkenstein DC Park 8\",\n      \"id\": 42,\n      \"location\": {\n        \"city\": \"Falkenstein\",\n        \"country\": \"DE\",\n        \"description\": \"Falkenstein DC Park 1\",\n        \"id\": 1,\n        \"latitude\": 50.47612,\n        \"longitude\": 12.370071,\n        \"name\": \"fsn1\",\n        \"network_zone\": \"eu-central\"\n      },\n      \"name\": \"fsn1-dc8\",\n      \"server_types\": {\n        \"available\": [\n          1,\n          2,\n          3\n        ],\n        \"available_for_migration\": [\n          1,\n          2,\n          3\n        ],\n        \"supported\": [\n          1,\n          2,\n          3\n        ]\n      }\n    },\n    \"id\": 42,\n    \"image\": {\n      \"bound_to\": null,\n      \"created\": \"2016-01-30T23:55:00+00:00\",\n      \"created_from\": {\n        \"id\": 1,\n        \"name\": \"Server\"\n      },\n      \"deleted\": null,\n      \"deprecated\": \"2018-02-28T00:00:00+00:00\",\n      \"description\": \"Ubuntu 20.04 Standard 64 bit\",\n      \"disk_size\": 10,\n      \"id\": 42,\n      \"image_size\": 2.3,\n      \"labels\": {},\n      \"name\": \"ubuntu-20.04\",\n      \"os_flavor\": \"ubuntu\",\n      \"os_version\": \"20.04\",\n      \"protection\": {\n        \"delete\": false\n      },\n      \"rapid_deploy\": false,\n      \"status\": \"available\",\n      \"type\": \"snapshot\"\n    },\n    \"included_traffic\": 654321,\n    \"ingoing_traffic\": 123456,\n    \"iso\": {\n      \"deprecated\": \"2018-02-28T00:00:00+00:00\",\n      \"description\": \"FreeBSD 11.0 x64\",\n      \"id\": 42,\n      \"name\": \"FreeBSD-11.0-RELEASE-amd64-dvd1\",\n      \"type\": \"public\"\n    },\n    \"labels\": {},\n    \"load_balancers\": [],\n    \"locked\": false,\n    \"name\": \"my-resource\",\n    \"outgoing_traffic\": 123456,\n    \"placement_group\": {\n      \"created\": \"2016-01-30T23:55:00+00:00\",\n      \"id\": 42,\n      \"labels\": {},\n      \"name\": \"my-resource\",\n      \"servers\": [\n        42\n      ],\n      \"type\": \"spread\"\n    },\n    \"primary_disk_size\": 50,\n    \"private_net\": [\n      {\n        \"alias_ips\": [],\n        \"ip\": \"10.0.0.2\",\n        \"mac_address\": \"86:00:ff:2a:7d:e1\",\n        \"network\": 4711\n      }\n    ],\n    \"protection\": {\n      \"delete\": false,\n      \"rebuild\": false\n    },\n    \"public_net\": {\n      \"firewalls\": [\n        {\n          \"id\": 42,\n          \"status\": \"applied\"\n        }\n      ],\n      \"floating_ips\": [\n        478\n      ],\n      \"ipv4\": {\n        \"blocked\": false,\n        \"dns_ptr\": \"server01.example.com\",\n        \"id\": 42,\n        \"ip\": \"1.2.3.4\"\n      },\n      \"ipv6\": {\n        \"blocked\": false,\n        \"dns_ptr\": [\n          {\n            \"dns_ptr\": \"server.example.com\",\n            \"ip\": \"2001:db8::1\"\n          }\n        ],\n        \"id\": 42,\n        \"ip\": \"2001:db8::/64\"\n      }\n    },\n    \"rescue_enabled\": false,\n    \"server_type\": {\n      \"cores\": 1,\n      \"cpu_type\": \"shared\",\n      \"deprecated\": false,\n      \"description\": \"CX11\",\n      \"disk\": 25,\n      \"id\": 1,\n      \"memory\": 1,\n      \"name\": \"cx11\",\n      \"prices\": [\n        {\n          \"location\": \"fsn1\",\n          \"price_hourly\": {\n            \"gross\": \"1.1900000000000000\",\n            \"net\": \"1.0000000000\"\n          },\n          \"price_monthly\": {\n            \"gross\": \"1.1900000000000000\",\n            \"net\": \"1.0000000000\"\n          }\n        }\n      ],\n      \"storage_type\": \"local\"\n    },\n    \"status\": \"running\",\n    \"volumes\": []\n  }\n}";
        GetByIdResponse? response = JsonConvert.DeserializeObject<GetByIdResponse>(serverResponse);

        this._hetznerCloudService
            .Setup(x => x.GetRequest<GetByIdResponse>(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
            .ReturnsAsync(response);

        var server = await _instance.GetByIdAsync(serverId);

        server.Should().NotBeNull();

        server.Id.Should().Be(42);
        server.Status.Should().Be(ServerStatus.Running);
        server.Name.Should().Be("my-resource");
    }

    [TestMethod]
    public async Task GetByIdAsync_WithMultipleStatus_ReturnsCorrectData()
    {
        long serverId = 42;
        Dictionary<string, ServerStatus> testStatusItems = new Dictionary<string, ServerStatus>
        {
            { "unknown", ServerStatus.Unknown },
            { "blablub", ServerStatus.Unknown },
            { "test", ServerStatus.Unknown },
            { "running", ServerStatus.Running },
            { "initializing", ServerStatus.Initializing},
            { "starting", ServerStatus.Starting },
            { "stopping", ServerStatus.Stopping },
            { "off", ServerStatus.Off },
            { "deleting", ServerStatus.Deleting },
            { "migrating", ServerStatus.Migrating },
            { "rebuilding", ServerStatus.Rebuilding },
            { "Running", ServerStatus.Running },
            { "Initializing", ServerStatus.Initializing},
            { "Starting", ServerStatus.Starting },
            { "Stopping", ServerStatus.Stopping },
            { "Off", ServerStatus.Off },
            { "Deleting", ServerStatus.Deleting },
            { "Migrating", ServerStatus.Migrating },
            { "Rebuilding", ServerStatus.Rebuilding }
        };

        string serverResponse = "{\n  \"server\": {\n    \"backup_window\": \"22-02\",\n    \"created\": \"2016-01-30T23:55:00+00:00\",\n    \"datacenter\": {\n      \"description\": \"Falkenstein DC Park 8\",\n      \"id\": 42,\n      \"location\": {\n        \"city\": \"Falkenstein\",\n        \"country\": \"DE\",\n        \"description\": \"Falkenstein DC Park 1\",\n        \"id\": 1,\n        \"latitude\": 50.47612,\n        \"longitude\": 12.370071,\n        \"name\": \"fsn1\",\n        \"network_zone\": \"eu-central\"\n      },\n      \"name\": \"fsn1-dc8\",\n      \"server_types\": {\n        \"available\": [\n          1,\n          2,\n          3\n        ],\n        \"available_for_migration\": [\n          1,\n          2,\n          3\n        ],\n        \"supported\": [\n          1,\n          2,\n          3\n        ]\n      }\n    },\n    \"id\": 42,\n    \"image\": {\n      \"bound_to\": null,\n      \"created\": \"2016-01-30T23:55:00+00:00\",\n      \"created_from\": {\n        \"id\": 1,\n        \"name\": \"Server\"\n      },\n      \"deleted\": null,\n      \"deprecated\": \"2018-02-28T00:00:00+00:00\",\n      \"description\": \"Ubuntu 20.04 Standard 64 bit\",\n      \"disk_size\": 10,\n      \"id\": 42,\n      \"image_size\": 2.3,\n      \"labels\": {},\n      \"name\": \"ubuntu-20.04\",\n      \"os_flavor\": \"ubuntu\",\n      \"os_version\": \"20.04\",\n      \"protection\": {\n        \"delete\": false\n      },\n      \"rapid_deploy\": false,\n      \"status\": \"available\",\n      \"type\": \"snapshot\"\n    },\n    \"included_traffic\": 654321,\n    \"ingoing_traffic\": 123456,\n    \"iso\": {\n      \"deprecated\": \"2018-02-28T00:00:00+00:00\",\n      \"description\": \"FreeBSD 11.0 x64\",\n      \"id\": 42,\n      \"name\": \"FreeBSD-11.0-RELEASE-amd64-dvd1\",\n      \"type\": \"public\"\n    },\n    \"labels\": {},\n    \"load_balancers\": [],\n    \"locked\": false,\n    \"name\": \"my-resource\",\n    \"outgoing_traffic\": 123456,\n    \"placement_group\": {\n      \"created\": \"2016-01-30T23:55:00+00:00\",\n      \"id\": 42,\n      \"labels\": {},\n      \"name\": \"my-resource\",\n      \"servers\": [\n        42\n      ],\n      \"type\": \"spread\"\n    },\n    \"primary_disk_size\": 50,\n    \"private_net\": [\n      {\n        \"alias_ips\": [],\n        \"ip\": \"10.0.0.2\",\n        \"mac_address\": \"86:00:ff:2a:7d:e1\",\n        \"network\": 4711\n      }\n    ],\n    \"protection\": {\n      \"delete\": false,\n      \"rebuild\": false\n    },\n    \"public_net\": {\n      \"firewalls\": [\n        {\n          \"id\": 42,\n          \"status\": \"applied\"\n        }\n      ],\n      \"floating_ips\": [\n        478\n      ],\n      \"ipv4\": {\n        \"blocked\": false,\n        \"dns_ptr\": \"server01.example.com\",\n        \"id\": 42,\n        \"ip\": \"1.2.3.4\"\n      },\n      \"ipv6\": {\n        \"blocked\": false,\n        \"dns_ptr\": [\n          {\n            \"dns_ptr\": \"server.example.com\",\n            \"ip\": \"2001:db8::1\"\n          }\n        ],\n        \"id\": 42,\n        \"ip\": \"2001:db8::/64\"\n      }\n    },\n    \"rescue_enabled\": false,\n    \"server_type\": {\n      \"cores\": 1,\n      \"cpu_type\": \"shared\",\n      \"deprecated\": false,\n      \"description\": \"CX11\",\n      \"disk\": 25,\n      \"id\": 1,\n      \"memory\": 1,\n      \"name\": \"cx11\",\n      \"prices\": [\n        {\n          \"location\": \"fsn1\",\n          \"price_hourly\": {\n            \"gross\": \"1.1900000000000000\",\n            \"net\": \"1.0000000000\"\n          },\n          \"price_monthly\": {\n            \"gross\": \"1.1900000000000000\",\n            \"net\": \"1.0000000000\"\n          }\n        }\n      ],\n      \"storage_type\": \"local\"\n    },\n    \"status\": \"running\",\n    \"volumes\": []\n  }\n}";
        GetByIdResponse? response = JsonConvert.DeserializeObject<GetByIdResponse>(serverResponse);

        foreach (var testStatusItem in testStatusItems)
        {
            response!.Server!.Status = testStatusItem.Key;

            this._hetznerCloudService
                .Setup(x => x.GetRequest<GetByIdResponse>(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
                .ReturnsAsync(response);

            var server = await _instance.GetByIdAsync(serverId);

            server.Should().NotBeNull();

            server.Status.Should().Be(testStatusItem.Value);
        }
    }

    [TestMethod]
    public async Task GetByIdAsync_WithDemoServerAndStatusMigrating_ReturnsCorrectData()
    {
        long serverId = 42;

        string serverResponse = "{\n  \"server\": {\n    \"backup_window\": \"22-02\",\n    \"created\": \"2016-01-30T23:55:00+00:00\",\n    \"datacenter\": {\n      \"description\": \"Falkenstein DC Park 8\",\n      \"id\": 42,\n      \"location\": {\n        \"city\": \"Falkenstein\",\n        \"country\": \"DE\",\n        \"description\": \"Falkenstein DC Park 1\",\n        \"id\": 1,\n        \"latitude\": 50.47612,\n        \"longitude\": 12.370071,\n        \"name\": \"fsn1\",\n        \"network_zone\": \"eu-central\"\n      },\n      \"name\": \"fsn1-dc8\",\n      \"server_types\": {\n        \"available\": [\n          1,\n          2,\n          3\n        ],\n        \"available_for_migration\": [\n          1,\n          2,\n          3\n        ],\n        \"supported\": [\n          1,\n          2,\n          3\n        ]\n      }\n    },\n    \"id\": 42,\n    \"image\": {\n      \"bound_to\": null,\n      \"created\": \"2016-01-30T23:55:00+00:00\",\n      \"created_from\": {\n        \"id\": 1,\n        \"name\": \"Server\"\n      },\n      \"deleted\": null,\n      \"deprecated\": \"2018-02-28T00:00:00+00:00\",\n      \"description\": \"Ubuntu 20.04 Standard 64 bit\",\n      \"disk_size\": 10,\n      \"id\": 42,\n      \"image_size\": 2.3,\n      \"labels\": {},\n      \"name\": \"ubuntu-20.04\",\n      \"os_flavor\": \"ubuntu\",\n      \"os_version\": \"20.04\",\n      \"protection\": {\n        \"delete\": false\n      },\n      \"rapid_deploy\": false,\n      \"status\": \"available\",\n      \"type\": \"snapshot\"\n    },\n    \"included_traffic\": 654321,\n    \"ingoing_traffic\": 123456,\n    \"iso\": {\n      \"deprecated\": \"2018-02-28T00:00:00+00:00\",\n      \"description\": \"FreeBSD 11.0 x64\",\n      \"id\": 42,\n      \"name\": \"FreeBSD-11.0-RELEASE-amd64-dvd1\",\n      \"type\": \"public\"\n    },\n    \"labels\": {},\n    \"load_balancers\": [],\n    \"locked\": false,\n    \"name\": \"my-resource\",\n    \"outgoing_traffic\": 123456,\n    \"placement_group\": {\n      \"created\": \"2016-01-30T23:55:00+00:00\",\n      \"id\": 42,\n      \"labels\": {},\n      \"name\": \"my-resource\",\n      \"servers\": [\n        42\n      ],\n      \"type\": \"spread\"\n    },\n    \"primary_disk_size\": 50,\n    \"private_net\": [\n      {\n        \"alias_ips\": [],\n        \"ip\": \"10.0.0.2\",\n        \"mac_address\": \"86:00:ff:2a:7d:e1\",\n        \"network\": 4711\n      }\n    ],\n    \"protection\": {\n      \"delete\": false,\n      \"rebuild\": false\n    },\n    \"public_net\": {\n      \"firewalls\": [\n        {\n          \"id\": 42,\n          \"status\": \"applied\"\n        }\n      ],\n      \"floating_ips\": [\n        478\n      ],\n      \"ipv4\": {\n        \"blocked\": false,\n        \"dns_ptr\": \"server01.example.com\",\n        \"id\": 42,\n        \"ip\": \"1.2.3.4\"\n      },\n      \"ipv6\": {\n        \"blocked\": false,\n        \"dns_ptr\": [\n          {\n            \"dns_ptr\": \"server.example.com\",\n            \"ip\": \"2001:db8::1\"\n          }\n        ],\n        \"id\": 42,\n        \"ip\": \"2001:db8::/64\"\n      }\n    },\n    \"rescue_enabled\": false,\n    \"server_type\": {\n      \"cores\": 1,\n      \"cpu_type\": \"shared\",\n      \"deprecated\": false,\n      \"description\": \"CX11\",\n      \"disk\": 25,\n      \"id\": 1,\n      \"memory\": 1,\n      \"name\": \"cx11\",\n      \"prices\": [\n        {\n          \"location\": \"fsn1\",\n          \"price_hourly\": {\n            \"gross\": \"1.1900000000000000\",\n            \"net\": \"1.0000000000\"\n          },\n          \"price_monthly\": {\n            \"gross\": \"1.1900000000000000\",\n            \"net\": \"1.0000000000\"\n          }\n        }\n      ],\n      \"storage_type\": \"local\"\n    },\n    \"status\": \"running\",\n    \"volumes\": []\n  }\n}";
        GetByIdResponse? response = JsonConvert.DeserializeObject<GetByIdResponse>(serverResponse);
        response!.Server!.Status = "migrating";

        this._hetznerCloudService
            .Setup(x => x.GetRequest<GetByIdResponse>(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
            .ReturnsAsync(response);

        var server = await _instance.GetByIdAsync(serverId);

        server.Should().NotBeNull();

        server.Status.Should().Be(ServerStatus.Migrating);
    }

    [TestMethod]
    public async Task GetByIdAsync_WithDemoServerAndStatusOff_ReturnsCorrectData()
    {
        long serverId = 42;

        string serverResponse = "{\n  \"server\": {\n    \"backup_window\": \"22-02\",\n    \"created\": \"2016-01-30T23:55:00+00:00\",\n    \"datacenter\": {\n      \"description\": \"Falkenstein DC Park 8\",\n      \"id\": 42,\n      \"location\": {\n        \"city\": \"Falkenstein\",\n        \"country\": \"DE\",\n        \"description\": \"Falkenstein DC Park 1\",\n        \"id\": 1,\n        \"latitude\": 50.47612,\n        \"longitude\": 12.370071,\n        \"name\": \"fsn1\",\n        \"network_zone\": \"eu-central\"\n      },\n      \"name\": \"fsn1-dc8\",\n      \"server_types\": {\n        \"available\": [\n          1,\n          2,\n          3\n        ],\n        \"available_for_migration\": [\n          1,\n          2,\n          3\n        ],\n        \"supported\": [\n          1,\n          2,\n          3\n        ]\n      }\n    },\n    \"id\": 42,\n    \"image\": {\n      \"bound_to\": null,\n      \"created\": \"2016-01-30T23:55:00+00:00\",\n      \"created_from\": {\n        \"id\": 1,\n        \"name\": \"Server\"\n      },\n      \"deleted\": null,\n      \"deprecated\": \"2018-02-28T00:00:00+00:00\",\n      \"description\": \"Ubuntu 20.04 Standard 64 bit\",\n      \"disk_size\": 10,\n      \"id\": 42,\n      \"image_size\": 2.3,\n      \"labels\": {},\n      \"name\": \"ubuntu-20.04\",\n      \"os_flavor\": \"ubuntu\",\n      \"os_version\": \"20.04\",\n      \"protection\": {\n        \"delete\": false\n      },\n      \"rapid_deploy\": false,\n      \"status\": \"available\",\n      \"type\": \"snapshot\"\n    },\n    \"included_traffic\": 654321,\n    \"ingoing_traffic\": 123456,\n    \"iso\": {\n      \"deprecated\": \"2018-02-28T00:00:00+00:00\",\n      \"description\": \"FreeBSD 11.0 x64\",\n      \"id\": 42,\n      \"name\": \"FreeBSD-11.0-RELEASE-amd64-dvd1\",\n      \"type\": \"public\"\n    },\n    \"labels\": {},\n    \"load_balancers\": [],\n    \"locked\": false,\n    \"name\": \"my-resource\",\n    \"outgoing_traffic\": 123456,\n    \"placement_group\": {\n      \"created\": \"2016-01-30T23:55:00+00:00\",\n      \"id\": 42,\n      \"labels\": {},\n      \"name\": \"my-resource\",\n      \"servers\": [\n        42\n      ],\n      \"type\": \"spread\"\n    },\n    \"primary_disk_size\": 50,\n    \"private_net\": [\n      {\n        \"alias_ips\": [],\n        \"ip\": \"10.0.0.2\",\n        \"mac_address\": \"86:00:ff:2a:7d:e1\",\n        \"network\": 4711\n      }\n    ],\n    \"protection\": {\n      \"delete\": false,\n      \"rebuild\": false\n    },\n    \"public_net\": {\n      \"firewalls\": [\n        {\n          \"id\": 42,\n          \"status\": \"applied\"\n        }\n      ],\n      \"floating_ips\": [\n        478\n      ],\n      \"ipv4\": {\n        \"blocked\": false,\n        \"dns_ptr\": \"server01.example.com\",\n        \"id\": 42,\n        \"ip\": \"1.2.3.4\"\n      },\n      \"ipv6\": {\n        \"blocked\": false,\n        \"dns_ptr\": [\n          {\n            \"dns_ptr\": \"server.example.com\",\n            \"ip\": \"2001:db8::1\"\n          }\n        ],\n        \"id\": 42,\n        \"ip\": \"2001:db8::/64\"\n      }\n    },\n    \"rescue_enabled\": false,\n    \"server_type\": {\n      \"cores\": 1,\n      \"cpu_type\": \"shared\",\n      \"deprecated\": false,\n      \"description\": \"CX11\",\n      \"disk\": 25,\n      \"id\": 1,\n      \"memory\": 1,\n      \"name\": \"cx11\",\n      \"prices\": [\n        {\n          \"location\": \"fsn1\",\n          \"price_hourly\": {\n            \"gross\": \"1.1900000000000000\",\n            \"net\": \"1.0000000000\"\n          },\n          \"price_monthly\": {\n            \"gross\": \"1.1900000000000000\",\n            \"net\": \"1.0000000000\"\n          }\n        }\n      ],\n      \"storage_type\": \"local\"\n    },\n    \"status\": \"running\",\n    \"volumes\": []\n  }\n}";
        GetByIdResponse? response = JsonConvert.DeserializeObject<GetByIdResponse>(serverResponse);
        response!.Server!.Status = "off";

        this._hetznerCloudService
            .Setup(x => x.GetRequest<GetByIdResponse>(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
            .ReturnsAsync(response);

        var server = await _instance.GetByIdAsync(serverId);

        server.Should().NotBeNull();

        server.Status.Should().Be(ServerStatus.Off);
    }

    [TestMethod]
    public async Task GetByIdAsync_WithDemoServerAndStatusInitializing_ReturnsCorrectData()
    {
        long serverId = 42;

        string serverResponse = "{\n  \"server\": {\n    \"backup_window\": \"22-02\",\n    \"created\": \"2016-01-30T23:55:00+00:00\",\n    \"datacenter\": {\n      \"description\": \"Falkenstein DC Park 8\",\n      \"id\": 42,\n      \"location\": {\n        \"city\": \"Falkenstein\",\n        \"country\": \"DE\",\n        \"description\": \"Falkenstein DC Park 1\",\n        \"id\": 1,\n        \"latitude\": 50.47612,\n        \"longitude\": 12.370071,\n        \"name\": \"fsn1\",\n        \"network_zone\": \"eu-central\"\n      },\n      \"name\": \"fsn1-dc8\",\n      \"server_types\": {\n        \"available\": [\n          1,\n          2,\n          3\n        ],\n        \"available_for_migration\": [\n          1,\n          2,\n          3\n        ],\n        \"supported\": [\n          1,\n          2,\n          3\n        ]\n      }\n    },\n    \"id\": 42,\n    \"image\": {\n      \"bound_to\": null,\n      \"created\": \"2016-01-30T23:55:00+00:00\",\n      \"created_from\": {\n        \"id\": 1,\n        \"name\": \"Server\"\n      },\n      \"deleted\": null,\n      \"deprecated\": \"2018-02-28T00:00:00+00:00\",\n      \"description\": \"Ubuntu 20.04 Standard 64 bit\",\n      \"disk_size\": 10,\n      \"id\": 42,\n      \"image_size\": 2.3,\n      \"labels\": {},\n      \"name\": \"ubuntu-20.04\",\n      \"os_flavor\": \"ubuntu\",\n      \"os_version\": \"20.04\",\n      \"protection\": {\n        \"delete\": false\n      },\n      \"rapid_deploy\": false,\n      \"status\": \"available\",\n      \"type\": \"snapshot\"\n    },\n    \"included_traffic\": 654321,\n    \"ingoing_traffic\": 123456,\n    \"iso\": {\n      \"deprecated\": \"2018-02-28T00:00:00+00:00\",\n      \"description\": \"FreeBSD 11.0 x64\",\n      \"id\": 42,\n      \"name\": \"FreeBSD-11.0-RELEASE-amd64-dvd1\",\n      \"type\": \"public\"\n    },\n    \"labels\": {},\n    \"load_balancers\": [],\n    \"locked\": false,\n    \"name\": \"my-resource\",\n    \"outgoing_traffic\": 123456,\n    \"placement_group\": {\n      \"created\": \"2016-01-30T23:55:00+00:00\",\n      \"id\": 42,\n      \"labels\": {},\n      \"name\": \"my-resource\",\n      \"servers\": [\n        42\n      ],\n      \"type\": \"spread\"\n    },\n    \"primary_disk_size\": 50,\n    \"private_net\": [\n      {\n        \"alias_ips\": [],\n        \"ip\": \"10.0.0.2\",\n        \"mac_address\": \"86:00:ff:2a:7d:e1\",\n        \"network\": 4711\n      }\n    ],\n    \"protection\": {\n      \"delete\": false,\n      \"rebuild\": false\n    },\n    \"public_net\": {\n      \"firewalls\": [\n        {\n          \"id\": 42,\n          \"status\": \"applied\"\n        }\n      ],\n      \"floating_ips\": [\n        478\n      ],\n      \"ipv4\": {\n        \"blocked\": false,\n        \"dns_ptr\": \"server01.example.com\",\n        \"id\": 42,\n        \"ip\": \"1.2.3.4\"\n      },\n      \"ipv6\": {\n        \"blocked\": false,\n        \"dns_ptr\": [\n          {\n            \"dns_ptr\": \"server.example.com\",\n            \"ip\": \"2001:db8::1\"\n          }\n        ],\n        \"id\": 42,\n        \"ip\": \"2001:db8::/64\"\n      }\n    },\n    \"rescue_enabled\": false,\n    \"server_type\": {\n      \"cores\": 1,\n      \"cpu_type\": \"shared\",\n      \"deprecated\": false,\n      \"description\": \"CX11\",\n      \"disk\": 25,\n      \"id\": 1,\n      \"memory\": 1,\n      \"name\": \"cx11\",\n      \"prices\": [\n        {\n          \"location\": \"fsn1\",\n          \"price_hourly\": {\n            \"gross\": \"1.1900000000000000\",\n            \"net\": \"1.0000000000\"\n          },\n          \"price_monthly\": {\n            \"gross\": \"1.1900000000000000\",\n            \"net\": \"1.0000000000\"\n          }\n        }\n      ],\n      \"storage_type\": \"local\"\n    },\n    \"status\": \"running\",\n    \"volumes\": []\n  }\n}";
        GetByIdResponse? response = JsonConvert.DeserializeObject<GetByIdResponse>(serverResponse);
        response!.Server!.Status = "initializing";

        this._hetznerCloudService
            .Setup(x => x.GetRequest<GetByIdResponse>(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
            .ReturnsAsync(response);

        var server = await _instance.GetByIdAsync(serverId);

        server.Should().NotBeNull();

        server.Status.Should().Be(ServerStatus.Initializing);
    }

    [TestMethod]
    public async Task GetByIdAsync_WithNullResponse_Throws()
    {
        long serverId = 42;

        GetByIdResponse? response = null;

        this._hetznerCloudService
            .Setup(x => x.GetRequest<GetByIdResponse>(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
            .ReturnsAsync(response);

        await _instance.Invoking(x => x.GetByIdAsync(serverId)).Should().ThrowAsync<InvalidResponseException>();
    }

    [TestMethod]
    public async Task GetByIdAsync_WithNullServerResponse_Throws()
    {
        long serverId = 42;

        GetByIdResponse? response = new GetByIdResponse
        {
            Server = null
        };

        this._hetznerCloudService
            .Setup(x => x.GetRequest<GetByIdResponse>(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
            .ReturnsAsync(response);

        await _instance.Invoking(x => x.GetByIdAsync(serverId)).Should().ThrowAsync<ResourceNotFoundException>();
    }
}