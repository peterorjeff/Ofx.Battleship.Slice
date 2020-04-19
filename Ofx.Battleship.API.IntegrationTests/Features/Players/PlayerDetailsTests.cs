using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Ofx.Battleship.API;
using Ofx.Battleship.API.Features.Players.Details;
using Ofx.Battleship.WebAPI.IntegrationTests.Common;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.WebAPI.IntegrationTests.Common.Utilities;

namespace Ofx.Battleship.WebAPI.IntegrationTests.Features.Players
{
    public class PlayerDetailsTests : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public PlayerDetailsTests(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task PlayerDetails_ReturnsPlayerDetails()
        {
            // Arrange
            var client = _factory.CreateClient();
            var playerId = 1;

            // Act
            var response = await client.GetAsync($"api/players/{playerId}");

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<Model>(response);

            // Assert
            content.Should().NotBeNull();
            content.PlayerId.Should().Be(playerId);
            content.Name.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task PlayerDetailsForUnknownId_ReturnsNotFoundStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var playerId = 100;

            // Act
            var response = await client.GetAsync($"api/players/{playerId}");

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
