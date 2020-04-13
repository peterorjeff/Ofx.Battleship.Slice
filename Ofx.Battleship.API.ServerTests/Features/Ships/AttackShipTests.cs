using FluentAssertions;
using Ofx.Battleship.API.Features.Ships.Attack;
using Ofx.Battleship.API.ServerTests.Infrastructure;
using Ofx.Battleship.API.ServerTests.Records;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.API.ServerTests.Common.Utilities;

namespace Ofx.Battleship.API.ServerTests.Features.Ships
{
    public class AttackShipTests
    {
        [Fact]
        public async Task AttackShip_ReturnsHit()
        {
            // Arrange
            await using var server = new Server();
            await server.StartAsync();
            var game = await server.NewGame().SaveAsync();
            var board = await server.NewBoard().WithGame(game).SaveAsync();
            var ship = await server.NewShip().WithBoard(board).SaveAsync();
            var shipPart = await server.NewShipPart().WithShip(ship).WithCoordinates(1, 1).SaveAsync();
            var command = new Command
            {
                BoardId = board.BoardId,
                AttackX = shipPart.X,
                AttackY = shipPart.Y
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await server.Client.PutAsync("/api/ships/attack", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<Model>(response);

            // Assert
            content.Hit.Should().BeTrue();
            content.X.Should().Be(command.AttackX);
            content.Y.Should().Be(command.AttackY);
        }

        [Fact]
        public async Task AttackWater_ReturnsMiss()
        {
            // Arrange
            await using var server = new Server();
            await server.StartAsync();
            var game = await server.NewGame().SaveAsync();
            var board = await server.NewBoard().WithGame(game).SaveAsync();
            var command = new Command
            {
                BoardId = board.BoardId,
                AttackX = 9,
                AttackY = 9
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await server.Client.PutAsync("/api/ships/attack", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<Model>(response);

            // Assert
            content.Hit.Should().BeFalse();
            content.X.Should().Be(0);
            content.Y.Should().Be(0);
        }
    }
}
