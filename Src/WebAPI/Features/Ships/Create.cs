using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ofx.Battleship.Application.Common.Exceptions;
using Ofx.Battleship.Application.Common.Interfaces;
using Ofx.Battleship.Domain.Entities;
using Ofx.Battleship.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.Features.Ships
{
    public class Create
    {
        public class Command : IRequest<int>
        {
            public int BoardId { get; set; }
            public int BowX { get; set; }
            public int BowY { get; set; }
            public int Length { get; set; }
            public ShipOrientation Orientation { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            private readonly IBattleshipDbContext _context;

            public CommandValidator(IBattleshipDbContext context)
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
                        var command = context.InstanceToValidate as Command;
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
                        var command = context.InstanceToValidate as Command;
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

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IBattleshipDbContext _context;

            public Handler(IBattleshipDbContext context) => _context = context;

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var board = await _context.Boards.FindAsync(request.BoardId);
                if (board == null)
                {
                    throw new NotFoundException(nameof(Board), request.BoardId);
                }

                // Generate X & Y lists based on Orientation and Length
                var xList = new List<int>();
                var yList = new List<int>();
                switch (request.Orientation)
                {
                    case ShipOrientation.Horizontal:
                        for (int i = 0; i < request.Length; i++) xList.Add(request.BowX + i);
                        yList.Add(request.BowY);
                        break;
                    case ShipOrientation.Vertical:
                        for (int i = 0; i < request.Length; i++) yList.Add(request.BowY + i);
                        xList.Add(request.BowX);
                        break;
                }

                // Collision Detection
                // Project the X & Y and compare to xList && yList. This works as X or Y is always a single integer list.
                var collisions = _context.Ships
                    .Include(ship => ship.ShipParts)
                    .Where(ship => ship.Board == board)
                    .SelectMany(ship => ship.ShipParts, (ship, parts) => new
                    {
                        parts.X,
                        parts.Y
                    })
                    .Where(part => xList.Contains(part.X) && yList.Contains(part.Y))
                    .AsNoTracking();

                if (collisions.Any())
                {
                    throw new ShipCollisionException(collisions.Select(x => $"[{x.X},{x.Y}]").ToList());
                }

                // Safe to insert the new Ship.
                var ship = new Ship
                {
                    Board = board
                };
                _context.Ships.Add(ship);

                // Project X or Y list as ShipParts based on Orientation. 
                switch (request.Orientation)
                {
                    case ShipOrientation.Horizontal:
                        _context.ShipParts.AddRange(xList
                            .Select(x => new ShipPart
                            {
                                Ship = ship,
                                X = x,
                                Y = request.BowY
                            })
                        );
                        break;
                    case ShipOrientation.Vertical:
                        _context.ShipParts.AddRange(yList
                            .Select(y => new ShipPart
                            {
                                Ship = ship,
                                X = request.BowX,
                                Y = y
                            })
                        );
                        break;
                }

                await _context.SaveChangesAsync(cancellationToken);

                return ship.ShipId;
            }
        }
    }
}
