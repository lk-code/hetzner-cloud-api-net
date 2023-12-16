using FluentAssertions;
using FluentDataBuilder;
using FluentDataBuilder.Json;
using Hetzner.Cloud.Mapping;

namespace Hetzner.Cloud.Tests.Mappings;

[TestClass]
public class ServerTypesMappingsTests
{
    [TestInitialize]
    public void TestInitialize()
    {
        
    }

    [TestMethod]
    public void ToServerTypes_WithHetznerSampleJson_Returns()
    {
        // Arrange
        var jsonElement = new DataBuilder()
            .LoadFrom("""
                {
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
                """)
            .Build().RootElement;

        // Act
        var serverTypes = jsonElement.ToServerTypes();

        // Assert
        serverTypes.Should().NotBeNull();
        serverTypes!.Available.Should().BeEquivalentTo(new long[] { 1, 2, 3 });
        serverTypes.AvailableForMigration.Should().BeEquivalentTo(new long[] { 1, 2, 3 });
        serverTypes.Supported.Should().BeEquivalentTo(new long[] { 1, 2, 3 });
    }

    [TestMethod]
    public void ToServerTypes_WithValidJsonElement_Returns()
    {
        // Arrange
        var jsonElement = new DataBuilder()
            .Add("available", new long[] { 1, 2, 3 })
            .Add("available_for_migration", new long[] { 4, 5, 6 })
            .Add("supported", new long[] { 7, 8, 9 })
            .Build().RootElement;

        // Act
        var serverTypes = jsonElement.ToServerTypes();

        // Assert
        serverTypes.Should().NotBeNull();
        serverTypes!.Available.Should().BeEquivalentTo(new long[] { 1, 2, 3 });
        serverTypes.AvailableForMigration.Should().BeEquivalentTo(new long[] { 4, 5, 6 });
        serverTypes.Supported.Should().BeEquivalentTo(new long[] { 7, 8, 9 });
    }

    [TestMethod]
    public void ToServerTypes_WithNullJsonElement_Returns()
    {
        // Arrange
        var jsonElement = new DataBuilder()
            .Add("NullItem", (object?)null)
            .Build().RootElement;

        // Act
        var serverTypes = jsonElement.GetProperty("NullItem").ToServerTypes();

        // Assert
        serverTypes.Should().BeNull();
    }
}