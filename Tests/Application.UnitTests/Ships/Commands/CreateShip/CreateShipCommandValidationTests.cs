using FluentValidation.TestHelper;
using Ofx.Battleship.Application.Ships.Commands.CreateShip;
using Ofx.Battleship.Application.UnitTests.Common;
using Ofx.Battleship.Domain.Enums;
using Xunit;

namespace Ofx.Battleship.Application.UnitTests.Ships.Commands.CreateShip
{
    public class CreateShipCommandValidationTests : CommandTestBase
    {
        private readonly CreateShipCommandValidator _validator;

        public CreateShipCommandValidationTests()
        {
            _validator = new CreateShipCommandValidator(_context);
        }

        [Fact]
        public void GivenInvalidBoardId_ShouldHaveValidationError()
        {
            // Arrange
            var command = new CreateShipCommand { BoardId = -1 };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BoardId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(11)]
        public void GivenInvalidBowX_ShouldHaveValidationError(int bowX)
        {
            // Arrange
            var command = new CreateShipCommand 
            {
                BoardId = 1,
                BowX = bowX
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BowX);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(11)]
        public void GivenInvalidBowY_ShouldHaveValidationError(int bowY)
        {
            // Arrange
            var command = new CreateShipCommand
            {
                BoardId = 1,
                BowY = bowY
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BowY);
        }

        [Fact]
        public void GivenBowXAndLengthGreaterThanHorizontalBoardDimension_ShouldHaveValidationError()
        {
            // Arrange
            var command = new CreateShipCommand
            {
                BoardId = 1,
                BowX = 8,
                Length = 3,
                Orientation = ShipOrientation.Horizontal
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BowX);
        }

        [Fact]
        public void GivenBowYAndLengthGreaterThanVerticalBoardDimension_ShouldHaveValidationError()
        {
            // Arrange
            var command = new CreateShipCommand
            {
                BoardId = 1,
                BowY = 8,
                Length = 3,
                Orientation = ShipOrientation.Vertical
            };

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
            var command = new CreateShipCommand
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
            var command = new CreateShipCommand
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
