using FluentAssertions;
using Ofx.Battleship.API.Features.Ships.Attack;
using Ofx.Battleship.API.ServerTests.Common;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.API.ServerTests.Common.Utilities;

namespace Ofx.Battleship.API.ServerTests.Features.Ships
{
    public class AttackShipTests : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public AttackShipTests(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task AttackShip_ReturnsHit()
        {
            // Arrange
            var client = _factory.CreateClient();
            var command = new Command
            {
                BoardId = 1,
                AttackX = 1,
                AttackY = 1
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await client.PutAsync("/api/ships/attack", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<Model>(response);

            // Assert
            content.Hit.Should().BeTrue();
            content.X.Should().Be(command.AttackX);
            content.Y.Should().Be(command.AttackY);
        }

        [Fact]
        public async Task AttackWater_ReturnsMiss()
        {
            // Arrange
            var client = _factory.CreateClient();
            var command = new Command
            {
                BoardId = 1,
                AttackX = 9,
                AttackY = 9
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await client.PutAsync("/api/ships/attack", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<Model>(response);

            // Assert
            content.Hit.Should().BeFalse();
            content.X.Should().Be(0);
            content.Y.Should().Be(0);
        }
    }
}
