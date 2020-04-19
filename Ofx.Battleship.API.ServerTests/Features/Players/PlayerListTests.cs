using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Ofx.Battleship.API.Features.Players.List;
using Ofx.Battleship.API.ServerTests.Common;
using Ofx.Battleship.API.ServerTests.Infrastructure;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.API.ServerTests.Common.Utilities;

namespace Ofx.Battleship.API.ServerTests.Features.Players
{
    public class PlayerListTests
    {
        [Fact]
        public async Task PlayerList_WhenNoPlayers_ReturnsEmptyList()
        {
            // Arrange
            await using var server = new Server();
            await server.StartAsync();

            // Act
            var response = await server.Client.GetAsync("/api/players");

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<Model>(response);

            // Assert
            content.Should().NotBeNull();
            content.Players.Should().BeEmpty();
        }

        [Fact]
        public async Task PlayerList_WhenPlayersExist_ReturnsListOfAllPlayers()
        {
            // Arrange
            await using var server = new Server();
            await server.StartAsync();
            await server.NewPlayer().SaveAsync();
            await server.NewPlayer().SaveAsync();

            // Act
            var response = await server.Client.GetAsync("/api/players");

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<Model>(response);

            // Assert
            content.Should().NotBeNull();
            content.Players.Should().NotBeEmpty();
            content.Players.Count.Should().Be(2);
        }
    }
}
