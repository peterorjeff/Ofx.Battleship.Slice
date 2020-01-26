using AutoMapper;
using FluentAssertions;
using Ofx.Battleship.Application.Common.Exceptions;
using Ofx.Battleship.Application.Ships.Commands.AttackShip;
using Ofx.Battleship.Application.UnitTests.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ofx.Battleship.Application.UnitTests.Ships.Commands.AttackShip
{
    public class AttackShipCommandTests : CommandTestBase, IClassFixture<MappingTestFixture>
    {
        private readonly IMapper _mapper;

        public AttackShipCommandTests(MappingTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async void Handle_GivenUnknownBoardId_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new AttackShipCommand
            {
                BoardId = 100
            };
            var handler = new AttackShipCommandHandler(_context, _mapper);

            // Act
            Func<Task> response = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await response.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async void Handle_GivenKnownBoardId_ShouldReturnAttackViewModel()
        {
            // Arrange
            var command = new AttackShipCommand
            {
                BoardId = 1,
                AttackX = 1,
                AttackY = 1
            };
            var handler = new AttackShipCommandHandler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().BeOfType<AttackViewModel>();
        }

        [Fact]
        public async void Handle_GivenAttackOnShip_ShouldReturnHit()
        {
            // Arrange
            var command = new AttackShipCommand
            {
                BoardId = 1,
                AttackX = 1,
                AttackY = 1
            };
            var handler = new AttackShipCommandHandler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Hit.Should().BeTrue();
            response.X.Should().Be(command.AttackX);
            response.Y.Should().Be(command.AttackY);
        }

        [Fact]
        public async void Handle_GivenAttackOnWater_ShouldReturnMiss()
        {
            // Arrange
            var command = new AttackShipCommand
            {
                BoardId = 1,
                AttackX = 9,
                AttackY = 9
            };
            var handler = new AttackShipCommandHandler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Hit.Should().BeFalse();
            response.X.Should().Be(0);
            response.Y.Should().Be(0);
        }
    }
}
