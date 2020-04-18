using AutoMapper;
using MediatR;
using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.Entities;
using Ofx.Battleship.API.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.Features.Boards.Create
{
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
            var player = await _context.Players.FindAsync(request.PlayerId);
            if (player == null)
            {
                throw new NotFoundException(nameof(Player), request.PlayerId);
            }

            var entity = new Board
            {
                Player = player,
                DimensionX = request.DimensionX,
                DimensionY = request.DimensionY,
            };

            _context.Boards.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            var model = _mapper.Map<Model>(entity);

            return model;
        }
    }
}
