using FluentAssertions;
using Ofx.Battleship.API.Features.Players.Create;
using Ofx.Battleship.API.ServerTests.Infrastructure;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.API.ServerTests.Common.Utilities;

namespace Ofx.Battleship.API.ServerTests.Features.Players
{
    public class CreatePlayerTests
    {
        [Fact]
        public async Task CreatePlayer_ReturnsNewPlayerId()
        {
            // Arrange
            await using var server = new Server();
            await server.StartAsync();
            var command = new Command
            {
                Name = "Pete"
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await server.Client.PostAsync("api/players", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<int>(response);

            // Assert
            content.Should().BePositive();
        }
    }
}
