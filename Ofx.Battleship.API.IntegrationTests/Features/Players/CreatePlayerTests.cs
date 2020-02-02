using FluentAssertions;
using Ofx.Battleship.API;
using Ofx.Battleship.API.Features.Players.Create;
using Ofx.Battleship.WebAPI.IntegrationTests.Common;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.WebAPI.IntegrationTests.Common.Utilities;

namespace Ofx.Battleship.WebAPI.IntegrationTests.Features.Players
{
    public class CreatePlayerTests : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public CreatePlayerTests(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreatePlayer_ReturnsNewPlayerId()
        {
            // Arrange
            var client = _factory.CreateClient();
            var command = new Command
            {
                Name = "Pete"
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await client.PostAsync("api/players", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<int>(response);

            // Assert
            content.Should().BePositive();
        }
    }
}
