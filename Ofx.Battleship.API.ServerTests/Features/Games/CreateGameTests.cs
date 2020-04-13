using FluentAssertions;
using Ofx.Battleship.API.ServerTests.Common;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.API.ServerTests.Common.Utilities;

namespace Ofx.Battleship.API.ServerTests.Features.Games
{
    public class CreateGameTests : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public CreateGameTests(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateGame_ReturnsNewGameId()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsync("/api/games", null);
            
            response.EnsureSuccessStatusCode();

            var gameId = await GetResponseContent<int>(response);

            // Assert
            gameId.Should().BePositive();
        }
    }
}
