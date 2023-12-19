using FluentAssertions;
using FluentDataBuilder;
using FluentDataBuilder.Json;
using Hetzner.Cloud.Mapping;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Tests.Mappings;

[TestClass]
public class ImageMappingsTests
{
    [TestInitialize]
    public void TestInitialize()
    {
        
    }

    [TestMethod]
    public void ToImage_WithHetznerSampleJson_Returns()
    {
        // Arrange
        var jsonElement = new DataBuilder()
            .LoadFrom("""
                {
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
                }
                """)
            .Build().RootElement;

        // Act
        var image = jsonElement.ToImage();

        // Assert
        image.Should().NotBeNull();
        image!.Architecture.Should().Be("x86");
        image.BoundTo.Should().BeNull();
        image.Created.Should().Be(DateTime.Parse("2016-01-30T23:55:00+00:00"));
        image.CreatedFrom.Should().NotBeNull();
        image.CreatedFrom!.Id.Should().Be(1);
        image.CreatedFrom.Name.Should().Be("Server");
        image.Deleted.Should().BeNull();
        image.Deprecated.Should().Be(DateTime.Parse("2018-02-28T00:00:00+00:00"));
        image.Description.Should().Be("Ubuntu 20.04 Standard 64 bit");
        image.DiskSize.Should().Be(10);
        image.Id.Should().Be(42);
        image.ImageSize.Should().Be(2.3);
        image.Labels.Should().BeEmpty();
        image.Name.Should().Be("ubuntu-20.04");
        image.OsFlavor.Should().Be(OsFlavor.Ubuntu);
        image.OsVersion.Should().Be("20.04");
        image.Protection.Should().NotBeNull();
        image.Protection!.Delete.Should().BeFalse();
        image.RapidDeploy.Should().BeFalse();
        image.Status.Should().Be(ImageStatus.Available);
        image.Type.Should().Be(ImageType.Snapshot);
        
    }

    [TestMethod]
    public void ToImage_WithValidJsonElement_Returns()
    {
        // Arrange
        var jsonElement = new DataBuilder()
            .Add("id", 42)
            .Add("architecture", "x86")
            .Add("created", "2016-01-30T23:55:00+00:00")
            .Add("created_from", new DataBuilder()
                .Add("id", 1)
                .Add("name", "Server")
                .Build())
            .Add("description", "Ubuntu 20.04 Standard 64 bit")
            .Add("disk_size", 10)
            .Add("labels", new DataBuilder())
            .Add("os_flavor", "ubuntu")
            .Add("protection", new DataBuilder()
                .Add("delete", false))
            .Add("rapid_deploy", false)
            .Add("status", "available")
            .Add("type", "snapshot")
            .Build().RootElement;

        // Act
        var image = jsonElement.ToImage();

        // Assert
        image.Should().NotBeNull();
        image!.Architecture.Should().Be("x86");
        image.BoundTo.Should().BeNull();
        image.Created.Should().Be(DateTime.Parse("2016-01-30T23:55:00+00:00"));
        image.CreatedFrom.Should().NotBeNull();
        image.CreatedFrom!.Id.Should().Be(1);
        image.CreatedFrom.Name.Should().Be("Server");
        image.Deleted.Should().BeNull();
        image.Deprecated.Should().BeNull();
        image.Description.Should().Be("Ubuntu 20.04 Standard 64 bit");
        image.DiskSize.Should().Be(10);
        image.Id.Should().Be(42);
        image.ImageSize.Should().BeNull();
        image.Labels.Should().BeEmpty();
        image.Name.Should().BeNull();
        image.OsFlavor.Should().Be(OsFlavor.Ubuntu);
        image.OsVersion.Should().BeNull();
        image.Protection.Should().NotBeNull();
        image.Protection!.Delete.Should().BeFalse();
        image.RapidDeploy.Should().BeFalse();
        image.Status.Should().Be(ImageStatus.Available);
        image.Type.Should().Be(ImageType.Snapshot);
    }

    [TestMethod]
    public void ToImage_WithNullJsonElement_Returns()
    {
        // Arrange
        var jsonElement = new DataBuilder()
            .Add("NullItem", (object?)null)
            .Build().RootElement;

        // Act
        var image = jsonElement.GetProperty("NullItem").ToImage();

        // Assert
        image.Should().BeNull();
    }
}