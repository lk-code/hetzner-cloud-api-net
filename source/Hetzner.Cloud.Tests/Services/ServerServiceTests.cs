using FluentAssertions;
using Hetzner.Cloud.Interfaces;
using Hetzner.Cloud.Models;
using Hetzner.Cloud.Services;
using NSubstitute;
using RichardSzalay.MockHttp;

namespace Hetzner.Cloud.Tests.Services;

[TestClass]
public class ServerServiceTests
{
    private MockHttpMessageHandler _mockHttp;
    private IHttpClientFactory _httpClientFactory;
    private IServerService _serverService;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockHttp = new MockHttpMessageHandler();

        var httpClient = _mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri("https://localhost");
        
        _httpClientFactory = Substitute.For<IHttpClientFactory>();
        _httpClientFactory.CreateClient(Arg.Any<string>())
            .Returns(httpClient);
        
        _serverService = new ServerService(_httpClientFactory);
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
        var result = await _serverService.GetAllAsync();

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
        
        result.Items.First().Iso.Should().NotBeNull();
        result.Items.First().Iso!.Architecture.Should().Be(IsoImageArchitecture.x86);
        result.Items.First().Iso!.Deprecated.Should().Be(DateTime.Parse("2018-02-28T00:00:00+00:00"));
        result.Items.First().Iso!.Deprecation.Should().NotBeNull();
        result.Items.First().Iso!.Deprecation!.Announced.Should().Be(DateTime.Parse("2023-06-01T00:00:00+00:00"));
        result.Items.First().Iso!.Deprecation!.UnavailableAfter.Should().Be(DateTime.Parse("2023-09-01T00:00:00+00:00"));
        result.Items.First().Iso!.Description.Should().Be("FreeBSD 11.0 x64");
        result.Items.First().Iso!.Id.Should().Be(42);
        result.Items.First().Iso!.Name.Should().Be("FreeBSD-11.0-RELEASE-amd64-dvd1");
        result.Items.First().Iso!.Type.Should().Be(IsoImageType.Public);
        
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
        
        // result.Items.First().PublicNet;
        
        // result.Items.First().Type;
        
        // result.Items.First().Volumes;
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
        var result = await _serverService.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.CurrentPage.Should().Be(3);
        result.ItemsPerPage.Should().Be(25);
        result.TotalItems.Should().Be(0);
        result.Items.Should().NotBeNull();
        result.Items.Should().BeEmpty();
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
        var result = await _serverService.GetByIdAsync(42);

        // Assert
        result.Should().NotBeNull();
        result.Item.Should().NotBeNull();
        
        result.Item!.BackupWindow.Should().Be("22-02");
        result.Item!.Created.Should().Be(DateTime.Parse("2016-01-30T23:55:00+00:00"));
        result.Item!.Id.Should().Be(42);
        result.Item!.IncludedTraffic.Should().Be(654321);
        result.Item!.IngoingTraffic.Should().Be(123456);
        result.Item!.Locked.Should().BeFalse();
        result.Item!.Name.Should().Be("my-resource");
        result.Item!.OutgoingTraffic.Should().Be(123456);
        result.Item!.PrimaryDiskSize.Should().Be(50);
        result.Item!.RescueEnabled.Should().BeFalse();
        result.Item!.Status.Should().Be(ServerStatus.Running);
        
        result.Item!.Datacenter.Should().NotBeNull();
        result.Item!.Datacenter!.Id.Should().Be(42);
        result.Item!.Datacenter!.Name.Should().Be("fsn1-dc8");
        result.Item!.Datacenter!.Description.Should().Be("Falkenstein DC Park 8");
        result.Item!.Datacenter!.Location.Should().NotBeNull();
        result.Item!.Datacenter!.Location!.Id.Should().Be(1);
        result.Item!.Datacenter!.Location!.Name.Should().Be("fsn1");
        result.Item!.Datacenter!.Location!.Description.Should().Be("Falkenstein DC Park 1");
        result.Item!.Datacenter!.Location!.City.Should().Be("Falkenstein");
        result.Item!.Datacenter!.Location!.Country.Should().Be("DE");
        result.Item!.Datacenter!.Location!.Latitude.Should().Be(50.47612);
        result.Item!.Datacenter!.Location!.Longitude.Should().Be(12.370071);
        result.Item!.Datacenter!.Location!.NetworkZone.Should().Be("eu-central");
        result.Item!.Datacenter!.ServerTypes.Should().NotBeNull();
        result.Item!.Datacenter!.ServerTypes!.Available.Should().NotBeNull();
        result.Item!.Datacenter!.ServerTypes!.Available.Should().HaveCount(3);
        result.Item!.Datacenter!.ServerTypes!.Available.Should().Contain(1);
        result.Item!.Datacenter!.ServerTypes!.Available.Should().Contain(2);
        result.Item!.Datacenter!.ServerTypes!.Available.Should().Contain(3);
        result.Item!.Datacenter!.ServerTypes!.AvailableForMigration.Should().NotBeNull();
        result.Item!.Datacenter!.ServerTypes!.AvailableForMigration.Should().HaveCount(3);
        result.Item!.Datacenter!.ServerTypes!.AvailableForMigration.Should().Contain(1);
        result.Item!.Datacenter!.ServerTypes!.AvailableForMigration.Should().Contain(2);
        result.Item!.Datacenter!.ServerTypes!.AvailableForMigration.Should().Contain(3);
        result.Item!.Datacenter!.ServerTypes!.Supported.Should().NotBeNull();
        result.Item!.Datacenter!.ServerTypes!.Supported.Should().HaveCount(3);
        result.Item!.Datacenter!.ServerTypes!.Supported.Should().Contain(1);
        result.Item!.Datacenter!.ServerTypes!.Supported.Should().Contain(2);
        result.Item!.Datacenter!.ServerTypes!.Supported.Should().Contain(3);
        
