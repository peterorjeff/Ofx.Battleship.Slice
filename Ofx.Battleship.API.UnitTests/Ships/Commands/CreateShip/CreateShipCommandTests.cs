using AutoMapper;
using FluentAssertions;
using Ofx.Battleship.API.Enums;
using Ofx.Battleship.API.Exceptions;
using Ofx.Battleship.API.Features.Ships.Create;
using Ofx.Battleship.API.UnitTests.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ofx.Battleship.API.UnitTests.Ships.Commands.CreateShip
{
    public class CreateShipCommandTests : CommandTestBase, IClassFixture<MappingTestFixture>
    {
        private readonly IMapper _mapper;

        public CreateShipCommandTests(MappingTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async void Handle_GivenUnknownBoardId_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new Command
            {
                BoardId = 100,
                BowX = 1,
                BowY = 1,
                Length = 2,
                Orientation = ShipOrientation.Horizontal
            };
            var handler = new Handler(_context);

            // Act
            Func<Task> response = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await response.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async void Handle_GivenKnownBoardId_ShouldReturnNewShipId()
        {
            // Arrange
            var command = new Command
            {
                BoardId = 1,
                BowX = 2,
                BowY = 1,
                Length = 2,
                Orientation = ShipOrientation.Horizontal
            };
            var handler = new Handler(_context);

            // Act
            var shipId = await handler.Handle(command, CancellationToken.None);

            // Assert
            shipId.Should().BePositive();
        }

        [Fact]
        public async void Handle_GivenCollidingShip_ShouldThrowShipCollisionException()
        {
            // Arrange
            var command = new Command
            {
                BoardId = 1,
                BowX = 1,
                BowY = 1,
                Length = 2,
                Orientation = ShipOrientation.Horizontal
            };
            var handler = new Handler(_context);

            // Act
            Func<Task> response = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await response.Should().ThrowAsync<ShipCollisionException>();
        }
    }
}
