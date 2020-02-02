using FluentValidation;

namespace Ofx.Battleship.API.Features.Players.Create
{
    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(25)
                .WithMessage("Name cannot be longer than 25 characters.")
                .NotEmpty();
        }
    }
}
