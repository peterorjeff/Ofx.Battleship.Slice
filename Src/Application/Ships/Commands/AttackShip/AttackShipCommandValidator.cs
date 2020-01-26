using FluentValidation;
using Ofx.Battleship.Application.Common.Interfaces;

namespace Ofx.Battleship.Application.Ships.Commands.AttackShip
{
    public class AttackShipCommandValidator : AbstractValidator<AttackShipCommand>
    {
        private readonly IBattleshipDbContext _context;

        public AttackShipCommandValidator(IBattleshipDbContext context)
        {
            _context = context;

            RuleFor(x => x.BoardId)
                .GreaterThan(0)
                .NotEmpty();

            RuleFor(x => x.AttackX)
                .GreaterThan(0)
                .WithMessage("Attack X must be greater than zero.")
                .Custom((attackX, context) =>
                {
                    var command = context.InstanceToValidate as AttackShipCommand;
                    var board = _context.Boards.Find(command.BoardId);
                    if (board == null) return;  // If no board found, cannot continue with validation. 
                    if (attackX > board.DimensionX)
                    {
                        context.AddFailure($"Attack X ({attackX}) cannot be larger than board dimension ({board.DimensionX})");
                    }
                })
                .NotEmpty();

            RuleFor(x => x.AttackY)
                .GreaterThan(0)
                .WithMessage("Attack Y must be greater than zero.")
                .Custom((attackY, context) =>
                {
                    var command = context.InstanceToValidate as AttackShipCommand;
                    var board = _context.Boards.Find(command.BoardId);
                    if (board == null) return;  // If no board found, cannot continue with validation. 
                    if (attackY > board.DimensionY)
                    {
                        context.AddFailure($"Attack Y ({attackY}) cannot be larger than board dimension ({board.DimensionY})");
                    }
                })
                .NotEmpty();
        }
    }
}
