using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.Entities;
using Ofx.Battleship.API.ServerTests.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.ServerTests.Records
{
    public static class BoardRecordExtensions
    {
        public static BoardRecord NewBoard(this Server server) => new BoardRecord(server.DbContext);
    }

    public class BoardRecord
    {
        private readonly IBattleshipDbContext _context;
        private readonly Board _board;

        public BoardRecord(IBattleshipDbContext context)
        {
            _context = context;
            // Currently these defaults are specified on the CreateBoardCommand object. As we are bypassing that to insert this record, we need to define defaults again here.
            // TODO: is there a better place to define them, so it will be only once? Or do they need to be constants/config somewhere?
            _board = new Board
            { 
                DimensionX = 10,
                DimensionY = 10
            };
        }

        public BoardRecord WithBoardId(int boardId)
        {
            _board.BoardId = boardId;
            return this;
        }

        public BoardRecord WithDimensions(int x, int y)
        {
            _board.DimensionX = x;
            _board.DimensionY = y;
            return this;
        }

        public BoardRecord WithGame(Game game)
        {
            _board.Game = game;
            return this;
        }

        public async Task<Board> SaveAsync()
        {
            var board = _context.Boards.Add(_board);
            await _context.SaveChangesAsync(CancellationToken.None);
            return board.Entity;
        }
    }
}
