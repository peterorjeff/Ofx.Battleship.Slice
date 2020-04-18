using Ofx.Battleship.API.Entities;

namespace Ofx.Battleship.API.UnitTests.Common.Builders
{
    public class GameBuilder
    {
        private readonly Game _game;
        private Player _player;

        public GameBuilder()
        {
            _game = new Game();
        }

        public GameBuilder WithId(int id)
        {
            _game.GameId = id;
            return this;
        }

        public GameBuilder WithPlayer(int id)
        {
            _player = new Player { PlayerId = id };
            _game.Players.Add(_player);
            return this;
        }

        public GameBuilder WithShip(int x, int y)
        {
            var ship = new Ship();
            ship.ShipParts.Add(new ShipPart { Ship = ship, X = x, Y = y });
            _player.Ships.Add(ship);
            return this;
        }

        public Game Build() => _game;
    }
}
