using FluentAssertions;
using Ofx.Battleship.API.Features.Boards.Create;
using Ofx.Battleship.API.ServerTests.Common;
using Ofx.Battleship.API.ServerTests.Infrastructure;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.API.ServerTests.Common.Utilities;

namespace Ofx.Battleship.API.ServerTests.Features.Boards
{
    public class CreateBoardTests
    {
        [Fact]
        public async Task CreateBoard_ReturnsNewBoardIdAndDimensions()
        {
            // Arrange
            await using var server = new Server();
            await server.StartAsync();
            var game = await server.NewGame().SaveAsync();
            var player = await server.NewPlayer().WithGame(game).SaveAsync();
            var command = new Command { PlayerId = player.PlayerId };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await server.Client.PostAsync("/api/boards", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<Model>(response);

            // Assert
            content.BoardId.Should().BePositive();
            // TODO: should be testing against the default value of X and Y.
            content.DimensionX.Should().BePositive();
            content.DimensionY.Should().BePositive();
        }

        [Fact]
        public async Task CreateBoard_ReturnsNewBoardIdAndSpecifiedDimensions()
        {
            // Arrange
            await using var server = new Server();
            await server.StartAsync();
            var game = await server.NewGame().SaveAsync();
            var player = await server.NewPlayer().WithGame(game).SaveAsync();
            var command = new Command
            {
                PlayerId = player.PlayerId,
                DimensionX = 8,
                DimensionY = 12
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await server.Client.PostAsync("/api/boards", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<Model>(response);

            // Assert
            content.BoardId.Should().BePositive();
            content.DimensionX.Should().Be(command.DimensionX);
            content.DimensionY.Should().Be(command.DimensionY);
        }
    }
}
