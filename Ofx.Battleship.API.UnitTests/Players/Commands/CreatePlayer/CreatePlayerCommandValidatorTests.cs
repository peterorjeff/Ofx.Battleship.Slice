using FluentValidation.TestHelper;
using Ofx.Battleship.API.Features.Players.Create;
using Xunit;

namespace Ofx.Battleship.API.UnitTests.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommandValidatorTests
    {
        private readonly CommandValidator _validator;

        public CreatePlayerCommandValidatorTests()
        {
            _validator = new CommandValidator();
        }

        [Fact]
        public void GivenValidPlayerName_ShouldNotHaveValidationError()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, "Pete");
        }

        [Fact]
        public void GivenEmptyPlayerName_ShouldHaveValidationError()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }

        [Fact]
        public void GivenInvalidPlayerName_ShouldHaveValidationError()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, "this name is really long, much longer than the character limit.");
        }
    }
}
