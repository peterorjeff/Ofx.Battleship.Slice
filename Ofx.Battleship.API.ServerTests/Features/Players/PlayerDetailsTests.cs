using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Ofx.Battleship.API.Features.Players.Details;
using Ofx.Battleship.API.ServerTests.Infrastructure;
using Ofx.Battleship.API.ServerTests.Records;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.API.ServerTests.Common.Utilities;

namespace Ofx.Battleship.API.ServerTests.Features.Players
{
    public class PlayerDetailsTests
    {
        [Fact]
        public async Task PlayerDetails_ReturnsPlayerDetails()
        {
            // Arrange
            await using var server = new Server();
            await server.StartAsync();
            var player = await server.NewPlayer().WithPlayerName("Pete").SaveAsync();

            // Act
            var response = await server.Client.GetAsync($"api/players/{player.PlayerId}");

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<Model>(response);

            // Assert
            content.Should().NotBeNull();
            content.PlayerId.Should().Be(player.PlayerId);
            content.Name.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task PlayerDetailsForUnknownId_ReturnsNotFoundStatusCode()
        {
            // Arrange
            await using var server = new Server();
            await server.StartAsync();
            var playerId = 100;

            // Act
            var response = await server.Client.GetAsync($"api/players/{playerId}");

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
