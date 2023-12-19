using FluentAssertions;
using FluentDataBuilder;
using FluentDataBuilder.Json;
using Hetzner.Cloud.Mapping;

namespace Hetzner.Cloud.Tests.Mappings;

[TestClass]
public class PlacementGroupMappingsTests
{
    [TestInitialize]
    public void TestInitialize()
    {
        
    }

    [TestMethod]
    public void ToPlacementGroup_WithHetznerSampleJson_Returns()
    {
        // Arrange
        var jsonElement = new DataBuilder()
            .LoadFrom(@"
                {
                    ""created"": ""2016-01-30T23:55:00+00:00"",
                    ""id"": 42,
                    ""labels"": {},
                    ""name"": ""my-resource"",
                    ""servers"": [42],
                    ""type"": ""spread""
                }
                ")
            .Build().RootElement;

        // Act
        var placementGroup = jsonElement.ToPlacementGroup();

        // Assert
        placementGroup.Should().NotBeNull();
        placementGroup!.Id.Should().Be(42);
        placementGroup.Name.Should().Be("my-resource");
        placementGroup.Created.Should().Be(DateTime.Parse("2016-01-30T23:55:00+00:00"));
        placementGroup.Labels.Should().BeEmpty();
        placementGroup.Type.Should().Be("spread");
        placementGroup.ServerIds.Should().BeEquivalentTo(new long[] { 42 });
    }

    [TestMethod]
    public void ToPlacementGroup_WithValidJsonElement_Returns()
    {
        // Arrange
        var jsonElement = new DataBuilder()
            .Add("id", 1)
            .Add("name", "TestGroup")
            .Add("created", "2023-01-01T12:00:00")
            .Add("labels", new DataBuilder().Build().RootElement)
            .Add("type", "TypeA")
            .Build().RootElement;

        // Act
        var placementGroup = jsonElement.ToPlacementGroup();

        // Assert
        placementGroup.Should().NotBeNull();
        placementGroup!.Id.Should().Be(1);
        placementGroup.Name.Should().Be("TestGroup");
        placementGroup.Created.Should().Be(new DateTime(2023, 1, 1, 12, 0, 0));
        placementGroup.Labels.Should().BeEmpty();
        placementGroup.Type.Should().Be("TypeA");
        placementGroup.ServerIds.Should().BeEmpty();
    }

    [TestMethod]
    public void ToPlacementGroup_WithNullJsonElement_Returns()
    {
        // Arrange
        var jsonElement = new DataBuilder()
            .Add("NullItem", (object?)null)
            .Build().RootElement;

        // Act
        var placementGroup = jsonElement.GetProperty("NullItem").ToPlacementGroup();

        // Assert
        placementGroup.Should().BeNull();
    }
}