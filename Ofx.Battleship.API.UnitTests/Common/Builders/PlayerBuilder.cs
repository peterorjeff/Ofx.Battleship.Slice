using Ofx.Battleship.API.Entities;

namespace Ofx.Battleship.API.UnitTests.Common.Builders
{
    public class PlayerBuilder
    {
        private readonly Player _player;

        public PlayerBuilder() => _player = new Player();

        public PlayerBuilder WithId(int id)
        {
            _player.PlayerId = id;
            return this;
        }

        public PlayerBuilder WithName(string name)
        {
            _player.Name = name;
            return this;
        }

        public Player Build() => _player;
    }
}
