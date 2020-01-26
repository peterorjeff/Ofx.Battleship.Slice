using FluentAssertions;
using Ofx.Battleship.Application.Boards.Commands.CreateBoard;
using Ofx.Battleship.WebAPI.IntegrationTests.Common;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.WebAPI.IntegrationTests.Common.Utilities;

namespace Ofx.Battleship.WebAPI.IntegrationTests.Controllers.Boards
{
    public class CreateBoardTests : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public CreateBoardTests(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateBoard_ReturnsNewBoardIdAndDimensions()
        {
            // Arrange
            var client = _factory.CreateClient();
            var command = new CreateBoardCommand
            {
                GameId = 1
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await client.PostAsync("/api/boards", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<BoardViewModel>(response);

            // Assert
            content.BoardId.Should().BePositive();
            content.DimensionX.Should().BePositive();
            content.DimensionY.Should().BePositive();
        }
    }
}
