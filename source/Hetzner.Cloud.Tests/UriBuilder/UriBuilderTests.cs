using FluentAssertions;
using Hetzner.Cloud.Interfaces;
using Hetzner.Cloud.Sorting;
using Hetzner.Cloud.Tests.TestCore.Sorting;
using Hetzner.Cloud.UriBuilder;

namespace Hetzner.Cloud.Tests.UriBuilder;

[TestClass]
public class UriBuilderTests
{
    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestMethod]
    [DataRow("/api/v1/address")]
    [DataRow("api/v1/address")]
    public void AsUriBuilder_WithValidAddress_Returns(string address)
    {
        // Arrange
        
        // Act
        var uriBuilder = address.AsUriBuilder();
        var uri = uriBuilder.ToUri();

        // Assert
        uriBuilder.Should().NotBeNull();
        uri.Should().NotBeNull();
        uri[0].Should().Be('/');
    }

    [TestMethod]
    [DataRow("")]
    public void AsUriBuilder_WithInvalidAddress_Throws(string address)
    {
        // Arrange
        
        // Act
        var uriBuilder = address.AsUriBuilder();

        // Assert
        Action act = () => uriBuilder.ToUri();
        act.Should().Throw<ArgumentException>();
    }

    [TestMethod]
    public void AddSorting_WithValid_Returns()
    {
        // Arrange
        var address = "/api/v1/address";
        
        // Act
        var uriBuilder = address.AsUriBuilder();
        uriBuilder.AddSorting(new Sorting<TestSorting>(TestSorting.Name, SortingDirection.Desc));

        // Assert
        var uri = uriBuilder.ToUri();
        uri.Should().Be("/api/v1/address?sort=name:desc");
    }

    [TestMethod]
    public void AddFilter_WithValid_Returns()
    {
        // Arrange
        var address = "/api/v1/address";
        
        // Act
        var uriBuilder = address.AsUriBuilder();
        uriBuilder.AddFilter(new List<IFilter>
        {
            new TestFilter("test", "test-value")
        });

        // Assert
        var uri = uriBuilder.ToUri();
        uri.Should().Be("/api/v1/address?test=test-value");
    }

    [TestMethod]
    public void AddFilter_Without_Returns()
    {
        // Arrange
        var address = "/api/v1/address";
        
        // Act
        var uriBuilder = address.AsUriBuilder();
        uriBuilder.AddFilter(null!);

        // Assert
        var uri = uriBuilder.ToUri();
        uri.Should().Be("/api/v1/address");
    }
}