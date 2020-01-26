using AutoMapper;
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

namespace Ofx.Battleship.Application.Ships.Commands.CreateShip
{
    public class CreateShipCommandHandler : IRequestHandler<CreateShipCommand, ShipViewModel>
    {
        private readonly IBattleshipDbContext _context;
        private readonly IMapper _mapper;

        public CreateShipCommandHandler(IBattleshipDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShipViewModel> Handle(CreateShipCommand request, CancellationToken cancellationToken)
        {
            var board = await _context.Boards.FindAsync(request.BoardId);
            if (board == null)
            {
                throw new NotFoundException(nameof(Board), request.BoardId);
            }

            // Generate X & Y lists based on Orientation and Length
            var xList = new List<int>();
            var yList = new List<int>();
            switch(request.Orientation)
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
            switch(request.Orientation)
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

            var viewModel = _mapper.Map<ShipViewModel>(ship);

            return viewModel;
        }
    }
}
