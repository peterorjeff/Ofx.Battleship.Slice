using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.Entities;
using Ofx.Battleship.API.ServerTests.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.ServerTests.Records
{
    public static class GameRecordExtensions
    {
        public static GameRecord NewGame(this Server server) => new GameRecord(server.DbContext);
    }

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
