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
    public async Task AsUriParameter_WithServerSortingCreatedAndSortingDirectionDESC_Returns()
    {
        // Act
        var sorting = new Sorting<ServerSorting>(ServerSorting.Created, SortingDirection.DESC);

        // Assert
        sorting.AsUriParameter().Should().Be("created:desc");
    }

    [TestMethod]
    public async Task AsUriParameter_WithServerSortingNameAndSortingDirectionASC_Returns()
    {
        // Act
        var sorting = new Sorting<ServerSorting>(ServerSorting.Name, SortingDirection.ASC);

        // Assert
        sorting.AsUriParameter().Should().Be("name:asc");
    }
}