using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.Entities;
using Ofx.Battleship.API.ServerTests.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.ServerTests.Records
{
    public static class ShipPartRecordExtensions
    {
        public static ShipPartRecord NewShipPart(this Server server) => new ShipPartRecord(server.DbContext);
    }

    public class ShipPartRecord
    {
        private readonly IBattleshipDbContext _context;
        private readonly ShipPart _shipPart;

        public ShipPartRecord(IBattleshipDbContext context)
        {
            _context = context;
            _shipPart = new ShipPart();
        }

        public ShipPartRecord WithShipPartId(int id)
        {
            _shipPart.ShipPartId = id;
            return this;
        }

        public ShipPartRecord WithShip(Ship ship)
        {
            _shipPart.Ship = ship;
            return this;
        }

        public ShipPartRecord WithCoordinates(int x, int y)
        {
            _shipPart.X = x;
            _shipPart.Y = y;
            return this;
        }

        public async Task<ShipPart> SaveAsync()
        {
            var shipPart = _context.ShipParts.Add(_shipPart);
            await _context.SaveChangesAsync(CancellationToken.None);
            return shipPart.Entity;
        }
    }
}
