using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.Test.Common.Records
{
    public class ShotRecord
    {
        private readonly IBattleshipDbContext _context;
        private readonly Shot _shot;

        public ShotRecord(IBattleshipDbContext context)
        {
            _context = context;
            _shot = new Shot();
        }

        public ShotRecord WithShipId(int id)
        {
            _shot.ShotId = id;
            return this;
        }

        public ShotRecord WithAttacker(Player attacker)
        {
            _shot.Player = attacker;
            return this;
        }

        public ShotRecord WithBoard(Board board)
        {
            _shot.Board = board;
            return this;
        }

        public ShotRecord WithAttack(int x, int y)
        {
            _shot.AttackX = x;
            _shot.AttackY = y;
            return this;
        }

        public ShotRecord IsHit(ShipPart shipPart)
        {
            _shot.Hit = true;
            _shot.ShipPartHit = shipPart;
            return this;
        }

        public async Task<Shot> SaveAsync()
        {
            var shot = _context.Shots.Add(_shot);
            await _context.SaveChangesAsync(CancellationToken.None);
            return shot.Entity;
        }
    }
}
