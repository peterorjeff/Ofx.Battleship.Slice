using FluentAssertions;
using Ofx.Battleship.API.ServerTests.Infrastructure;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.API.ServerTests.Common.Utilities;

namespace Ofx.Battleship.API.ServerTests.Features.Games
{
    public class CreateGameTests
    {
        [Fact]
        public async Task CreateGame_ReturnsNewGameId()
        {
            // Arrange
            await using var server = new Server();
            await server.StartAsync();

            // Act
            var response = await server.Client.PostAsync("/api/games", null);
            
            response.EnsureSuccessStatusCode();

            var gameId = await GetResponseContent<int>(response);

            // Assert
            gameId.Should().BePositive();
        }
    }
}
