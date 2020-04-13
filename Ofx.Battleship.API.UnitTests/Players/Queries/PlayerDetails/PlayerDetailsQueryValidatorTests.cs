using FluentValidation.TestHelper;
using Ofx.Battleship.API.Features.Players.Details;
using Xunit;

namespace Ofx.Battleship.API.UnitTests.Players.Queries.PlayerDetails
{
    public class PlayerDetailsQueryValidatorTests
    {
        private readonly QueryValidator _validator;

        public PlayerDetailsQueryValidatorTests()
        {
            _validator = new QueryValidator();
        }

        [Fact]
        public void GivenValidPlayerId_ShouldNotHaveValidationError()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Id, 1);
        }

        [Fact]
        public void GivenInvalidPlayerId_ShouldHaveValidationError()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
