using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Ofx.Battleship.API.Features.Games.Details;
using Ofx.Battleship.API.ServerTests.Infrastructure;
using Ofx.Battleship.API.ServerTests.Records;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.API.ServerTests.Common.Utilities;

namespace Ofx.Battleship.API.ServerTests.Features.Games
{
    public class GameDetailsTests
    {
        [Fact]
        public async Task GameDetailsForKnownId_ReturnsGameDetails()
        {
            // Arrange
            await using var server = new Server();
            await server.StartAsync();
            var game = await server.NewGame().SaveAsync();
            await server.NewBoard().WithGame(game).SaveAsync();
            await server.NewBoard().WithGame(game).SaveAsync();

            // Act
            var response = await server.Client.GetAsync($"/api/games/{game.GameId}");

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<Model>(response);

            // Assert
            content.Should().NotBeNull();
            content.GameId.Should().Be(game.GameId);
            content.Boards.Should().NotBeNull();
            content.Boards.Count.Should().Be(2);
        }

        [Fact]
        public async Task GameDetailsForUnknownId_ReturnsNotFoundStatusCode()
        {
            // Arrange
            await using var server = new Server();
            await server.StartAsync();
            var gameId = 100;

            // Act
            var response = await server.Client.GetAsync($"/api/games/{gameId}");

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
