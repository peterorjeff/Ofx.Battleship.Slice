using FluentValidation.TestHelper;
using Ofx.Battleship.API.Features.Boards.Create;
using Xunit;

namespace Ofx.Battleship.API.UnitTests.Boards.Commands.CreateBoard
{
    public class CreateBoardCommandValidationTests
    {
        private readonly CommandValidator _validator;

        public CreateBoardCommandValidationTests()
        {
            _validator = new CommandValidator();
        }

        [Fact]
        public void GivenInvalidPlaterId_ShouldHaveValidationError()
        {
            // Arrange
            var gameId = -1;

            // Act & Assert
            _validator.ShouldHaveValidationErrorFor(x => x.PlayerId, gameId);
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
