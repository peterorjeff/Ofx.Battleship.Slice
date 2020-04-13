using FluentValidation.TestHelper;
using Ofx.Battleship.API.Features.Games.Details;
using Xunit;

namespace Ofx.Battleship.API.UnitTests.Games.Queries.GameDetails
{
    public class GameDetailsQueryValidatorTests
    {
        private readonly QueryValidator _validator;

        public GameDetailsQueryValidatorTests()
        {
            _validator = new QueryValidator();
        }

        [Fact]
        public void GivenValidGameId_ShouldNotHaveValidationError()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Id, 1);
        }

        [Fact]
        public void GivenInvalidGameId_ShouldHaveValidationError()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
