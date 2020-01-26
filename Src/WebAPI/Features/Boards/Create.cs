using AutoMapper;
using FluentValidation;
using MediatR;
using Ofx.Battleship.Application.Common.Exceptions;
using Ofx.Battleship.Application.Common.Interfaces;
using Ofx.Battleship.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.Features.Boards
{
    public class Create
    {
        public class Command : IRequest<Model>
        {
            public int GameId { get; set; }
            public int DimensionX { get; set; } = 10;
            public int DimensionY { get; set; } = 10;
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.GameId).GreaterThan(0).NotEmpty();

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
                var game = await _context.Games.FindAsync(request.GameId);
                if (game == null)
                {
                    throw new NotFoundException(nameof(Game), request.GameId);
                }

                var entity = new Board
                {
                    Game = game,
                    DimensionX = request.DimensionX,
                    DimensionY = request.DimensionY,
                };

                _context.Boards.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                var model = _mapper.Map<Model>(entity);

                return model;
            }
        }

        public class Model
        {
            public int BoardId { get; set; }
            public int DimensionX { get; set; }
            public int DimensionY { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile() =>
                CreateMap<Board, Model>(MemberList.Source);
        }
    }
}
