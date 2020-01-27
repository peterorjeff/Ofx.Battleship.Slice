using FluentAssertions;
using Ofx.Battleship.API;
using Ofx.Battleship.API.Enums;
using Ofx.Battleship.API.Features.Ships;
using Ofx.Battleship.WebAPI.IntegrationTests.Common;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.WebAPI.IntegrationTests.Common.Utilities;

namespace Ofx.Battleship.WebAPI.IntegrationTests.Features.Ships
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
            var command = new Create.Command
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
