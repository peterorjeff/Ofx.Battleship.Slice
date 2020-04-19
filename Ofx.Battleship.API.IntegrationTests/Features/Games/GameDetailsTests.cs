using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Ofx.Battleship.API;
using Ofx.Battleship.API.Features.Games.Details;
using Ofx.Battleship.WebAPI.IntegrationTests.Common;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.WebAPI.IntegrationTests.Common.Utilities;

namespace Ofx.Battleship.WebAPI.IntegrationTests.Features.Games
{
    public class GameDetailsTests : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public GameDetailsTests(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GameDetailsForKnownId_ReturnsGameDetails()
        {
            // Arrange
            var client = _factory.CreateClient();
            var gameId = 1;

            // Act
            var response = await client.GetAsync($"/api/games/{gameId}");

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<Model>(response);

            // Assert
            content.Should().NotBeNull();
            content.GameId.Should().Be(gameId);
            content.Players.Should().NotBeNull();
            content.Players.Count.Should().Be(2);
        }

        [Fact]
        public async Task GameDetailsForUnknownId_ReturnsNotFoundStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var gameId = 100;

            // Act
            var response = await client.GetAsync($"/api/games/{gameId}");

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
