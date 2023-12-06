using FluentAssertions;
using FluentDataBuilder;
using FluentDataBuilder.Json;
using Hetzner.Cloud.Mapping;

namespace Hetzner.Cloud.Tests.Mappings;

[TestClass]
public class DatacenterMappingsTests
{
    [TestInitialize]
    public void TestInitialize()
    {
        
    }

    [TestMethod]
    public void ToDatacenter_WithHetznerSampleJson_Returns()
    {
        // Arrange
        var jsonElement = new DataBuilder()
            .LoadFrom("""
                {
                  "description": "Falkenstein 1 DC 8",
                  "id": 1,
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
                }
                """)
            .Build().RootElement;

        // Act
        var datacenter = jsonElement.ToDatacenter();

        // Assert
        datacenter.Should().NotBeNull();
        datacenter!.Id.Should().Be(1);
        datacenter.Name.Should().Be("fsn1-dc8");
        datacenter.Description.Should().Be("Falkenstein 1 DC 8");
    }

    [TestMethod]
    public void ToDatacenter_WithValidJsonElement_Returns()
    {
        // Arrange
        var jsonElement = new DataBuilder()
            .Add("id", 1)
            .Add("name", "DatacenterName")
            .Add("description", "Description")
            .Add("location", new DataBuilder()
                .Add("id", 7)
                .Add("name", "LocationName")
                .Add("description", "Description")
                .Add("city", "City")
                .Add("country", "Country")
                .Add("latitude", 15.4)
                .Add("longitude", 7.3)
                .Add("network_zone", "NetworkZone"))
            .Add("server_types", new DataBuilder()
                .Add("available", Array.Empty<int>())
                .Add("available_for_migration", Array.Empty<int>())
                .Add("supported", Array.Empty<int>()))
            .Build().RootElement;

        // Act
        var datacenter = jsonElement.ToDatacenter();

        // Assert
        datacenter.Should().NotBeNull();
        datacenter!.Id.Should().Be(1);
        datacenter.Name.Should().Be("DatacenterName");
        datacenter.Description.Should().Be("Description");
        datacenter.Location.Should().NotBeNull();
        datacenter.Location!.Id.Should().Be(7);
        datacenter.Location.Name.Should().Be("LocationName");
        datacenter.Location.Description.Should().Be("Description");
        datacenter.Location.City.Should().Be("City");
        datacenter.Location.Country.Should().Be("Country");
        datacenter.Location.Latitude.Should().Be(15.4);
        datacenter.Location.Longitude.Should().Be(7.3);
        datacenter.Location.NetworkZone.Should().Be("NetworkZone");
        datacenter.ServerTypes.Should().NotBeNull();
        datacenter.ServerTypes!.Available.Should().BeEmpty();
        datacenter.ServerTypes.AvailableForMigration.Should().BeEmpty();
        datacenter.ServerTypes.Supported.Should().BeEmpty();
    }

    [TestMethod]
    public void ToDatacenter_WithNullJsonElement_Returns()
    {
        // Arrange
        var jsonElement = new DataBuilder()
            .Add("NullItem", (object?)null)
            .Build().RootElement;

        // Act
        var datacenter = jsonElement.GetProperty("NullItem").ToDatacenter();

        // Assert
        datacenter.Should().BeNull();
    }
}