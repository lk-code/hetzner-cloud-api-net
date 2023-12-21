using System.Net;
using FluentAssertions;
using Hetzner.Cloud.Exceptions;
using Hetzner.Cloud.Interfaces;
using Hetzner.Cloud.Models;
using Hetzner.Cloud.Services;
using NSubstitute;
using RichardSzalay.MockHttp;

namespace Hetzner.Cloud.Tests.Services;

[TestClass]
public class ServerServiceTests
{
    private MockHttpMessageHandler _mockHttp = null!;
    private IHttpClientFactory _httpClientFactory = null!;
    private IServerService _instance = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockHttp = new MockHttpMessageHandler();

        var httpClient = _mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri("https://localhost");

        _httpClientFactory = Substitute.For<IHttpClientFactory>();
        _httpClientFactory.CreateClient(Arg.Any<string>())
            .Returns(httpClient);

        _instance = new ServerService(_httpClientFactory);
    }

    [TestMethod]
    public async Task GetAllAsync_WithHetznerSample_Returns()
    {
        // Arrange
        var jsonContent = """
                          {
                            "meta": {
                              "pagination": {
                                "last_page": 4,
                                "next_page": 4,
                                "page": 3,
                                "per_page": 25,
                                "previous_page": 2,
                                "total_entries": 100
                              }
                            },
                            "servers": [
                              {
                                "backup_window": "22-02",
                                "created": "2016-01-30T23:55:00+00:00",
                                "datacenter": {
                                  "description": "Falkenstein DC Park 8",
                                  "id": 42,
                                  "location": {
                                    "city": "Falkenstein",
                                    "country": "DE",
                                    "description": "Falkenstein DC Park 1",
                                    "id": 1,
                                    "latitude": 50.47612,
                                    "longitude": 12.370071,
                                    "name": "fsn1",
                                    "network_zone": "eu-central"
                                  },
                                  "name": "fsn1-dc8",
                                  "server_types": {
                                    "available": [
                                      1,
                                      2,
                                      3
                                    ],
                                    "available_for_migration": [
                                      1,
                                      2,
                                      3
                                    ],
                                    "supported": [
                                      1,
                                      2,
                                      3
                                    ]
                                  }
                                },
                                "id": 42,
                                "image": {
                                  "architecture": "x86",
                                  "bound_to": null,
                                  "created": "2016-01-30T23:55:00+00:00",
                                  "created_from": {
                                    "id": 1,
                                    "name": "Server"
                                  },
                                  "deleted": null,
                                  "deprecated": "2018-02-28T00:00:00+00:00",
                                  "description": "Ubuntu 20.04 Standard 64 bit",
                                  "disk_size": 10,
                                  "id": 42,
                                  "image_size": 2.3,
                                  "labels": {},
                                  "name": "ubuntu-20.04",
                                  "os_flavor": "ubuntu",
                                  "os_version": "20.04",
                                  "protection": {
                                    "delete": false
                                  },
                                  "rapid_deploy": false,
                                  "status": "available",
                                  "type": "snapshot"
                                },
                                "included_traffic": 654321,
                                "ingoing_traffic": 123456,
                                "iso": {
                                  "architecture": "x86",
                                  "deprecated": "2018-02-28T00:00:00+00:00",
                                  "deprecation": {
                                    "announced": "2023-06-01T00:00:00+00:00",
                                    "unavailable_after": "2023-09-01T00:00:00+00:00"
                                  },
                                  "description": "FreeBSD 11.0 x64",
                                  "id": 42,
                                  "name": "FreeBSD-11.0-RELEASE-amd64-dvd1",
                                  "type": "public"
                                },
                                "labels": {},
                                "load_balancers": [],
                                "locked": false,
                                "name": "my-resource",
                                "outgoing_traffic": 123456,
                                "placement_group": {
                                  "created": "2016-01-30T23:55:00+00:00",
                                  "id": 42,
                                  "labels": {},
                                  "name": "my-resource",
                                  "servers": [
                                    42
                                  ],
                                  "type": "spread"
                                },
                                "primary_disk_size": 50,
                                "private_net": [
                                  {
                                    "alias_ips": [],
                                    "ip": "10.0.0.2",
                                    "mac_address": "86:00:ff:2a:7d:e1",
                                    "network": 4711
                                  }
                                ],
                                "protection": {
                                  "delete": false,
                                  "rebuild": false
                                },
                                "public_net": {
                                  "firewalls": [
                                    {
                                      "id": 42,
                                      "status": "applied"
                                    }
                                  ],
                                  "floating_ips": [
                                    478
                                  ],
                                  "ipv4": {
                                    "blocked": false,
                                    "dns_ptr": "server01.example.com",
                                    "id": 42,
                                    "ip": "1.2.3.4"
                                  },
                                  "ipv6": {
                                    "blocked": false,
                                    "dns_ptr": [
                                      {
                                        "dns_ptr": "server.example.com",
                                        "ip": "2001:db8::1"
                                      }
                                    ],
                                    "id": 42,
                                    "ip": "2001:db8::/64"
                                  }
                                },
                                "rescue_enabled": false,
                                "server_type": {
                                  "cores": 1,
                                  "cpu_type": "shared",
                                  "deprecated": false,
                                  "description": "CX11",
                                  "disk": 25,
                                  "id": 1,
                                  "memory": 1,
                                  "name": "cx11",
                                  "prices": [
                                    {
                                      "location": "fsn1",
                                      "price_hourly": {
                                        "gross": "1.1900000000000000",
                                        "net": "1.0000000000"
                                      },
                                      "price_monthly": {
                                        "gross": "1.1900000000000000",
                                        "net": "1.0000000000"
                                      }
                                    }
                                  ],
                                  "storage_type": "local"
                                },
                                "status": "running",
                                "volumes": []
                              }
                            ]
                          }
                          """;
        _mockHttp.When("https://localhost/v1/servers?page=1&per_page=25")
            .Respond("application/json", jsonContent);

        // Act
        var result = await _instance.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.CurrentPage.Should().Be(3);
        result.ItemsPerPage.Should().Be(25);
        result.TotalItems.Should().Be(100);

        result.Items.Should().NotBeNull();
        result.Items.Should().HaveCount(1);

        result.Items.First().Should().NotBeNull();
        result.Items.First().BackupWindow.Should().Be("22-02");
        result.Items.First().Created.Should().Be(DateTime.Parse("2016-01-30T23:55:00+00:00"));
        result.Items.First().Id.Should().Be(42);
        result.Items.First().IncludedTraffic.Should().Be(654321);
        result.Items.First().IngoingTraffic.Should().Be(123456);
        result.Items.First().Locked.Should().BeFalse();
        result.Items.First().Name.Should().Be("my-resource");
        result.Items.First().OutgoingTraffic.Should().Be(123456);
        result.Items.First().PrimaryDiskSize.Should().Be(50);
        result.Items.First().RescueEnabled.Should().BeFalse();

        result.Items.First().Status.Should().Be(ServerStatus.Running);

        result.Items.First().Datacenter.Should().NotBeNull();
        result.Items.First().Datacenter!.Id.Should().Be(42);
        result.Items.First().Datacenter!.Name.Should().Be("fsn1-dc8");
        result.Items.First().Datacenter!.Description.Should().Be("Falkenstein DC Park 8");
        result.Items.First().Datacenter!.Location.Should().NotBeNull();
        result.Items.First().Datacenter!.Location!.Id.Should().Be(1);
        result.Items.First().Datacenter!.Location!.Name.Should().Be("fsn1");
        result.Items.First().Datacenter!.Location!.Description.Should().Be("Falkenstein DC Park 1");
        result.Items.First().Datacenter!.Location!.City.Should().Be("Falkenstein");
        result.Items.First().Datacenter!.Location!.Country.Should().Be("DE");
        result.Items.First().Datacenter!.Location!.Latitude.Should().Be(50.47612);
        result.Items.First().Datacenter!.Location!.Longitude.Should().Be(12.370071);
        result.Items.First().Datacenter!.Location!.NetworkZone.Should().Be("eu-central");
        result.Items.First().Datacenter!.ServerTypes.Should().NotBeNull();
        result.Items.First().Datacenter!.ServerTypes!.Available.Should().NotBeNull();
        result.Items.First().Datacenter!.ServerTypes!.Available.Should().HaveCount(3);
        result.Items.First().Datacenter!.ServerTypes!.Available.Should().Contain(1);
        result.Items.First().Datacenter!.ServerTypes!.Available.Should().Contain(2);
        result.Items.First().Datacenter!.ServerTypes!.Available.Should().Contain(3);
        result.Items.First().Datacenter!.ServerTypes!.AvailableForMigration.Should().NotBeNull();
        result.Items.First().Datacenter!.ServerTypes!.AvailableForMigration.Should().HaveCount(3);
        result.Items.First().Datacenter!.ServerTypes!.AvailableForMigration.Should().Contain(1);
        result.Items.First().Datacenter!.ServerTypes!.AvailableForMigration.Should().Contain(2);
        result.Items.First().Datacenter!.ServerTypes!.AvailableForMigration.Should().Contain(3);
        result.Items.First().Datacenter!.ServerTypes!.Supported.Should().NotBeNull();
        result.Items.First().Datacenter!.ServerTypes!.Supported.Should().HaveCount(3);
        result.Items.First().Datacenter!.ServerTypes!.Supported.Should().Contain(1);
        result.Items.First().Datacenter!.ServerTypes!.Supported.Should().Contain(2);
        result.Items.First().Datacenter!.ServerTypes!.Supported.Should().Contain(3);

        result.Items.First().Image.Should().NotBeNull();
        result.Items.First().Image!.Id.Should().Be(42);
        result.Items.First().Image!.Name.Should().Be("ubuntu-20.04");
        result.Items.First().Image!.Description.Should().Be("Ubuntu 20.04 Standard 64 bit");
        result.Items.First().Image!.Type.Should().Be(ImageType.Snapshot);
        result.Items.First().Image!.Status.Should().Be(ImageStatus.Available);
        result.Items.First().Image!.ImageSize.Should().Be(2.3);
        result.Items.First().Image!.DiskSize.Should().Be(10);
        result.Items.First().Image!.Created.Should().Be(DateTime.Parse("2016-01-30T23:55:00+00:00"));
        result.Items.First().Image!.CreatedFrom.Should().NotBeNull();
        result.Items.First().Image!.CreatedFrom!.Id.Should().Be(1);
        result.Items.First().Image!.CreatedFrom!.Name.Should().Be("Server");
        result.Items.First().Image!.BoundTo.Should().BeNull();
        result.Items.First().Image!.OsFlavor.Should().Be(OsFlavor.Ubuntu);
        result.Items.First().Image!.OsVersion.Should().Be("20.04");
        result.Items.First().Image!.RapidDeploy.Should().BeFalse();
        result.Items.First().Image!.Protection.Should().NotBeNull();
        result.Items.First().Image!.Protection!.Delete.Should().BeFalse();
        result.Items.First().Image!.Deprecated.Should().Be(DateTime.Parse("2018-02-28T00:00:00+00:00"));
        result.Items.First().Image!.Labels.Should().NotBeNull();
        result.Items.First().Image!.Labels.Should().BeEmpty();
        result.Items.First().Image!.Architecture.Should().Be("x86");

        result.Items.First().IsoImage.Should().NotBeNull();
        result.Items.First().IsoImage!.Architecture.Should().Be(IsoImageArchitecture.X86);
        result.Items.First().IsoImage!.Deprecated.Should().Be(DateTime.Parse("2018-02-28T00:00:00+00:00"));
        result.Items.First().IsoImage!.Deprecation.Should().NotBeNull();
        result.Items.First().IsoImage!.Deprecation!.Announced.Should().Be(DateTime.Parse("2023-06-01T00:00:00+00:00"));
        result.Items.First().IsoImage!.Deprecation!.UnavailableAfter.Should()
            .Be(DateTime.Parse("2023-09-01T00:00:00+00:00"));
        result.Items.First().IsoImage!.Description.Should().Be("FreeBSD 11.0 x64");
        result.Items.First().IsoImage!.Id.Should().Be(42);
        result.Items.First().IsoImage!.Name.Should().Be("FreeBSD-11.0-RELEASE-amd64-dvd1");
        result.Items.First().IsoImage!.Type.Should().Be(IsoImageType.Public);

        result.Items.First().Labels.Should().NotBeNull();
        result.Items.First().Labels.Should().BeEmpty();

        result.Items.First().LoadBalancers.Should().NotBeNull();
        result.Items.First().LoadBalancers.Should().BeEmpty();

        result.Items.First().PlacementGroup.Should().NotBeNull();
        result.Items.First().PlacementGroup!.Id.Should().Be(42);
        result.Items.First().PlacementGroup!.Name.Should().Be("my-resource");
        result.Items.First().PlacementGroup!.Type.Should().Be("spread");
        result.Items.First().PlacementGroup!.ServerIds.Should().NotBeNull();
        result.Items.First().PlacementGroup!.ServerIds.Should().HaveCount(1);
        result.Items.First().PlacementGroup!.ServerIds.Should().Contain(42);

        result.Items.First().PrivateNetworks.Should().NotBeNull();
        result.Items.First().PrivateNetworks.Should().HaveCount(1);
        result.Items.First().PrivateNetworks.First().AliasIps.Should().NotBeNull();
        result.Items.First().PrivateNetworks.First().AliasIps.Should().BeEmpty();
        result.Items.First().PrivateNetworks.First().Ip.Should().Be("10.0.0.2");
        result.Items.First().PrivateNetworks.First().MacAddress.Should().Be("86:00:ff:2a:7d:e1");
        result.Items.First().PrivateNetworks.First().Network.Should().Be(4711);

        result.Items.First().Protection.Should().NotBeNull();
        result.Items.First().Protection!.Delete.Should().BeFalse();
        result.Items.First().Protection!.Rebuild.Should().BeFalse();

        result.Items.First().PublicNetwork.Should().NotBeNull();
        result.Items.First().PublicNetwork.Firewalls.Should().NotBeNull();
        result.Items.First().PublicNetwork.Firewalls.Should().HaveCount(1);
        result.Items.First().PublicNetwork.Firewalls.First().Id.Should().Be(42);
        result.Items.First().PublicNetwork.Firewalls.First().Status.Should().Be(FirewallStatus.Applied);
        result.Items.First().PublicNetwork.FloatingIps.Should().NotBeNull();
        result.Items.First().PublicNetwork.FloatingIps.Should().HaveCount(1);
        result.Items.First().PublicNetwork.FloatingIps.Should().Contain(478);
        result.Items.First().PublicNetwork.Ipv4.Should().NotBeNull();
        result.Items.First().PublicNetwork.Ipv4!.Id.Should().Be(42);
        result.Items.First().PublicNetwork.Ipv4!.Ip.Should().Be("1.2.3.4");
        result.Items.First().PublicNetwork.Ipv4!.Blocked.Should().BeFalse();
        result.Items.First().PublicNetwork.Ipv4!.DnsPointer.Should().Be("server01.example.com");
        result.Items.First().PublicNetwork.Ipv6.Should().NotBeNull();
        result.Items.First().PublicNetwork.Ipv6!.Id.Should().Be(42);
        result.Items.First().PublicNetwork.Ipv6!.Ip.Should().Be("2001:db8::/64");
        result.Items.First().PublicNetwork.Ipv6!.Blocked.Should().BeFalse();
        result.Items.First().PublicNetwork.Ipv6!.DnsPointer.Should().NotBeNull();
        result.Items.First().PublicNetwork.Ipv6!.DnsPointer.Should().HaveCount(1);
        result.Items.First().PublicNetwork.Ipv6!.DnsPointer.First().Ip.Should().Be("2001:db8::1");
        result.Items.First().PublicNetwork.Ipv6!.DnsPointer.First().DnsPointer.Should().Be("server.example.com");

        result.Items.First().Type.Should().NotBeNull();
        result.Items.First().Type.Id.Should().Be(1);
        result.Items.First().Type.Cores.Should().Be(1);
        result.Items.First().Type.CpuType.Should().Be(ServerCpuTypes.Shared);
        result.Items.First().Type.Deprecated.Should().BeFalse();
        result.Items.First().Type.Description.Should().Be("CX11");
        result.Items.First().Type.Disk.Should().Be(25);
        result.Items.First().Type.Memory.Should().Be(1);
        result.Items.First().Type.Name.Should().Be("cx11");
        result.Items.First().Type.StorageType.Should().Be(ServerStorageTypes.Local);
        result.Items.First().Type.Prices.Should().NotBeNull();
        result.Items.First().Type.Prices.Should().HaveCount(1);
        result.Items.First().Type.Prices.First().Location.Should().Be("fsn1");
        result.Items.First().Type.Prices.First().Hourly.Should().NotBeNull();
        result.Items.First().Type.Prices.First().Hourly.Gross.Should().Be("1.1900000000000000");
        result.Items.First().Type.Prices.First().Hourly.Net.Should().Be("1.0000000000");
        result.Items.First().Type.Prices.First().Monthly.Should().NotBeNull();
        result.Items.First().Type.Prices.First().Monthly.Gross.Should().Be("1.1900000000000000");
        result.Items.First().Type.Prices.First().Monthly.Net.Should().Be("1.0000000000");

        result.Items.First().Volumes.Should().NotBeNull();
        result.Items.First().Volumes.Should().BeEmpty();
    }

    [TestMethod]
    public async Task GetAllAsync_WithEmpty_Returns()
    {
        // Arrange
        var jsonContent = """
                          {
                            "meta": {
                              "pagination": {
                                "last_page": 4,
                                "next_page": 4,
                                "page": 3,
                                "per_page": 25,
                                "previous_page": 2,
                                "total_entries": 0
                              }
                            },
                            "servers": [
                            ]
                          }
                          """;
        _mockHttp.When("https://localhost/v1/servers?page=1&per_page=25")
            .Respond("application/json", jsonContent);

        // Act
        var result = await _instance.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.CurrentPage.Should().Be(3);
        result.ItemsPerPage.Should().Be(25);
        result.TotalItems.Should().Be(0);
        result.Items.Should().NotBeNull();
        result.Items.Should().BeEmpty();
    }

    [TestMethod]
    public async Task GetAllAsync_WithSampleServerResponse_Returns()
    {
        // Arrange
        var jsonContent = """
                          {
                            "servers": [
                              {
                                "id": 42,
                                "name": "test",
                                "status": "running",
                                "created": "2022-11-03T19:07:20+00:00",
                                "public_net": {
                                  "ipv4": {
                                    "ip": "1.2.3.4",
                                    "blocked": false,
                                    "dns_ptr": "static.1.2.3.4.clients.your-server.de",
                                    "id": 42
                                  },
                                  "ipv6": {
                                    "ip": "2001:db8::1",
                                    "blocked": false,
                                    "dns_ptr": [],
                                    "id": 42
                                  },
                                  "floating_ips": [],
                                  "firewalls": []
                                },
                                "private_net": [],
                                "server_type": {
                                  "id": 3,
                                  "name": "cx21",
                                  "description": "CX21",
                                  "cores": 2,
                                  "memory": 4.0,
                                  "disk": 40,
                                  "deprecated": false,
                                  "prices": [
                                    {
                                      "location": "nbg1",
                                      "price_hourly": {
                                        "net": "0.0079000000",
                                        "gross": "0.0094010000000000"
                                      },
                                      "price_monthly": {
                                        "net": "4.8500000000",
                                        "gross": "5.7715000000000000"
                                      }
                                    },
                                    {
                                      "location": "fsn1",
                                      "price_hourly": {
                                        "net": "0.0079000000",
                                        "gross": "0.0094010000000000"
                                      },
                                      "price_monthly": {
                                        "net": "4.8500000000",
                                        "gross": "5.7715000000000000"
                                      }
                                    },
                                    {
                                      "location": "hel1",
                                      "price_hourly": {
                                        "net": "0.0079000000",
                                        "gross": "0.0094010000000000"
                                      },
                                      "price_monthly": {
                                        "net": "4.8500000000",
                                        "gross": "5.7715000000000000"
                                      }
                                    }
                                  ],
                                  "storage_type": "local",
                                  "cpu_type": "shared",
                                  "architecture": "x86",
                                  "included_traffic": 21990232555520,
                                  "deprecation": null
                                },
                                "datacenter": {
                                  "id": 4,
                                  "name": "fsn1-dc14",
                                  "description": "Falkenstein 1 virtual DC 14",
                                  "location": {
                                    "id": 1,
                                    "name": "fsn1",
                                    "description": "Falkenstein DC Park 1",
                                    "country": "DE",
                                    "city": "Falkenstein",
                                    "latitude": 50.47612,
                                    "longitude": 12.370071,
                                    "network_zone": "eu-central"
                                  },
                                  "server_types": {
                                    "supported": [
                                      1,
                                      3,
                                      5,
                                      7,
                                      9,
                                      22,
                                      23,
                                      24,
                                      25,
                                      26,
                                      45,
                                      93,
                                      94,
                                      95,
                                      96,
                                      97,
                                      98,
                                      99,
                                      100,
                                      101
                                    ],
                                    "available": [
                                      1,
                                      3,
                                      5,
                                      7,
                                      9,
                                      22,
                                      23,
                                      24,
                                      25,
                                      26,
                                      45,
                                      93,
                                      94,
                                      95,
                                      96,
                                      97,
                                      98,
                                      99,
                                      100,
                                      101
                                    ],
                                    "available_for_migration": [
                                      1,
                                      3,
                                      5,
                                      7,
                                      9,
                                      22,
                                      23,
                                      24,
                                      25,
                                      26,
                                      27,
                                      28,
                                      29,
                                      30,
                                      31,
                                      32,
                                      45,
                                      93,
                                      94,
                                      95,
                                      96,
                                      97,
                                      98,
                                      99,
                                      100,
                                      101,
                                      102,
                                      103
                                    ]
                                  }
                                },
                                "image": {
                                  "id": 67794396,
                                  "type": "system",
                                  "status": "available",
                                  "name": "ubuntu-22.04",
                                  "description": "Ubuntu 22.04",
                                  "image_size": null,
                                  "disk_size": 5,
                                  "created": "2022-04-21T13:32:38+00:00",
                                  "created_from": null,
                                  "bound_to": null,
                                  "os_flavor": "ubuntu",
                                  "os_version": "22.04",
                                  "rapid_deploy": true,
                                  "protection": {
                                    "delete": false
                                  },
                                  "deprecated": null,
                                  "labels": {},
                                  "deleted": null,
                                  "architecture": "x86"
                                },
                                "iso": null,
                                "rescue_enabled": false,
                                "locked": false,
                                "backup_window": null,
                                "outgoing_traffic": 318350000,
                                "ingoing_traffic": 703210000,
                                "included_traffic": 21990232555520,
                                "protection": {
                                  "delete": false,
                                  "rebuild": false
                                },
                                "labels": {
                                  "apps": "lk-code",
                                  "apps/demo": "ein-wert",
                                  "environment": "development"
                                },
                                "volumes": [],
                                "load_balancers": [],
                                "primary_disk_size": 40,
                                "placement_group": null
                              }
                            ],
                            "meta": {
                              "pagination": {
                                "page": 1,
                                "per_page": 25,
                                "previous_page": null,
                                "next_page": null,
                                "last_page": 1,
                                "total_entries": 1
                              }
                            }
                          }
                          """;
        _mockHttp.When("https://localhost/v1/servers?page=1&per_page=25")
            .Respond("application/json", jsonContent);

        // Act
        var result = await _instance.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.CurrentPage.Should().Be(1);
        result.ItemsPerPage.Should().Be(25);
        result.TotalItems.Should().Be(1);

        result.Items.Should().NotBeNull();
        result.Items.Should().HaveCount(1);

        result.Items.First().Should().NotBeNull();
        result.Items.First().BackupWindow.Should().BeNull();
        result.Items.First().Created.Should().Be(DateTime.Parse("2022-11-03T19:07:20+00:00"));
        result.Items.First().Id.Should().Be(42);
        result.Items.First().IncludedTraffic.Should().Be(21990232555520);
        result.Items.First().IngoingTraffic.Should().Be(703210000);
        result.Items.First().Locked.Should().BeFalse();
        result.Items.First().Name.Should().Be("test");
        result.Items.First().OutgoingTraffic.Should().Be(318350000);
        result.Items.First().PrimaryDiskSize.Should().Be(40);
        result.Items.First().RescueEnabled.Should().BeFalse();

        result.Items.First().Status.Should().Be(ServerStatus.Running);

        result.Items.First().Datacenter.Should().NotBeNull();
        result.Items.First().Datacenter!.Id.Should().Be(4);
        result.Items.First().Datacenter!.Name.Should().Be("fsn1-dc14");
        result.Items.First().Datacenter!.Description.Should().Be("Falkenstein 1 virtual DC 14");
        result.Items.First().Datacenter!.Location.Should().NotBeNull();
        result.Items.First().Datacenter!.Location!.Id.Should().Be(1);
        result.Items.First().Datacenter!.Location!.Name.Should().Be("fsn1");
        result.Items.First().Datacenter!.Location!.Description.Should().Be("Falkenstein DC Park 1");
        result.Items.First().Datacenter!.Location!.City.Should().Be("Falkenstein");
        result.Items.First().Datacenter!.Location!.Country.Should().Be("DE");
        result.Items.First().Datacenter!.Location!.Latitude.Should().Be(50.47612);
        result.Items.First().Datacenter!.Location!.Longitude.Should().Be(12.370071);
        result.Items.First().Datacenter!.Location!.NetworkZone.Should().Be("eu-central");
        result.Items.First().Datacenter!.ServerTypes.Should().NotBeNull();
        result.Items.First().Datacenter!.ServerTypes!.Available.Should().NotBeNull();
        result.Items.First().Datacenter!.ServerTypes!.Available.Should().HaveCount(20);
        result.Items.First().Datacenter!.ServerTypes!.AvailableForMigration.Should().NotBeNull();
        result.Items.First().Datacenter!.ServerTypes!.AvailableForMigration.Should().HaveCount(28);
        result.Items.First().Datacenter!.ServerTypes!.Supported.Should().NotBeNull();
        result.Items.First().Datacenter!.ServerTypes!.Supported.Should().HaveCount(20);

        result.Items.First().Image.Should().NotBeNull();
        result.Items.First().Image!.Id.Should().Be(67794396);
        result.Items.First().Image!.Name.Should().Be("ubuntu-22.04");
        result.Items.First().Image!.Description.Should().Be("Ubuntu 22.04");
        result.Items.First().Image!.Type.Should().Be(ImageType.System);
        result.Items.First().Image!.Status.Should().Be(ImageStatus.Available);
        result.Items.First().Image!.ImageSize.Should().BeNull();
        result.Items.First().Image!.DiskSize.Should().Be(5);
        result.Items.First().Image!.Created.Should().Be(DateTime.Parse("2022-04-21T13:32:38+00:00"));
        result.Items.First().Image!.CreatedFrom.Should().BeNull();
        result.Items.First().Image!.BoundTo.Should().BeNull();
        result.Items.First().Image!.OsFlavor.Should().Be(OsFlavor.Ubuntu);
        result.Items.First().Image!.OsVersion.Should().Be("22.04");
        result.Items.First().Image!.RapidDeploy.Should().BeTrue();
        result.Items.First().Image!.Protection.Should().NotBeNull();
        result.Items.First().Image!.Protection!.Delete.Should().BeFalse();
        result.Items.First().Image!.Deprecated.Should().BeNull();
        result.Items.First().Image!.Labels.Should().NotBeNull();
        result.Items.First().Image!.Labels.Should().BeEmpty();
        result.Items.First().Image!.Architecture.Should().Be("x86");

        result.Items.First().IsoImage.Should().BeNull();

        result.Items.First().Labels.Should().NotBeNull();
        result.Items.First().Labels.Should().HaveCount(3);
        result.Items.First().Labels.Should().ContainKey("apps");
        result.Items.First().Labels.Should().ContainKey("apps/demo");
        result.Items.First().Labels.Should().ContainKey("environment");

        result.Items.First().LoadBalancers.Should().NotBeNull();
        result.Items.First().LoadBalancers.Should().BeEmpty();

        result.Items.First().PlacementGroup.Should().BeNull();

        result.Items.First().PrivateNetworks.Should().NotBeNull();
        result.Items.First().PrivateNetworks.Should().BeEmpty();

        result.Items.First().Protection.Should().NotBeNull();
        result.Items.First().Protection!.Delete.Should().BeFalse();
        result.Items.First().Protection!.Rebuild.Should().BeFalse();

        result.Items.First().PublicNetwork.Should().NotBeNull();
        result.Items.First().PublicNetwork.Firewalls.Should().NotBeNull();
        result.Items.First().PublicNetwork.Firewalls.Should().BeEmpty();
        result.Items.First().PublicNetwork.FloatingIps.Should().NotBeNull();
        result.Items.First().PublicNetwork.FloatingIps.Should().BeEmpty();
        result.Items.First().PublicNetwork.Ipv4.Should().NotBeNull();
        result.Items.First().PublicNetwork.Ipv4!.Id.Should().Be(42);
        result.Items.First().PublicNetwork.Ipv4!.Ip.Should().Be("1.2.3.4");
        result.Items.First().PublicNetwork.Ipv4!.Blocked.Should().BeFalse();
        result.Items.First().PublicNetwork.Ipv4!.DnsPointer.Should().Be("static.1.2.3.4.clients.your-server.de");
        result.Items.First().PublicNetwork.Ipv6.Should().NotBeNull();
        result.Items.First().PublicNetwork.Ipv6!.Id.Should().Be(42);
        result.Items.First().PublicNetwork.Ipv6!.Ip.Should().Be("2001:db8::1");
        result.Items.First().PublicNetwork.Ipv6!.Blocked.Should().BeFalse();
        result.Items.First().PublicNetwork.Ipv6!.DnsPointer.Should().NotBeNull();
        result.Items.First().PublicNetwork.Ipv6!.DnsPointer.Should().BeEmpty();

        result.Items.First().Type.Should().NotBeNull();
        result.Items.First().Type.Id.Should().Be(3);
        result.Items.First().Type.Cores.Should().Be(2);
        result.Items.First().Type.CpuType.Should().Be(ServerCpuTypes.Shared);
        result.Items.First().Type.Deprecated.Should().BeFalse();
        result.Items.First().Type.Description.Should().Be("CX21");
        result.Items.First().Type.Disk.Should().Be(40);
        result.Items.First().Type.Memory.Should().Be(4.0);
        result.Items.First().Type.Name.Should().Be("cx21");
        result.Items.First().Type.StorageType.Should().Be(ServerStorageTypes.Local);
        result.Items.First().Type.Prices.Should().NotBeNull();
        result.Items.First().Type.Prices.Should().HaveCount(3);

        result.Items.First().Volumes.Should().NotBeNull();
        result.Items.First().Volumes.Should().BeEmpty();
    }

    [TestMethod]
    public async Task GetAllAsync_WithServerErrorException_Throws()
    {
      // Arrange
      _mockHttp.When("https://localhost/v1/servers")
        .Respond(HttpStatusCode.InternalServerError);

      // Act
      Func<Task> act = async () => await _instance.GetAllAsync();

      // Assert
      await act.Should().ThrowAsync<ApiException>()
        .WithMessage("Invalid Request");
    }

    [TestMethod]
    public async Task GetAllAsync_WithUnauthorizedException_Throws()
    {
      // Arrange
      _mockHttp.When("https://localhost/v1/servers")
        .Respond(HttpStatusCode.Unauthorized);

      // Act
      Func<Task> act = async () => await _instance.GetAllAsync();

      // Assert
      await act.Should().ThrowAsync<UnauthorizedException>();
    }

    [TestMethod]
    public async Task GetAllAsync_WithInvalidPage_Throws()
    {
      // Arrange
      _mockHttp.When("https://localhost/v1/servers")
        .Respond(HttpStatusCode.InternalServerError);

      // Act
      Func<Task> act = async () => await _instance.GetAllAsync(0);

      // Assert
      await act.Should().ThrowAsync<InvalidArgumentException>()
        .WithMessage("invalid page number (0).");
    }

    [TestMethod]
    public async Task GetAllAsync_WithInvalidItemsPerPage_Throws()
    {
      // Arrange
      _mockHttp.When("https://localhost/v1/servers")
        .Respond(HttpStatusCode.InternalServerError);

      // Act
      Func<Task> act = async () => await _instance.GetAllAsync(1, 0);

      // Assert
      await act.Should().ThrowAsync<InvalidArgumentException>()
        .WithMessage("invalid items per page (0).");
    }

    [TestMethod]
    public async Task GetByIdAsync_WithHetznerSample_Returns()
    {
        // Arrange
        var jsonContent = """
                          {
                            "server": {
                              "backup_window": "22-02",
                              "created": "2016-01-30T23:55:00+00:00",
                              "datacenter": {
                                "description": "Falkenstein DC Park 8",
                                "id": 42,
                                "location": {
                                  "city": "Falkenstein",
                                  "country": "DE",
                                  "description": "Falkenstein DC Park 1",
                                  "id": 1,
                                  "latitude": 50.47612,
                                  "longitude": 12.370071,
                                  "name": "fsn1",
                                  "network_zone": "eu-central"
                                },
                                "name": "fsn1-dc8",
                                "server_types": {
                                  "available": [
                                    1,
                                    2,
                                    3
                                  ],
                                  "available_for_migration": [
                                    1,
                                    2,
                                    3
                                  ],
                                  "supported": [
                                    1,
                                    2,
                                    3
                                  ]
                                }
                              },
                              "id": 42,
                              "image": {
                                "architecture": "x86",
                                "bound_to": null,
                                "created": "2016-01-30T23:55:00+00:00",
                                "created_from": {
                                  "id": 1,
                                  "name": "Server"
                                },
                                "deleted": null,
                                "deprecated": "2018-02-28T00:00:00+00:00",
                                "description": "Ubuntu 20.04 Standard 64 bit",
                                "disk_size": 10,
                                "id": 42,
                                "image_size": 2.3,
                                "labels": {},
                                "name": "ubuntu-20.04",
                                "os_flavor": "ubuntu",
                                "os_version": "20.04",
                                "protection": {
                                  "delete": false
                                },
                                "rapid_deploy": false,
                                "status": "available",
                                "type": "snapshot"
                              },
                              "included_traffic": 654321,
                              "ingoing_traffic": 123456,
                              "iso": {
                                "architecture": "x86",
                                "deprecated": "2018-02-28T00:00:00+00:00",
                                "deprecation": {
                                  "announced": "2023-06-01T00:00:00+00:00",
                                  "unavailable_after": "2023-09-01T00:00:00+00:00"
                                },
                                "description": "FreeBSD 11.0 x64",
                                "id": 42,
                                "name": "FreeBSD-11.0-RELEASE-amd64-dvd1",
                                "type": "public"
                              },
                              "labels": {},
                              "load_balancers": [],
                              "locked": false,
                              "name": "my-resource",
                              "outgoing_traffic": 123456,
                              "placement_group": {
                                "created": "2016-01-30T23:55:00+00:00",
                                "id": 42,
                                "labels": {},
                                "name": "my-resource",
                                "servers": [
                                  42
                                ],
                                "type": "spread"
                              },
                              "primary_disk_size": 50,
                              "private_net": [
                                {
                                  "alias_ips": [],
                                  "ip": "10.0.0.2",
                                  "mac_address": "86:00:ff:2a:7d:e1",
                                  "network": 4711
                                }
                              ],
                              "protection": {
                                "delete": false,
                                "rebuild": false
                              },
                              "public_net": {
                                "firewalls": [
                                  {
                                    "id": 42,
                                    "status": "applied"
                                  }
                                ],
                                "floating_ips": [
                                  478
                                ],
                                "ipv4": {
                                  "blocked": false,
                                  "dns_ptr": "server01.example.com",
                                  "id": 42,
                                  "ip": "1.2.3.4"
                                },
                                "ipv6": {
                                  "blocked": false,
                                  "dns_ptr": [
                                    {
                                      "dns_ptr": "server.example.com",
                                      "ip": "2001:db8::1"
                                    }
                                  ],
                                  "id": 42,
                                  "ip": "2001:db8::/64"
                                }
                              },
                              "rescue_enabled": false,
                              "server_type": {
                                "cores": 1,
                                "cpu_type": "shared",
                                "deprecated": false,
                                "description": "CX11",
                                "disk": 25,
                                "id": 1,
                                "memory": 1,
                                "name": "cx11",
                                "prices": [
                                  {
                                    "location": "fsn1",
                                    "price_hourly": {
                                      "gross": "1.1900000000000000",
                                      "net": "1.0000000000"
                                    },
                                    "price_monthly": {
                                      "gross": "1.1900000000000000",
                                      "net": "1.0000000000"
                                    }
                                  }
                                ],
                                "storage_type": "local"
                              },
                              "status": "running",
                              "volumes": []
                            }
                          }
                          """;
        _mockHttp.When("https://localhost/v1/servers/42")
            .Respond("application/json", jsonContent);

        // Act
        var result = await _instance.GetByIdAsync(42);

        // Assert
        result.Should().NotBeNull();
        result.Item.Should().NotBeNull();

        result.Item.BackupWindow.Should().Be("22-02");
        result.Item.Created.Should().Be(DateTime.Parse("2016-01-30T23:55:00+00:00"));
        result.Item.Id.Should().Be(42);
        result.Item.IncludedTraffic.Should().Be(654321);
        result.Item.IngoingTraffic.Should().Be(123456);
        result.Item.Locked.Should().BeFalse();
        result.Item.Name.Should().Be("my-resource");
        result.Item.OutgoingTraffic.Should().Be(123456);
        result.Item.PrimaryDiskSize.Should().Be(50);
        result.Item.RescueEnabled.Should().BeFalse();
        result.Item.Status.Should().Be(ServerStatus.Running);

        result.Item.Datacenter.Should().NotBeNull();
        result.Item.Datacenter!.Id.Should().Be(42);
        result.Item.Datacenter!.Name.Should().Be("fsn1-dc8");
        result.Item.Datacenter!.Description.Should().Be("Falkenstein DC Park 8");
        result.Item.Datacenter!.Location.Should().NotBeNull();
        result.Item.Datacenter!.Location!.Id.Should().Be(1);
        result.Item.Datacenter!.Location!.Name.Should().Be("fsn1");
        result.Item.Datacenter!.Location!.Description.Should().Be("Falkenstein DC Park 1");
        result.Item.Datacenter!.Location!.City.Should().Be("Falkenstein");
        result.Item.Datacenter!.Location!.Country.Should().Be("DE");
        result.Item.Datacenter!.Location!.Latitude.Should().Be(50.47612);
        result.Item.Datacenter!.Location!.Longitude.Should().Be(12.370071);
        result.Item.Datacenter!.Location!.NetworkZone.Should().Be("eu-central");
        result.Item.Datacenter!.ServerTypes.Should().NotBeNull();
        result.Item.Datacenter!.ServerTypes!.Available.Should().NotBeNull();
        result.Item.Datacenter!.ServerTypes!.Available.Should().HaveCount(3);
        result.Item.Datacenter!.ServerTypes!.Available.Should().Contain(1);
        result.Item.Datacenter!.ServerTypes!.Available.Should().Contain(2);
        result.Item.Datacenter!.ServerTypes!.Available.Should().Contain(3);
        result.Item.Datacenter!.ServerTypes!.AvailableForMigration.Should().NotBeNull();
        result.Item.Datacenter!.ServerTypes!.AvailableForMigration.Should().HaveCount(3);
        result.Item.Datacenter!.ServerTypes!.AvailableForMigration.Should().Contain(1);
        result.Item.Datacenter!.ServerTypes!.AvailableForMigration.Should().Contain(2);
        result.Item.Datacenter!.ServerTypes!.AvailableForMigration.Should().Contain(3);
        result.Item.Datacenter!.ServerTypes!.Supported.Should().NotBeNull();
        result.Item.Datacenter!.ServerTypes!.Supported.Should().HaveCount(3);
        result.Item.Datacenter!.ServerTypes!.Supported.Should().Contain(1);
        result.Item.Datacenter!.ServerTypes!.Supported.Should().Contain(2);
        result.Item.Datacenter!.ServerTypes!.Supported.Should().Contain(3);

        result.Item.Image.Should().NotBeNull();
        result.Item.Image!.Id.Should().Be(42);
        result.Item.Image!.Name.Should().Be("ubuntu-20.04");
        result.Item.Image!.Description.Should().Be("Ubuntu 20.04 Standard 64 bit");
        result.Item.Image!.Type.Should().Be(ImageType.Snapshot);
        result.Item.Image!.Status.Should().Be(ImageStatus.Available);
        result.Item.Image!.ImageSize.Should().Be(2.3);
        result.Item.Image!.DiskSize.Should().Be(10);
        result.Item.Image!.Created.Should().Be(DateTime.Parse("2016-01-30T23:55:00+00:00"));
        result.Item.Image!.CreatedFrom.Should().NotBeNull();
        result.Item.Image!.CreatedFrom!.Id.Should().Be(1);
        result.Item.Image!.CreatedFrom!.Name.Should().Be("Server");
        result.Item.Image!.BoundTo.Should().BeNull();
        result.Item.Image!.OsFlavor.Should().Be(OsFlavor.Ubuntu);
        result.Item.Image!.OsVersion.Should().Be("20.04");
        result.Item.Image!.RapidDeploy.Should().BeFalse();
        result.Item.Image!.Protection.Should().NotBeNull();
        result.Item.Image!.Protection!.Delete.Should().BeFalse();
        result.Item.Image!.Deprecated.Should().Be(DateTime.Parse("2018-02-28T00:00:00+00:00"));
        result.Item.Image!.Labels.Should().NotBeNull();
        result.Item.Image!.Labels.Should().BeEmpty();
        result.Item.Image!.Architecture.Should().Be("x86");

        result.Item.IsoImage.Should().NotBeNull();
        result.Item.IsoImage!.Architecture.Should().Be(IsoImageArchitecture.X86);
        result.Item.IsoImage!.Deprecated.Should().Be(DateTime.Parse("2018-02-28T00:00:00+00:00"));
        result.Item.IsoImage!.Deprecation.Should().NotBeNull();
        result.Item.IsoImage!.Deprecation!.Announced.Should().Be(DateTime.Parse("2023-06-01T00:00:00+00:00"));
        result.Item.IsoImage!.Deprecation!.UnavailableAfter.Should().Be(DateTime.Parse("2023-09-01T00:00:00+00:00"));
        result.Item.IsoImage!.Description.Should().Be("FreeBSD 11.0 x64");
        result.Item.IsoImage!.Id.Should().Be(42);
        result.Item.IsoImage!.Name.Should().Be("FreeBSD-11.0-RELEASE-amd64-dvd1");
        result.Item.IsoImage!.Type.Should().Be(IsoImageType.Public);

        result.Item.Labels.Should().NotBeNull();
        result.Item.Labels.Should().BeEmpty();

        result.Item.LoadBalancers.Should().NotBeNull();
        result.Item.LoadBalancers.Should().BeEmpty();

        result.Item.PlacementGroup.Should().NotBeNull();
        result.Item.PlacementGroup!.Id.Should().Be(42);
        result.Item.PlacementGroup!.Name.Should().Be("my-resource");
        result.Item.PlacementGroup!.Type.Should().Be("spread");
        result.Item.PlacementGroup!.ServerIds.Should().NotBeNull();
        result.Item.PlacementGroup!.ServerIds.Should().HaveCount(1);
        result.Item.PlacementGroup!.ServerIds.Should().Contain(42);

        result.Item.PrivateNetworks.Should().NotBeNull();
        result.Item.PrivateNetworks.Should().HaveCount(1);
        result.Item.PrivateNetworks.First().AliasIps.Should().NotBeNull();
        result.Item.PrivateNetworks.First().AliasIps.Should().BeEmpty();
        result.Item.PrivateNetworks.First().Ip.Should().Be("10.0.0.2");
        result.Item.PrivateNetworks.First().MacAddress.Should().Be("86:00:ff:2a:7d:e1");
        result.Item.PrivateNetworks.First().Network.Should().Be(4711);

        result.Item.Protection.Should().NotBeNull();
        result.Item.Protection!.Delete.Should().BeFalse();
        result.Item.Protection!.Rebuild.Should().BeFalse();

        result.Item.PublicNetwork.Should().NotBeNull();
        result.Item.PublicNetwork.Firewalls.Should().NotBeNull();
        result.Item.PublicNetwork.Firewalls.Should().HaveCount(1);
        result.Item.PublicNetwork.Firewalls.First().Id.Should().Be(42);
        result.Item.PublicNetwork.Firewalls.First().Status.Should().Be(FirewallStatus.Applied);
        result.Item.PublicNetwork.FloatingIps.Should().NotBeNull();
        result.Item.PublicNetwork.FloatingIps.Should().HaveCount(1);
        result.Item.PublicNetwork.FloatingIps.Should().Contain(478);
        result.Item.PublicNetwork.Ipv4.Should().NotBeNull();
        result.Item.PublicNetwork.Ipv4!.Id.Should().Be(42);
        result.Item.PublicNetwork.Ipv4!.Ip.Should().Be("1.2.3.4");
        result.Item.PublicNetwork.Ipv4!.Blocked.Should().BeFalse();
        result.Item.PublicNetwork.Ipv4!.DnsPointer.Should().Be("server01.example.com");
        result.Item.PublicNetwork.Ipv6.Should().NotBeNull();
        result.Item.PublicNetwork.Ipv6!.Id.Should().Be(42);
        result.Item.PublicNetwork.Ipv6!.Ip.Should().Be("2001:db8::/64");
        result.Item.PublicNetwork.Ipv6!.Blocked.Should().BeFalse();
        result.Item.PublicNetwork.Ipv6!.DnsPointer.Should().NotBeNull();
        result.Item.PublicNetwork.Ipv6!.DnsPointer.Should().HaveCount(1);
        result.Item.PublicNetwork.Ipv6!.DnsPointer.First().Ip.Should().Be("2001:db8::1");
        result.Item.PublicNetwork.Ipv6!.DnsPointer.First().DnsPointer.Should().Be("server.example.com");

        result.Item.Type.Should().NotBeNull();
        result.Item.Type.Id.Should().Be(1);
        result.Item.Type.Cores.Should().Be(1);
        result.Item.Type.CpuType.Should().Be(ServerCpuTypes.Shared);
        result.Item.Type.Deprecated.Should().BeFalse();
        result.Item.Type.Description.Should().Be("CX11");
        result.Item.Type.Disk.Should().Be(25);
        result.Item.Type.Memory.Should().Be(1);
        result.Item.Type.Name.Should().Be("cx11");
        result.Item.Type.StorageType.Should().Be(ServerStorageTypes.Local);
        result.Item.Type.Prices.Should().NotBeNull();
        result.Item.Type.Prices.Should().HaveCount(1);
        result.Item.Type.Prices.First().Location.Should().Be("fsn1");
        result.Item.Type.Prices.First().Hourly.Should().NotBeNull();
        result.Item.Type.Prices.First().Hourly.Gross.Should().Be("1.1900000000000000");
        result.Item.Type.Prices.First().Hourly.Net.Should().Be("1.0000000000");
        result.Item.Type.Prices.First().Monthly.Should().NotBeNull();
        result.Item.Type.Prices.First().Monthly.Gross.Should().Be("1.1900000000000000");
        result.Item.Type.Prices.First().Monthly.Net.Should().Be("1.0000000000");

        result.Item.Volumes.Should().NotBeNull();
        result.Item.Volumes.Should().BeEmpty();
    }

    [TestMethod]
    public async Task GetByIdAsync_WithNotFoundException_Throws()
    {
        // Arrange
        _mockHttp.When("https://localhost/v1/servers/41")
            .Respond(HttpStatusCode.NotFound);

        // Act
        Func<Task> act = async () => await _instance.GetByIdAsync(41);

        // Assert
        await act.Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage("The requested resource was not found");
    }

    [TestMethod]
    public async Task GetByIdAsync_WithServerErrorException_Throws()
    {
        // Arrange
        _mockHttp.When("https://localhost/v1/servers/41")
            .Respond(HttpStatusCode.InternalServerError);

        // Act
        Func<Task> act = async () => await _instance.GetByIdAsync(41);

        // Assert
        await act.Should().ThrowAsync<ApiException>()
            .WithMessage("Invalid Request");
    }
}