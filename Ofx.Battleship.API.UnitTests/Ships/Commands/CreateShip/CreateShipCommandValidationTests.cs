using FluentValidation.TestHelper;
using Ofx.Battleship.API.Enums;
using Ofx.Battleship.API.Features.Ships.Create;
using Ofx.Battleship.API.UnitTests.Common;
using Xunit;

namespace Ofx.Battleship.API.UnitTests.Ships.Commands.CreateShip
{
    public class CreateShipCommandValidationTests : TestBase
    {
        private readonly CommandValidator _validator;

        public CreateShipCommandValidationTests()
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
        public async void GivenInvalidBowX_ShouldHaveValidationError(int bowX)
        {
            // Arrange
            var command = new Command 
            {
                BoardId = 1,
                BowX = bowX
            };
            var game = await _context.NewGame().WithGameId(1).SaveAsync();
            var player = await _context.NewPlayer().WithGame(game).SaveAsync();
            await _context.NewBoard().WithBoardId(command.BoardId).WithPlayer(player).SaveAsync();

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BowX);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(11)]
        public async void GivenInvalidBowY_ShouldHaveValidationError(int bowY)
        {
            // Arrange
            var command = new Command
            {
                BoardId = 1,
                BowY = bowY
            };
            var game = await _context.NewGame().WithGameId(1).SaveAsync();
            var player = await _context.NewPlayer().WithGame(game).SaveAsync();
            await _context.NewBoard().WithBoardId(command.BoardId).WithPlayer(player).SaveAsync();

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BowY);
        }

        [Fact]
        public async void GivenBowXAndLengthGreaterThanHorizontalBoardDimension_ShouldHaveValidationError()
        {
            // Arrange
            var command = new Command
            {
                BoardId = 1,
                BowX = 8,
                Length = 3,
                Orientation = ShipOrientation.Horizontal
            };
            var game = await _context.NewGame().WithGameId(1).SaveAsync();
            var player = await _context.NewPlayer().WithGame(game).SaveAsync();
            await _context.NewBoard().WithBoardId(command.BoardId).WithPlayer(player).SaveAsync();

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BowX);
        }

        [Fact]
        public async void GivenBowYAndLengthGreaterThanVerticalBoardDimension_ShouldHaveValidationError()
        {
            // Arrange
            var command = new Command
            {
                BoardId = 1,
                BowY = 8,
                Length = 3,
                Orientation = ShipOrientation.Vertical
            };
            var game = await _context.NewGame().WithGameId(1).SaveAsync();
            var player = await _context.NewPlayer().WithGame(game).SaveAsync();
            await _context.NewBoard().WithBoardId(command.BoardId).WithPlayer(player).SaveAsync();

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BowY);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenInvalidLength_ShouldHaveValidationError(int length)
        {
            // Arrange
            var command = new Command
            {
                BoardId = 1,
                BowX = 1,
                BowY = 1,
                Length = length
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Length);
        }

        [Fact]
        public void GivenInvalidOrientation_ShouldHaveValidationError()
        {
            // Arrange
            var command = new Command
            {
                BoardId = 1,
                Orientation = (ShipOrientation)3
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Orientation);
        }
    }
}
