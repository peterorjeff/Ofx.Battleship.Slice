using Ofx.Battleship.API.Entities;

namespace Ofx.Battleship.API.UnitTests.Common.Builders
{
    public class GameBuilder
    {
        private readonly Game _game;
        private Board _board;

        public GameBuilder() => _game = new Game();

        public GameBuilder WithId(int id)
        {
            _game.GameId = id;
            return this;
        }

        public GameBuilder WithBoard(int id)
        {
            _board = new Board { BoardId = id };
            _game.Boards.Add(_board);
            return this;
        }

        public GameBuilder WithShip(int x, int y)
        {
            var ship = new Ship();
            ship.ShipParts.Add(new ShipPart { Ship = ship, X = x, Y = y });
            _board.Ships.Add(ship);
            return this;
        }

        public Game Build() => _game;
    }
}
