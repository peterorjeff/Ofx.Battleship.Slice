using FluentValidation.TestHelper;
using Ofx.Battleship.API.Features.Ships;
using Ofx.Battleship.API.UnitTests.Common;
using Xunit;

namespace Ofx.Battleship.API.UnitTests.Ships.Commands.AttackShip
{
    public class AttackShipCommandValidationTests : CommandTestBase
    {
        private readonly Attack.CommandValidator _validator;

        public AttackShipCommandValidationTests()
        {
            _validator = new Attack.CommandValidator(_context);
        }

        [Fact]
        public void GivenInvalidBoardId_ShouldHaveValidationError()
        {
            // Arrange
            var command = new Attack.Command { BoardId = -1 };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BoardId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(11)]
        public void GivenInvalidBowX_ShouldHaveValidationError(int attackX)
        {
            // Arrange
            var command = new Attack.Command
            {
                BoardId = 1,
                AttackX = attackX,
                AttackY = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.AttackX);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(11)]
        public void GivenInvalidAttackY_ShouldHaveValidationError(int attackY)
        {
            // Arrange
            var command = new Attack.Command
            {
                BoardId = 1,
                AttackX = 1,
                AttackY = attackY
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.AttackY);
        }
    }
}
