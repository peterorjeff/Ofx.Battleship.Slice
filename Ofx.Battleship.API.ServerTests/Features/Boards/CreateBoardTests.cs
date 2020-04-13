using FluentAssertions;
using Ofx.Battleship.API.Features.Boards.Create;
using Ofx.Battleship.API.ServerTests.Infrastructure;
using Ofx.Battleship.API.ServerTests.Records;
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
            var command = new Command { GameId = game.GameId };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await server.Client.PostAsync("/api/boards", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<Model>(response);

            // Assert
            content.BoardId.Should().BePositive();
            content.DimensionX.Should().BePositive();
            content.DimensionY.Should().BePositive();
        }
    }
}
