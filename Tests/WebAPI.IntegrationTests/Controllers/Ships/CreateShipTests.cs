using FluentAssertions;
using Ofx.Battleship.Application.Ships.Commands.CreateShip;
using Ofx.Battleship.Domain.Enums;
using Ofx.Battleship.WebAPI.IntegrationTests.Common;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.WebAPI.IntegrationTests.Common.Utilities;

namespace Ofx.Battleship.WebAPI.IntegrationTests.Controllers.Ships
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
            var command = new CreateShipCommand
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

            var content = await GetResponseContent<ShipViewModel>(response);

            // Assert
            content.ShipId.Should().BePositive();
        }
    }
}
