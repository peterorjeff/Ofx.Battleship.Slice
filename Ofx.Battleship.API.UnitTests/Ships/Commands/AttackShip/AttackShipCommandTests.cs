using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Ofx.Battleship.API.Exceptions;
using Ofx.Battleship.API.Features.Ships.Attack;
using Ofx.Battleship.API.UnitTests.Common;
using System;
using System.Linq;
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
        public async void Handle_GivenUnknownPlayerId_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new Command
            {
                AttackerPlayerId = 100
            };
            var handler = new Handler(_context, _mapper);

            // Act
            Func<Task> response = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await response.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async void Handle_GivenFirstTurnOfGame_ShouldNotThrowOutOfTurnException()
        {
            // Arrange
            var game = await _context.NewGame().SaveAsync();
            var player = await _context.NewPlayer().WithGame(game).SaveAsync();
            var board = await _context.NewBoard().WithPlayer(player).SaveAsync();

            var command = new Command
            {
                AttackerPlayerId = player.PlayerId,
                BoardId = board.BoardId
            };
            var handler = new Handler(_context, _mapper);

            // Act
            Func<Task> response = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await response.Should().NotThrowAsync<OutOfTurnException>();
        }

        [Fact]
        public async void Handle_GivenPlayerAttackingInTurn_ShouldNotThrowOutOfTurnException()
        {
            // Arrange
            var game = await _context.NewGame().SaveAsync();
            var playerOne = await _context.NewPlayer().WithGame(game).SaveAsync();
            var playerTwo = await _context.NewPlayer().WithGame(game).SaveAsync();
            var board = await _context.NewBoard().WithPlayer(playerOne).SaveAsync();
            var shot = await _context.NewShot().WithAttacker(playerOne).SaveAsync();

            var command = new Command
            {
                AttackerPlayerId = playerTwo.PlayerId,
                BoardId = board.BoardId
            };
            var handler = new Handler(_context, _mapper);

            // Act
            Func<Task> response = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await response.Should().NotThrowAsync<OutOfTurnException>();
        }

        [Fact]
        public async void Handle_GivenPlayerAttackingOutOfTurn_ShouldThrowOutOfTurnException()
        {
            // Arrange
            var game = await _context.NewGame().SaveAsync();
            var player = await _context.NewPlayer().WithGame(game).SaveAsync();
            var board = await _context.NewBoard().WithPlayer(player).SaveAsync();
            var shot = await _context.NewShot().WithAttacker(player).SaveAsync();

            var command = new Command
            {
                AttackerPlayerId = player.PlayerId,
                BoardId = board.BoardId
            };
            var handler = new Handler(_context, _mapper);

            // Act
            Func<Task> response = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await response.Should().ThrowAsync<OutOfTurnException>();
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
            var game = await _context.NewGame().WithGameId(1).SaveAsync();
            var player = await _context.NewPlayer().WithGame(game).SaveAsync();
            var board = await _context.NewBoard().WithPlayer(player).SaveAsync();

            var command = new Command
            {
                AttackerPlayerId = player.PlayerId,
                BoardId = board.BoardId,
                AttackX = 1,
                AttackY = 1
            };
            var handler = new Handler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().BeOfType<Model>();
        }

        [Fact]
        public async void Handle_GivenAttackOnShip_ShouldReturnHit()
        {
            // Arrange
            var game = await _context.NewGame().SaveAsync();
            var player = await _context.NewPlayer().WithGame(game).SaveAsync();
            var board = await _context.NewBoard().WithPlayer(player).SaveAsync();
            var ship = await _context.NewShip().WithBoard(board).SaveAsync();
            var shipPart = await _context.NewShipPart().WithShip(ship).WithCoordinates(1, 1).SaveAsync();

            var command = new Command
            {
                AttackerPlayerId = player.PlayerId,
                BoardId = board.BoardId,
                AttackX = shipPart.X,
                AttackY = shipPart.Y
            };
            var handler = new Handler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Hit.Should().BeTrue();
            response.X.Should().Be(command.AttackX);
            response.Y.Should().Be(command.AttackY);

            var shot = await _context.Shots
                .Where(x => x.Player == player)
                .Where(x => x.Board == board)
                .Where(x => x.AttackX == command.AttackX && x.AttackY == command.AttackY)
                .FirstOrDefaultAsync();
            shot.Should().NotBeNull();
            shot.Hit.Should().BeTrue();
            shot.ShipPartHit.Should().NotBeNull();
        }

        [Fact]
        public async void Handle_GivenAttackOnWater_ShouldReturnMiss()
        {
            // Arrange
            var game = await _context.NewGame().WithGameId(1).SaveAsync();
            var player = await _context.NewPlayer().WithGame(game).SaveAsync();
            var board = await _context.NewBoard().WithPlayer(player).SaveAsync();

            var command = new Command
            {
                AttackerPlayerId = player.PlayerId,
                BoardId = board.BoardId,
                AttackX = 9,
                AttackY = 9
            };
            var handler = new Handler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Hit.Should().BeFalse();
            response.X.Should().Be(0);
            response.Y.Should().Be(0);

            var shot = await _context.Shots
                .Where(x => x.Player == player)
                .Where(x => x.Board == board)
                .Where(x => x.AttackX == command.AttackX && x.AttackY == command.AttackY)
                .FirstOrDefaultAsync();
            shot.Should().NotBeNull();
            shot.Hit.Should().BeFalse();
            shot.ShipPartHit.Should().BeNull();
        }
    }
}
