using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.Entities;
using Ofx.Battleship.API.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.Features.Ships.Attack
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
}
