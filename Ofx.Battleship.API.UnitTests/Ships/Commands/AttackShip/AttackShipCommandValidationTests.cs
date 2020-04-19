using FluentValidation.TestHelper;
using Ofx.Battleship.API.Features.Ships.Attack;
using Ofx.Battleship.API.UnitTests.Common;
using Xunit;

namespace Ofx.Battleship.API.UnitTests.Ships.Commands.AttackShip
{
    public class AttackShipCommandValidationTests : TestBase
    {
        private readonly CommandValidator _validator;

        public AttackShipCommandValidationTests()
        {
            _validator = new CommandValidator(_context);
        }

        [Fact]
        public void GivenInvalidBoardId_ShouldHaveValidationError()
        {
            // Arrange
            var command = new Command { BoardId = -1 };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BoardId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(11)]
        public async void GivenInvalidBowX_ShouldHaveValidationError(int attackX)
        {
            // Arrange
            var command = new Command
            {
                BoardId = 1,
                AttackX = attackX,
                AttackY = 1
            };
            var game = await _context.NewGame().WithGameId(1).SaveAsync();
            var player = await _context.NewPlayer().WithGame(game).SaveAsync();
            await _context.NewBoard().WithBoardId(command.BoardId).WithPlayer(player).SaveAsync();

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.AttackX);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(11)]
        public async void GivenInvalidAttackY_ShouldHaveValidationError(int attackY)
        {
            // Arrange
            var command = new Command
            {
                BoardId = 1,
                AttackX = 1,
                AttackY = attackY
            };
            var game = await _context.NewGame().WithGameId(1).SaveAsync();
            var player = await _context.NewPlayer().WithGame(game).SaveAsync();
            await _context.NewBoard().WithBoardId(command.BoardId).WithPlayer(player).SaveAsync();

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.AttackY);
        }
    }
}
