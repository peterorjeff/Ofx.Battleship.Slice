using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.Entities;
using Ofx.Battleship.API.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.Features.Ships
{
    public class Attack
    {
        public class Command : IRequest<Model>
        {
            public int BoardId { get; set; }
            public int AttackX { get; set; }
            public int AttackY { get; set; }
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

                RuleFor(x => x.AttackX)
                    .GreaterThan(0)
                    .WithMessage("Attack X must be greater than zero.")
                    .Custom((attackX, context) =>
                    {
                        var command = context.InstanceToValidate as Command;
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
                        var command = context.InstanceToValidate as Command;
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

        public class Handler : IRequestHandler<Command, Model>
        {
            private readonly IBattleshipDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IBattleshipDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Model> Handle(Command request, CancellationToken cancellationToken)
            {
                var board = await _context.Boards.FindAsync(request.BoardId);
                if (board == null)
                {
                    throw new NotFoundException(nameof(Board), request.BoardId);
                }

                var part = await _context.ShipParts
                    .Where(x => x.Ship.Board == board)
                    .Where(x => x.X == request.AttackX && x.Y == request.AttackY)
                    .FirstOrDefaultAsync();

                if (part == null)
                {
                    // Miss
                    return new Model();
                }

                if (!part.Hit)
                {
                    // Hit
                    part.Hit = true;
                    await _context.SaveChangesAsync(cancellationToken);
                }

                var viewModel = _mapper.Map<Model>(part);

                return viewModel;
            }
        }

        public class Model
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool Hit { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile() =>
                CreateMap<ShipPart, Model>(MemberList.Source);
        }
    }
}
