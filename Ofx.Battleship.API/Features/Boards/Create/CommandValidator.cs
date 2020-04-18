using FluentValidation;

namespace Ofx.Battleship.API.Features.Boards.Create
{
    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.PlayerId).GreaterThan(0).NotEmpty();

            RuleFor(x => x.DimensionX)
                .GreaterThan(5)
                .WithMessage("Minimum board size is 5 x 5.")
                .LessThanOrEqualTo(50)
                .WithMessage("Maximum board size is 50 x 50.");

            RuleFor(x => x.DimensionY)
                .GreaterThan(5)
                .WithMessage("Minimum board size is 5 x 5.")
                .LessThanOrEqualTo(50)
                .WithMessage("Maximum board size is 50 x 50.");
        }
    }
}
