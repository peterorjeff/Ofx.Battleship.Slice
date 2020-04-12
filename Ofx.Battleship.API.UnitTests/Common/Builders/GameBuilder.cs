using Ofx.Battleship.API.Entities;

namespace Ofx.Battleship.API.UnitTests.Common.Builders
{
    public class GameBuilder
    {
        private readonly Game _game;

        public GameBuilder() => _game = new Game();

        public GameBuilder WithId(int id)
        {
            _game.GameId = id;
            return this;
        }

        public GameBuilder WithBoard(int id)
        {
            _game.Boards.Add(new Board { BoardId = id });
            return this;
        }

        public Game Build() => _game;
    }
}
