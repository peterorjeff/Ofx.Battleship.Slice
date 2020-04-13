using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.Entities;
using Ofx.Battleship.API.ServerTests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.ServerTests.Records
{
    public static class ShipRecordExtensions
    {
        public static ShipRecord NewShip(this Server server) => new ShipRecord(server.DbContext);
    }

    public class ShipRecord
    {
        private readonly IBattleshipDbContext _context;
        private readonly Ship _ship;

        public ShipRecord(IBattleshipDbContext context)
        {
            _context = context;
            _ship = new Ship();
        }

        public ShipRecord WithShipId(int id)
        {
            _ship.ShipId = id;
            return this;
        }

        public ShipRecord WithBoard(Board board)
        {
            _ship.Board = board;
            return this;
        }

        public async Task<Ship> SaveAsync()
        {
            var ship = _context.Ships.Add(_ship);
            await _context.SaveChangesAsync(CancellationToken.None);
            return ship.Entity;
        }
    }
}
