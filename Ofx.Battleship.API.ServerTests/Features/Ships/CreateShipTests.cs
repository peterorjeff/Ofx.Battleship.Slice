using FluentAssertions;
using Ofx.Battleship.API.Enums;
using Ofx.Battleship.API.Features.Ships.Create;
using Ofx.Battleship.API.ServerTests.Common;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.API.ServerTests.Common.Utilities;

namespace Ofx.Battleship.API.ServerTests.Features.Ships
{
    public class CreateShipTests : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public CreateShipTests(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateShip_ReturnsNewShip()
        {
            // Arrange
            var client = _factory.CreateClient();
            var command = new Command
            {
                BoardId = 1,
                BowX = 3,
                BowY = 1,
                Length = 2,
                Orientation = ShipOrientation.Horizontal
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await client.PostAsync("/api/ships", requestContent);

            response.EnsureSuccessStatusCode();

            var shipId = await GetResponseContent<int>(response);

            // Assert
            shipId.Should().BePositive();
        }
    }
}
