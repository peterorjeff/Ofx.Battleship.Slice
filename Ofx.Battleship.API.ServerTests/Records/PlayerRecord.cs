using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.ServerTests.Records
{
    public class PlayerRecord
    {
        private readonly IBattleshipDbContext _context;
        private readonly Player _player;

        public PlayerRecord(IBattleshipDbContext context)
        {
            _context = context;
            _player = new Player();
        }

        public PlayerRecord WithPlayerId(int id)
        {
            _player.PlayerId = id;
            return this;
        }

        public PlayerRecord WithPlayerName(string name)
        {
            _player.Name = name;
            return this;
        }

        public async Task<Player> SaveAsync()
        {
            var player = _context.Players.Add(_player);
            await _context.SaveChangesAsync(CancellationToken.None);
            return player.Entity;
        }
    }
}