        result.Item!.Image.Should().NotBeNull();
        result.Item!.Image!.Id.Should().Be(42);
        result.Item!.Image!.Name.Should().Be("ubuntu-20.04");
        result.Item!.Image!.Description.Should().Be("Ubuntu 20.04 Standard 64 bit");
        result.Item!.Image!.Type.Should().Be(ImageType.Snapshot);
        result.Item!.Image!.Status.Should().Be(ImageStatus.Available);
        result.Item!.Image!.ImageSize.Should().Be(2.3);
        result.Item!.Image!.DiskSize.Should().Be(10);
        result.Item!.Image!.Created.Should().Be(DateTime.Parse("2016-01-30T23:55:00+00:00"));
        result.Item!.Image!.CreatedFrom.Should().NotBeNull();
        result.Item!.Image!.CreatedFrom!.Id.Should().Be(1);
        result.Item!.Image!.CreatedFrom!.Name.Should().Be("Server");
        result.Item!.Image!.BoundTo.Should().BeNull();
        result.Item!.Image!.OsFlavor.Should().Be(OsFlavor.Ubuntu);
        result.Item!.Image!.OsVersion.Should().Be("20.04");
        result.Item!.Image!.RapidDeploy.Should().BeFalse();
        result.Item!.Image!.Protection.Should().NotBeNull();
        result.Item!.Image!.Protection!.Delete.Should().BeFalse();
        result.Item!.Image!.Deprecated.Should().Be(DateTime.Parse("2018-02-28T00:00:00+00:00"));
        result.Item!.Image!.Labels.Should().NotBeNull();
        result.Item!.Image!.Labels.Should().BeEmpty();
        result.Item!.Image!.Architecture.Should().Be("x86");
        
        result.Item!.Iso.Should().NotBeNull();
        result.Item!.Iso!.Architecture.Should().Be(IsoImageArchitecture.x86);
        result.Item!.Iso!.Deprecated.Should().Be(DateTime.Parse("2018-02-28T00:00:00+00:00"));
        result.Item!.Iso!.Deprecation.Should().NotBeNull();
        result.Item!.Iso!.Deprecation!.Announced.Should().Be(DateTime.Parse("2023-06-01T00:00:00+00:00"));
        result.Item!.Iso!.Deprecation!.UnavailableAfter.Should().Be(DateTime.Parse("2023-09-01T00:00:00+00:00"));
        result.Item!.Iso!.Description.Should().Be("FreeBSD 11.0 x64");
        result.Item!.Iso!.Id.Should().Be(42);
        result.Item!.Iso!.Name.Should().Be("FreeBSD-11.0-RELEASE-amd64-dvd1");
        result.Item!.Iso!.Type.Should().Be(IsoImageType.Public);
        
        result.Item!.Labels.Should().NotBeNull();
        result.Item!.Labels.Should().BeEmpty();
        
        result.Item!.LoadBalancers.Should().NotBeNull();
        result.Item!.LoadBalancers.Should().BeEmpty();
        
        result.Item!.PlacementGroup.Should().NotBeNull();
        result.Item!.PlacementGroup!.Id.Should().Be(42);
        result.Item!.PlacementGroup!.Name.Should().Be("my-resource");
        result.Item!.PlacementGroup!.Type.Should().Be("spread");
        result.Item!.PlacementGroup!.ServerIds.Should().NotBeNull();
        result.Item!.PlacementGroup!.ServerIds.Should().HaveCount(1);
        result.Item!.PlacementGroup!.ServerIds.Should().Contain(42);
        
        result.Item!.PrivateNetworks.Should().NotBeNull();
        result.Item!.PrivateNetworks.Should().HaveCount(1);
        result.Item!.PrivateNetworks.First().AliasIps.Should().NotBeNull();
        result.Item!.PrivateNetworks.First().AliasIps.Should().BeEmpty();
        result.Item!.PrivateNetworks.First().Ip.Should().Be("10.0.0.2");
        result.Item!.PrivateNetworks.First().MacAddress.Should().Be("86:00:ff:2a:7d:e1");
        result.Item!.PrivateNetworks.First().Network.Should().Be(4711);
        
        result.Item!.Protection.Should().NotBeNull();
        result.Item!.Protection!.Delete.Should().BeFalse();
        result.Item!.Protection!.Rebuild.Should().BeFalse();
        
        // result.Item!.PublicNet;
        
        // result.Item!.Type;
        
        // result.Item!.Volumes;
    }
}