using FluentAssertions;
using Ofx.Battleship.API.Features.Games;
using Ofx.Battleship.API.FixieTests.Common;
using System.Threading;
using System.Threading.Tasks;
using static Ofx.Battleship.API.FixieTests.Common.Utilities;

namespace Ofx.Battleship.API.FixieTests.Features
{
    public class CreateGameTests : TestBase
    {
        public CreateGameTests(IntegrationTestWebApplicationFactory<Startup> factory) : base(factory)
        { }

        public async Task Handle_GivenValidRequest_ShouldReturnNewGameId()
        {
            // Arrange
            var handler = new Create.Handler(_context);

            // Act
            var response = await handler.Handle(new Create.Command(), CancellationToken.None);

            // Assert
            response.Should().BePositive();
        }

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
