using FluentAssertions;
using FluentDataBuilder;
using FluentDataBuilder.Json;
using Hetzner.Cloud.Mapping;

namespace Hetzner.Cloud.Tests.Mappings;

[TestClass]
public class LocationMappingsTests
{
    [TestInitialize]
    public void TestInitialize()
    {
        
    }

    [TestMethod]
    public void ToLocation_WithHetznerSampleJson_Returns()
    {
        // Arrange
        var jsonElement = new DataBuilder()
            .LoadFrom("""
                {
                  "city": "Falkenstein",
                  "country": "DE",
                  "description": "Falkenstein DC Park 1",
                  "id": 1,
                  "latitude": 50.47612,
                  "longitude": 12.370071,
                  "name": "fsn1",
                  "network_zone": "eu-central"
                }
                """)
            .Build().RootElement;

        // Act
        var location = jsonElement.ToLocation();

        // Assert
        location.Should().NotBeNull();
        location!.City.Should().Be("Falkenstein");
        location.Country.Should().Be("DE");
        location.Description.Should().Be("Falkenstein DC Park 1");
        location.Id.Should().Be(1);
        location.Latitude.Should().Be(50.47612);
        location.Longitude.Should().Be(12.370071);
        location.Name.Should().Be("fsn1");
        location.NetworkZone.Should().Be("eu-central");
    }

    [TestMethod]
    public void ToLocation_WithValidJsonElement_Returns()
    {
        // Arrange
        var jsonElement = new DataBuilder()
            .Add("id", 1)
            .Add("name", "LocationName")
            .Add("description", "LocationDescription")
            .Add("city", "LocationCity")
            .Add("country", "LocationCountry")
            .Add("latitude", 12.3456789)
            .Add("longitude", 98.7654321)
            .Add("network_zone", "LocationNetworkZone")
            .Build().RootElement;

        // Act
        var location = jsonElement.ToLocation();

        // Assert
        location.Should().NotBeNull();
        location!.Id.Should().Be(1);
        location.Name.Should().Be("LocationName");
        location.Description.Should().Be("LocationDescription");
        location.City.Should().Be("LocationCity");
        location.Country.Should().Be("LocationCountry");
        location.Latitude.Should().Be(12.3456789);
        location.Longitude.Should().Be(98.7654321);
        location.NetworkZone.Should().Be("LocationNetworkZone");
    }

    [TestMethod]
    public void ToLocation_WithNullJsonElement_Returns()
    {
        // Arrange
        var jsonElement = new DataBuilder()
            .Add("NullItem", (object?)null)
            .Build().RootElement;

        // Act
        var location = jsonElement.GetProperty("NullItem").ToLocation();

        // Assert
        location.Should().BeNull();
    }
}