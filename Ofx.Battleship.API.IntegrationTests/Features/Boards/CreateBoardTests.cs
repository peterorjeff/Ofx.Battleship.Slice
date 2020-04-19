using FluentAssertions;
using Ofx.Battleship.API;
using Ofx.Battleship.API.Features.Boards;
using Ofx.Battleship.API.Features.Boards.Create;
using Ofx.Battleship.WebAPI.IntegrationTests.Common;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.WebAPI.IntegrationTests.Common.Utilities;

namespace Ofx.Battleship.WebAPI.IntegrationTests.Features.Boards
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
            var command = new Command
            {
                PlayerId = 2
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await client.PostAsync("/api/boards", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<Model>(response);

            // Assert
            content.BoardId.Should().BePositive();
            content.DimensionX.Should().BePositive();
            content.DimensionY.Should().BePositive();
        }
    }
}
