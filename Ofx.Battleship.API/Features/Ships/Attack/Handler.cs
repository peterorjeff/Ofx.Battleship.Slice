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
            var attacker = await _context.Players.FindAsync(request.AttackerPlayerId);
            if (attacker == null)
            {
                throw new NotFoundException(nameof(Player), request.AttackerPlayerId);
            }

            var lastAttack = await _context.Shots
                .OrderByDescending(x => x.ShotId)
                .FirstOrDefaultAsync();
            if (lastAttack?.Player == attacker)
            {
                throw new OutOfTurnException(attacker.Name);
            }

            var board = await _context.Boards.FindAsync(request.BoardId);
            if (board == null)
            {
                throw new NotFoundException(nameof(Board), request.BoardId);
            }

            // TODO: Ensure the attacker is attacking opponent board in the current game.

            // Check hit or miss
            var part = await _context.ShipParts
                .Where(x => x.Ship.Board == board)
                .Where(x => x.X == request.AttackX && x.Y == request.AttackY)
                .FirstOrDefaultAsync();

            var viewModel = new Model();
            if (part != null)
            {
                part.Hit = true;
                viewModel = _mapper.Map<Model>(part);
            }

            // Record shot.
            var shot = new Shot
            {
                Player = attacker,
                Board = board,
                AttackX = request.AttackX,
                AttackY = request.AttackY,
                Hit = part?.Hit ?? false,
                ShipPartHit = part
            };
            await _context.Shots.AddAsync(shot);


            await _context.SaveChangesAsync(cancellationToken);

            return viewModel;
        }
    }
}
