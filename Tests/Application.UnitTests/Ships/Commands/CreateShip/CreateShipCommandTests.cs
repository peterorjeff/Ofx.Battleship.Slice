using AutoMapper;
using FluentAssertions;
using Ofx.Battleship.Application.Common.Exceptions;
using Ofx.Battleship.Application.Ships.Commands.CreateShip;
using Ofx.Battleship.Application.UnitTests.Common;
using Ofx.Battleship.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ofx.Battleship.Application.UnitTests.Ships.Commands.CreateShip
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
            var command = new CreateShipCommand
            {
                BoardId = 100,
                BowX = 1,
                BowY = 1,
                Length = 2,
                Orientation = ShipOrientation.Horizontal
            };
            var handler = new CreateShipCommandHandler(_context, _mapper);

            // Act
            Func<Task> response = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await response.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async void Handle_GivenKnownBoardId_ShouldReturnNewShipId()
        {
            // Arrange
            var command = new CreateShipCommand
            {
                BoardId = 1,
                BowX = 2,
                BowY = 1,
                Length = 2,
                Orientation = ShipOrientation.Horizontal
            };
            var handler = new CreateShipCommandHandler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.ShipId.Should().BePositive();
        }

        [Fact]
        public async void Handle_GivenKnownBoardId_ShouldReturnShipViewModel()
        {
            // Arrange
            var command = new CreateShipCommand
            {
                BoardId = 1,
                BowX = 3,
                BowY = 1,
                Length = 2,
                Orientation = ShipOrientation.Horizontal
            };
            var handler = new CreateShipCommandHandler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().BeOfType<ShipViewModel>();
        }

        [Fact]
        public async void Handle_GivenCollidingShip_ShouldThrowShipCollisionException()
        {
            // Arrange
            var command = new CreateShipCommand
            {
                BoardId = 1,
                BowX = 1,
                BowY = 1,
                Length = 2,
                Orientation = ShipOrientation.Horizontal
            };
            var handler = new CreateShipCommandHandler(_context, _mapper);

            // Act
            Func<Task> response = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await response.Should().ThrowAsync<ShipCollisionException>();
        }
    }
}
