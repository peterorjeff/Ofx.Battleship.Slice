using FluentAssertions;
using Ofx.Battleship.API.Enums;
using Ofx.Battleship.API.Features.Ships.Create;
using Ofx.Battleship.API.ServerTests.Common;
using Ofx.Battleship.API.ServerTests.Infrastructure;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.API.ServerTests.Common.Utilities;

namespace Ofx.Battleship.API.ServerTests.Features.Ships
{
    public class CreateShipTests
    {
        [Fact]
        public async Task CreateShip_ReturnsNewShip()
        {
            // Arrange
            await using var server = new Server();
            await server.StartAsync();
            var game = await server.NewGame().SaveAsync();
            var player = await server.NewPlayer().WithGame(game).SaveAsync();
            var board = await server.NewBoard().WithPlayer(player).SaveAsync();
            var command = new Command
            {
                BoardId = board.BoardId,
                BowX = 3,
                BowY = 1,
                Length = 2,
                Orientation = ShipOrientation.Horizontal
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await server.Client.PostAsync("/api/ships", requestContent);

            response.EnsureSuccessStatusCode();

            var shipId = await GetResponseContent<int>(response);

            // Assert
            shipId.Should().BePositive();
        }
    }
}
