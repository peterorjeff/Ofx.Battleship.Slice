using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.ServerTests.Records
{
    public class GameRecord
    {
        private readonly IBattleshipDbContext _context;
        private readonly Game _game;

        public GameRecord(IBattleshipDbContext context)
        {
            _context = context;
            _game = new Game();
        }

        public GameRecord WithGameId(int gameId)
        {
            _game.GameId = gameId;
            return this;
        }

        public async Task<Game> SaveAsync()
        {
            var game = _context.Games.Add(_game);
            await _context.SaveChangesAsync(CancellationToken.None);
            return game.Entity;
        }
    }
}
