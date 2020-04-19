using AutoMapper;
using FluentAssertions;
using Ofx.Battleship.API.Exceptions;
using Ofx.Battleship.API.Features.Ships.Attack;
using Ofx.Battleship.API.UnitTests.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ofx.Battleship.API.UnitTests.Ships.Commands.AttackShip
{
    public class AttackShipCommandTests : TestBase, IClassFixture<MappingTestFixture>
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
            var command = new Command
            {
                BoardId = 100
            };
            var handler = new Handler(_context, _mapper);

            // Act
            Func<Task> response = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await response.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async void Handle_GivenKnownBoardId_ShouldReturnAttackModel()
        {
            // Arrange
            var command = new Command
            {
                BoardId = 1,
                AttackX = 1,
                AttackY = 1
            };
            var handler = new Handler(_context, _mapper);
            var game = await _context.NewGame().WithGameId(1).SaveAsync();
            var player = await _context.NewPlayer().WithGame(game).SaveAsync();
            await _context.NewBoard().WithBoardId(command.BoardId).WithPlayer(player).SaveAsync();

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().BeOfType<Model>();
        }

        [Fact]
        public async void Handle_GivenAttackOnShip_ShouldReturnHit()
        {
            // Arrange
            var command = new Command
            {
                BoardId = 1,
                AttackX = 1,
                AttackY = 1
            };
            var handler = new Handler(_context, _mapper);
            var game = await _context.NewGame().WithGameId(1).SaveAsync();
            var player = await _context.NewPlayer().WithGame(game).SaveAsync();
            var board = await _context.NewBoard().WithBoardId(command.BoardId).WithPlayer(player).SaveAsync();
            var ship = await _context.NewShip().WithBoard(board).SaveAsync();
            await _context.NewShipPart().WithShip(ship).WithCoordinates(command.AttackX, command.AttackY).SaveAsync();

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
            var command = new Command
            {
                BoardId = 1,
                AttackX = 9,
                AttackY = 9
            };
            var handler = new Handler(_context, _mapper);
            var game = await _context.NewGame().WithGameId(1).SaveAsync();
            var player = await _context.NewPlayer().WithGame(game).SaveAsync();
            await _context.NewBoard().WithBoardId(command.BoardId).WithPlayer(player).SaveAsync();

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Hit.Should().BeFalse();
            response.X.Should().Be(0);
            response.Y.Should().Be(0);
        }
    }
}
