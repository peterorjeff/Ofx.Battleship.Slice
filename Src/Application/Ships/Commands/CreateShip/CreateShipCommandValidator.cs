using FluentValidation;
using Ofx.Battleship.Application.Common.Interfaces;

namespace Ofx.Battleship.Application.Ships.Commands.CreateShip
{
    public class CreateShipCommandValidator : AbstractValidator<CreateShipCommand>
    {
        private readonly IBattleshipDbContext _context;

        public CreateShipCommandValidator(IBattleshipDbContext context)
        {
            _context = context;

            RuleFor(x => x.BoardId)
                .GreaterThan(0)
                .NotEmpty();

            RuleFor(x => x.BowX)
                .GreaterThan(0)
                .WithMessage("Bow X must be greater than zero.")
                .Custom((bowX, context) =>
                {
                    var command = context.InstanceToValidate as CreateShipCommand;
                    var board = _context.Boards.Find(command.BoardId);
                    if (board == null) return;  // If no board found, cannot continue with validation. 
                    if (bowX > board.DimensionX)
                    {
                        context.AddFailure($"Bow X ({bowX}) cannot be larger than board dimension ({board.DimensionX})");
                    }
                    if (command.Orientation == Domain.Enums.ShipOrientation.Horizontal)
                    {
                        if (bowX + command.Length > board.DimensionX)
                        {
                            context.AddFailure("Ship too large to fit on board.");
                        }
                    }
                })
                .NotEmpty();
            
            RuleFor(x => x.BowY)
                .GreaterThan(0)
                .WithMessage("Bow Y must be greater than zero.")
                .Custom((bowY, context) =>
                {
                    var command = context.InstanceToValidate as CreateShipCommand;
                    var board = _context.Boards.Find(command.BoardId);
                    if (board == null) return;  // If no board found, cannot continue with validation. 
                    if (bowY > board.DimensionX)
                    {
                        context.AddFailure($"Bow Y ({bowY}) cannot be larger than board dimension ({board.DimensionY})");
                    }
                    if (command.Orientation == Domain.Enums.ShipOrientation.Vertical)
                    {
                        if (bowY + command.Length > board.DimensionY)
                        {
                            context.AddFailure("Ship too large to fit on board.");
                        }
                    }
                })
                .NotEmpty();
            
            RuleFor(x => x.Length)
                .GreaterThanOrEqualTo(2)
                .WithMessage("Minimum length of Ship is 2.")
                .LessThanOrEqualTo(4)
                .WithMessage("Maximum length of Ship is 4.")
                .NotEmpty();

            RuleFor(x => x.Orientation).IsInEnum().NotNull();
        }
    }
}
