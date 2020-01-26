using FluentValidation.TestHelper;
using Ofx.Battleship.Application.Boards.Commands.CreateBoard;
using Xunit;

namespace Ofx.Battleship.Application.UnitTests.Boards.Commands.CreateBoard
{
    public class CreateBoardCommandValidationTests
    {
        private readonly CreateBoardCommandValidator _validator;

        public CreateBoardCommandValidationTests()
        {
            _validator = new CreateBoardCommandValidator();
        }

        [Fact]
        public void GivenInvalidGameId_ShouldHaveValidationError()
        {
            // Arrange
            var gameId = -1;

            // Act & Assert
            _validator.ShouldHaveValidationErrorFor(x => x.GameId, gameId);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(3)]
        [InlineData(55)]
        public void GivenRequestWithInvalidXDimension_ShouldHaveValidationError(int x)
        {
            // Arrange, Act & Assert
            _validator.ShouldHaveValidationErrorFor(x => x.DimensionX, x);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(3)]
        [InlineData(55)]
        public void GivenRequestWithInvalidDimensions_ShouldHaveValidationError(int y)
        {
            // Arrange, Act & Assert
            _validator.ShouldHaveValidationErrorFor(x => x.DimensionY, y);
        }
    }
}
