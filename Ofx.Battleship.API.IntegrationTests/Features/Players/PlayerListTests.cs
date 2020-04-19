using FluentAssertions;
using Ofx.Battleship.API;
using Ofx.Battleship.API.Features.Players.List;
using Ofx.Battleship.WebAPI.IntegrationTests.Common;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.WebAPI.IntegrationTests.Common.Utilities;

namespace Ofx.Battleship.WebAPI.IntegrationTests.Features.Players
{
    public class PlayerListTests : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public PlayerListTests(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact(Skip = "Shared dbcontext, so there are never no players.")]
        public async Task PlayerList_WhenNoPlayers_ReturnsEmptyList()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/players");

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<Model>(response);

            // Assert
            content.Should().NotBeNull();
            content.Players.Should().BeEmpty();
        }

        [Fact]
        public async Task PlayerList_WhenPlayersExist_ReturnsListOfAllPlayers()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/players");

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<Model>(response);

            // Assert
            content.Should().NotBeNull();
            content.Players.Should().NotBeEmpty();
            content.Players.Count.Should().BeGreaterOrEqualTo(2);       // Cannot match exact number due to shared dbcontext.
        }
    }
}
