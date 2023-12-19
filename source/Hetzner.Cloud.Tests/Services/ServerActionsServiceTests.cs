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
public class ServerActionsServiceTests
{
    private MockHttpMessageHandler _mockHttp;
    private IHttpClientFactory _httpClientFactory;
    private IServerActionsService _instance;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockHttp = new MockHttpMessageHandler();

        var httpClient = _mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri("https://localhost");
        
        _httpClientFactory = Substitute.For<IHttpClientFactory>();
        _httpClientFactory.CreateClient(Arg.Any<string>())
            .Returns(httpClient);
        
        _instance = new ServerActionsService(_httpClientFactory);
    }

    [TestMethod]
    public async Task GetAllAsync_WithHetznerSample_Returns()
    {
        // Arrange
        var jsonContent = """
                          {
                            "actions": [
                              {
                                "command": "start_resource",
                                "error": {
                                  "code": "action_failed",
                                  "message": "Action failed"
                                },
                                "finished": "2016-01-30T23:55:00+00:00",
                                "id": 42,
                                "progress": 100,
                                "resources": [
                                  {
                                    "id": 42,
                                    "type": "server"
                                  }
                                ],
                                "started": "2016-01-30T23:55:00+00:00",
                                "status": "success"
                              }
                            ],
                            "meta": {
                              "pagination": {
                                "last_page": 4,
                                "next_page": 4,
                                "page": 3,
                                "per_page": 25,
                                "previous_page": 2,
                                "total_entries": 100
                              }
                            }
                          }
                          """;
        _mockHttp.When("https://localhost/v1/servers/actions")
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
        result.Items.First().Id.Should().Be(42);
        result.Items.First().Command.Should().Be("start_resource");
        result.Items.First().Finished.Should().Be(DateTime.Parse("2016-01-30T23:55:00+00:00"));
        result.Items.First().Progress.Should().Be(100);
        result.Items.First().Started.Should().Be(DateTime.Parse("2016-01-30T23:55:00+00:00"));
        result.Items.First().Status.Should().Be(ServerActionStatus.Success);
        
        result.Items.First().Error.Should().NotBeNull();
        result.Items.First().Error!.Code.Should().Be("action_failed");
        result.Items.First().Error!.Message.Should().Be("Action failed");
        
        result.Items.First().Resources.Should().NotBeNull();
        result.Items.First().Resources.Should().HaveCount(1);
        result.Items.First().Resources.First().Should().NotBeNull();
        result.Items.First().Resources.First().Id.Should().Be(42);
        result.Items.First().Resources.First().Type.Should().Be("server");
    }

    [TestMethod]
    public async Task GetAllAsync_WithInvalidPage_Throws()
    {
        // Arrange
        _mockHttp.When("https://localhost/v1/servers*")
            .Respond(HttpStatusCode.InternalServerError);

        // Act
        Func<Task> act = async () => await _instance.GetAllAsync(0);

        // Assert
        await act.Should().ThrowAsync<InvalidArgumentException>()
            .WithMessage("invalid page number (0). must be greather than 0.");
    }
}