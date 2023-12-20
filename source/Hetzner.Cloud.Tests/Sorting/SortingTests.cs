using FluentAssertions;
using Hetzner.Cloud.Sorting;

namespace Hetzner.Cloud.Tests.Sorting;

[TestClass]
public class SortingTests
{
    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestMethod]
    public void AsUriParameter_WithServerSortingCreatedAndSortingDirectionDESC_Returns()
    {
        // Act
        var sorting = new Sorting<ServerSorting>(ServerSorting.Created, SortingDirection.Desc);

        // Assert
        sorting.AsUriParameter().Should().Be("created:desc");
    }

    [TestMethod]
    public void AsUriParameter_WithServerSortingNameAndSortingDirectionASC_Returns()
    {
        // Act
        var sorting = new Sorting<ServerSorting>(ServerSorting.Name, SortingDirection.Asc);

        // Assert
        sorting.AsUriParameter().Should().Be("name:asc");
    }
}