using FluentAssertions;
using Ofx.Battleship.Application.Games.Commands.CreateGame;
using Ofx.Battleship.WebAPI.IntegrationTests.Common;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.WebAPI.IntegrationTests.Common.Utilities;

namespace Ofx.Battleship.WebAPI.IntegrationTests.Controllers.Games
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

            var content = await GetResponseContent<GameViewModel>(response);

            // Assert
            content.GameId.Should().BePositive();
        }
    }
}
