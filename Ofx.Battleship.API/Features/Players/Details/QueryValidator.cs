using FluentValidation;

namespace Ofx.Battleship.API.Features.Players.Details
{
    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .NotEmpty();
        }
    }
}
